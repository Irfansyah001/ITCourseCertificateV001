using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    public partial class FormKelolaMateri : Form
    {
        private string connStr = "Data Source=LAPTOPGW1;Initial Catalog=CertificateCourseDB;Integrated Security=True";
        private BindingSource bsKursus = new BindingSource();

        public FormKelolaMateri()
        {
            InitializeComponent();
        }


        private async void FormKelolaMateri_Load(object sender, EventArgs e)
        {
            await Task.Delay(200);
            LoadKursus();

        }


        private void LoadKursus()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT KursusID, JudulKursus FROM DataKursus", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                bsKursus.DataSource = dt;
                cmbKursus.DataSource = bsKursus;
                cmbKursus.DisplayMember = "JudulKursus";
                cmbKursus.ValueMember = "KursusID";

                if (dt.Rows.Count > 0)
                    cmbKursus.SelectedIndex = 0;
            }
        }

        private void cmbKursus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbKursus.SelectedValue is int)
                LoadMateri();
        }

        private void LoadMateri()
        {
            if (cmbKursus.SelectedValue == null) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("EXEC GetMateriByKursusID @kursusID", conn);
                cmd.Parameters.AddWithValue("@kursusID", cmbKursus.SelectedValue);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvMateri.DataSource = dt;
                dgvMateri.Columns["MateriID"].Visible = false;
                dgvMateri.Columns["KursusID"].Visible = false;
            }
        }

        private void ClearInput()
        {
            txtJudul.Text = "";
            txtLink.Text = "";
            txtDurasi.Text = "";
            txtUrutan.Text = "";
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (cmbKursus.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih kursus terlebih dahulu!");
                return;
            }

            if (txtJudul.Text.Length > 100)
            {
                MessageBox.Show("Judul materi maksimal 100 karakter.");
                return;
            }

            if (!txtLink.Text.Contains("youtube.com") && !txtLink.Text.Contains("youtu.be"))
            {
                MessageBox.Show("Link video harus dari YouTube.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand("EXEC InsertMateri @kursus, @judul, @link, @urutan, @durasi", conn, trans);
                    cmd.Parameters.AddWithValue("@kursus", cmbKursus.SelectedValue);
                    cmd.Parameters.AddWithValue("@judul", txtJudul.Text.Trim());
                    cmd.Parameters.AddWithValue("@link", txtLink.Text.Trim());
                    cmd.Parameters.AddWithValue("@urutan", int.Parse(txtUrutan.Text));
                    cmd.Parameters.AddWithValue("@durasi", int.Parse(txtDurasi.Text));
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }

            LoadMateri();
            ClearInput();
        }

        private void dgvMateri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvMateri.Rows[e.RowIndex];
                txtJudul.Text = row.Cells["JudulMateri"].Value?.ToString();
                txtLink.Text = row.Cells["LinkVideo"].Value?.ToString();
                txtDurasi.Text = row.Cells["DurasiMenit"].Value?.ToString();
                txtUrutan.Text = row.Cells["Urutan"].Value?.ToString();

                if (row.Cells["KursusID"].Value != null)
                    cmbKursus.SelectedValue = Convert.ToInt32(row.Cells["KursusID"].Value);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvMateri.CurrentRow == null) return;

            if (txtJudul.Text.Length > 100)
            {
                MessageBox.Show("Judul materi maksimal 100 karakter.");
                return;
            }

            if (!txtLink.Text.Contains("youtube.com") && !txtLink.Text.Contains("youtu.be"))
            {
                MessageBox.Show("Link video harus dari YouTube.");
                return;
            }

            if (!int.TryParse(txtUrutan.Text, out int urutan) || !int.TryParse(txtDurasi.Text, out int durasi))
            {
                MessageBox.Show("Urutan dan durasi harus berupa angka.");
                return;
            }

            int id = Convert.ToInt32(dgvMateri.CurrentRow.Cells["MateriID"].Value);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand("EXEC UpdateMateri @MateriID, @KursusID, @JudulMateri, @LinkVideo, @Urutan, @DurasiMenit", conn, trans);
                    cmd.Parameters.AddWithValue("@MateriID", id);
                    cmd.Parameters.AddWithValue("@KursusID", Convert.ToInt32(cmbKursus.SelectedValue));
                    cmd.Parameters.AddWithValue("@JudulMateri", txtJudul.Text.Trim());
                    cmd.Parameters.AddWithValue("@LinkVideo", txtLink.Text.Trim());
                    cmd.Parameters.AddWithValue("@Urutan", urutan);
                    cmd.Parameters.AddWithValue("@DurasiMenit", durasi);
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }

            LoadMateri();
            ClearInput();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMateri.CurrentRow == null || dgvMateri.CurrentRow.Cells["MateriID"].Value == null) return;

            DialogResult result = MessageBox.Show("Yakin ingin menghapus?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvMateri.CurrentRow.Cells["MateriID"].Value);

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        SqlCommand cmd = new SqlCommand("EXEC DeleteMateri @id", conn, trans);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Gagal menghapus data: " + ex.Message);
                    }
                }

                LoadMateri();
                ClearInput();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMateri();
            ClearInput();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (cmbKursus.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih kursus terlebih dahulu.");
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog { Filter = "Excel File|*.xlsx" };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(ofd.FileName)))
                {
                    ExcelWorksheet sheet = package.Workbook.Worksheets[0];
                    int rowCount = sheet.Dimension.Rows;

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        SqlTransaction trans = conn.BeginTransaction();
                        try
                        {
                            for (int i = 2; i <= rowCount; i++)
                            {
                                SqlCommand cmd = new SqlCommand("EXEC InsertMateri @k, @j, @l, @u, @d", conn, trans);
                                cmd.Parameters.AddWithValue("@k", cmbKursus.SelectedValue);
                                cmd.Parameters.AddWithValue("@j", sheet.Cells[i, 1].Text);
                                cmd.Parameters.AddWithValue("@l", sheet.Cells[i, 2].Text);
                                cmd.Parameters.AddWithValue("@u", int.Parse(sheet.Cells[i, 3].Text));
                                cmd.Parameters.AddWithValue("@d", int.Parse(sheet.Cells[i, 4].Text));
                                cmd.ExecuteNonQuery();
                            }
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            MessageBox.Show("Gagal import: " + ex.Message);
                        }
                    }
                }

                LoadMateri();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvMateri.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk diexport.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV File|*.csv",
                FileName = "Kumpulan_Materi.csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(sfd.FileName))
                {
                    for (int i = 0; i < dgvMateri.Columns.Count; i++)
                    {
                        if (dgvMateri.Columns[i].Visible)
                        {
                            writer.Write(dgvMateri.Columns[i].HeaderText);
                            if (i < dgvMateri.Columns.Count - 1)
                                writer.Write(",");
                        }
                    }
                    writer.WriteLine();

                    foreach (DataGridViewRow row in dgvMateri.Rows)
                    {
                        for (int i = 0; i < dgvMateri.Columns.Count; i++)
                        {
                            if (dgvMateri.Columns[i].Visible)
                            {
                                writer.Write(row.Cells[i].Value?.ToString());
                                if (i < dgvMateri.Columns.Count - 1)
                                    writer.Write(",");
                            }
                        }
                        writer.WriteLine();
                    }
                }

                MessageBox.Show("Export berhasil disimpan.");
            }
        }

        private void btnPrintPDF_Click(object sender, EventArgs e)
        {
            if (dgvMateri.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk dicetak.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                FileName = "MateriKursus.pdf"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    Document doc = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    PdfPTable table = new PdfPTable(dgvMateri.Columns.Count);
                    foreach (DataGridViewColumn col in dgvMateri.Columns)
                    {
                        table.AddCell(new Phrase(col.HeaderText));
                    }

                    foreach (DataGridViewRow row in dgvMateri.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            table.AddCell(new Phrase(cell.Value?.ToString()));
                        }
                    }

                    doc.Add(table);
                    doc.Close();
                }

                MessageBox.Show("PDF berhasil dibuat.");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
