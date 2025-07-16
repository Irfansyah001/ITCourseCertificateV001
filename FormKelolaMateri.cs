using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.VisualBasic.ApplicationServices;
using OfficeOpenXml;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ITCourseCertificateV001
{
    public partial class FormKelolaMateri : Form
    {
        //Koneksi kn = new Koneksi(); // Koneksi ke database menggunakan class Koneksi
        //string strKonek = ""; // string koneksi ke database
        //private string connStr = "Data Source=LAPTOPGW1;Initial Catalog=CertificateCourseDB;Integrated Security=True";
        //string connString = ITCourseCertificateV001.Properties.Settings.Default.CertificateCourseDBConnectionString;
        private BindingSource bsKursus = new BindingSource();
        private int currentUserID;
        private Form previousForm;

        public FormKelolaMateri(int userID, FormDashboard dashboardForm)
        {
            InitializeComponent();
            //strKonek = kn.connectionString();
            currentUserID = userID;
            previousForm = dashboardForm;
        }


        private async void FormKelolaMateri_Load(object sender, EventArgs e)
        {
            await Task.Delay(200);
            LoadKursus();

        }


        private void LoadKursus()
        {

            string currentConnString = Koneksi.GetConnectionString();
            if (string.IsNullOrEmpty(currentConnString))
            {
                MessageBox.Show("String koneksi database tidak valid. Mohon periksa pengaturan IP.", "Kesalahan Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(currentConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT KursusID, JudulKursus FROM DataKursus", conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
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
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Terjadi masalah database saat memuat daftar kursus: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga saat memuat daftar kursus: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbKursus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbKursus.SelectedValue is int)
                LoadMateri();
        }

        private void dgvMateri_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvMateri.Columns[e.ColumnIndex].Name == "LinkVideo")
            {
                string url = dgvMateri.Rows[e.RowIndex].Cells["LinkVideo"].Value?.ToString(); // Gunakan nama kolom yang spesifik
                if (!string.IsNullOrEmpty(url))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(url);
                    }
                    catch (System.ComponentModel.Win32Exception noBrowsr)
                    {
                        // Terjadi jika tidak ada browser default atau link tidak valid
                        MessageBox.Show("Tidak dapat membuka link. Pastikan Anda memiliki browser terinstal dan link valid.", "Kesalahan Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Log 'noBrowser'
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal membuka link video: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Log 'ex'
                    }
                }
            }
        }

        private void LoadMateri()
        {
            if (cmbKursus.SelectedValue == null) return;

            string currentConnString = Koneksi.GetConnectionString();
            if (string.IsNullOrEmpty(currentConnString))
            {
                MessageBox.Show("String koneksi database tidak valid. Mohon periksa pengaturan IP.", "Kesalahan Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(currentConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("EXEC GetMateriByKursusID @kursusID", conn))
                    {
                        cmd.Parameters.AddWithValue("@kursusID", cmbKursus.SelectedValue);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            dgvMateri.DataSource = dt;
                            dgvMateri.Columns["MateriID"].Visible = false;
                            dgvMateri.Columns["KursusID"].Visible = false;
                            dgvMateri.Columns["LinkVideo"].DefaultCellStyle.ForeColor = Color.Blue;
                            dgvMateri.Columns["LinkVideo"].DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Underline);
                            dgvMateri.Columns["LinkVideo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Terjadi masalah database saat memuat materi: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga saat memuat materi: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (cmbKursus.SelectedValue == null || cmbKursus.SelectedIndex == -1) // Tambahkan null check untuk SelectedValue
            {
                MessageBox.Show("Pilih kursus terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtJudul.Text) || string.IsNullOrWhiteSpace(txtLink.Text) ||
                string.IsNullOrWhiteSpace(txtUrutan.Text) || string.IsNullOrWhiteSpace(txtDurasi.Text))
            {
                MessageBox.Show("Semua kolom harus diisi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtJudul.Text.Length > 100)
            {
                MessageBox.Show("Judul materi maksimal 100 karakter.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi link harus diawali dengan http:// atau https://
            if (!txtLink.Text.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !txtLink.Text.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Link video harus diawali dengan http:// atau https://.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi apakah link adalah link YouTube yang valid
            // Pola regex ini mencocokkan berbagai format URL YouTube (youtube.com, youtu.be)
            string youtubeUrlPattern = @"^(https?:\/\/)?(www\.)?(youtube\.com|youtu\.be)\/.+$";
            if (!Regex.IsMatch(txtLink.Text, youtubeUrlPattern, RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Link video yang dimasukkan bukan link YouTube yang valid.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Gunakan int.TryParse untuk input numerik
            if (!int.TryParse(txtUrutan.Text, out int urutan))
            {
                MessageBox.Show("Urutan harus berupa angka.", "Kesalahan Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtDurasi.Text, out int durasi))
            {
                MessageBox.Show("Durasi harus berupa angka.", "Kesalahan Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string currentConnString = Koneksi.GetConnectionString();
            if (string.IsNullOrEmpty(currentConnString))
            {
                MessageBox.Show("String koneksi database tidak valid. Mohon periksa pengaturan IP.", "Kesalahan Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(currentConnString))
                {
                    conn.Open();
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        using (SqlCommand cmd = new SqlCommand("EXEC InsertMateri @kursus, @judul, @link, @urutan, @durasi", conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@kursus", cmbKursus.SelectedValue);
                            cmd.Parameters.AddWithValue("@judul", txtJudul.Text.Trim());
                            cmd.Parameters.AddWithValue("@link", txtLink.Text.Trim());
                            cmd.Parameters.AddWithValue("@urutan", urutan);
                            cmd.Parameters.AddWithValue("@durasi", durasi);
                            cmd.ExecuteNonQuery();
                            trans.Commit();
                            MessageBox.Show("Materi berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Terjadi masalah database saat menyimpan materi: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga saat menyimpan materi: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Validasi link harus diawali dengan http:// atau https://
            if (!txtLink.Text.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !txtLink.Text.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Link video harus diawali dengan http:// atau https://.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi apakah link adalah link YouTube yang valid
            string youtubeUrlPattern = @"^(https?:\/\/)?(www\.)?(youtube\.com|youtu\.be)\/.+$";
            if (!Regex.IsMatch(txtLink.Text, youtubeUrlPattern, RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Link video yang dimasukkan bukan link YouTube yang valid.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtUrutan.Text, out int urutan) || !int.TryParse(txtDurasi.Text, out int durasi))
            {
                MessageBox.Show("Urutan dan durasi harus berupa angka.");
                return;
            }

            int id = Convert.ToInt32(dgvMateri.CurrentRow.Cells["MateriID"].Value);

            string currentConnString = Koneksi.GetConnectionString();
            if (string.IsNullOrEmpty(currentConnString))
            {
                MessageBox.Show("String koneksi database tidak valid. Mohon periksa pengaturan IP.", "Kesalahan Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(currentConnString))
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

            }

            catch (SqlException ex)
            {
                MessageBox.Show("Terjadi masalah database saat memperbarui materi: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga saat memperbarui materi: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadMateri();
            ClearInput();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMateri.CurrentRow == null || dgvMateri.CurrentRow.Cells["MateriID"].Value == null)
            {
                MessageBox.Show("Pilih materi yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Yakin ingin menghapus materi ini?", "Konfirmasi Penghapusan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvMateri.CurrentRow.Cells["MateriID"].Value);

                string currentConnString = Koneksi.GetConnectionString();
                if (string.IsNullOrEmpty(currentConnString))
                {
                    MessageBox.Show("String koneksi database tidak valid. Mohon periksa pengaturan IP.", "Kesalahan Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    using (SqlConnection conn = new SqlConnection(currentConnString))
                    {
                        conn.Open();
                        using (SqlTransaction trans = conn.BeginTransaction())
                        {
                            using (SqlCommand cmd = new SqlCommand("EXEC DeleteMateri @id", conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.ExecuteNonQuery();
                                trans.Commit();
                                MessageBox.Show("Materi berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Terjadi masalah database saat menghapus data: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan tak terduga saat menghapus data: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (cmbKursus.SelectedValue == null || cmbKursus.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih kursus terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog { Filter = "Excel File|*.xlsx", Title = "Pilih File Excel untuk Import Materi" };

            if (ofd.ShowDialog() == DialogResult.OK)
            {

                string currentConnString = Koneksi.GetConnectionString();
                if (string.IsNullOrEmpty(currentConnString))
                {
                    MessageBox.Show("String koneksi database tidak valid. Mohon periksa pengaturan IP.", "Kesalahan Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    // Pastikan Anda memiliki paket NuGet EPPlus terinstal dengan benar.
                    // Jika Anda menggunakan versi EPPlus 5 ke atas, lisensi non-komersial harus disebutkan.
                    // ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Tambahkan ini jika perlu

                    using (ExcelPackage package = new ExcelPackage(new FileInfo(ofd.FileName)))
                    {
                        ExcelWorksheet sheet = package.Workbook.Worksheets[0]; // Ambil sheet pertama
                        if (sheet == null)
                        {
                            MessageBox.Show("File Excel tidak memiliki sheet yang dapat dibaca.", "Kesalahan Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        int rowCount = sheet.Dimension?.Rows ?? 0; // Pastikan Dimension tidak null
                        if (rowCount <= 1) // Jika hanya header atau kosong
                        {
                            MessageBox.Show("File Excel tidak memiliki data materi.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        int importedCount = 0;
                        int failedCount = 0;

                        using (SqlConnection conn = new SqlConnection(currentConnString))
                        {
                            conn.Open();
                            using (SqlTransaction trans = conn.BeginTransaction())
                            {
                                try
                                {
                                    for (int i = 2; i <= rowCount; i++) // Mulai dari baris ke-2 (asumsi baris 1 adalah header)
                                    {
                                        string judul = sheet.Cells[i, 1].Text?.Trim();
                                        string link = sheet.Cells[i, 2].Text?.Trim();
                                        string urutanText = sheet.Cells[i, 3].Text?.Trim();
                                        string durasiText = sheet.Cells[i, 4].Text?.Trim();

                                        // Validasi data dari Excel
                                        if (string.IsNullOrWhiteSpace(judul) || string.IsNullOrWhiteSpace(link) ||
                                            string.IsNullOrWhiteSpace(urutanText) || string.IsNullOrWhiteSpace(durasiText))
                                        {
                                            MessageBox.Show($"Baris {i} di Excel tidak lengkap (Judul, Link, Urutan, atau Durasi kosong). Melewati baris ini.", "Peringatan Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            failedCount++;
                                            continue;
                                        }

                                        if (!int.TryParse(urutanText, out int urutan))
                                        {
                                            MessageBox.Show($"Urutan di baris {i} tidak valid (bukan angka). Melewati baris ini.", "Peringatan Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            failedCount++;
                                            continue;
                                        }

                                        if (!int.TryParse(durasiText, out int durasi))
                                        {
                                            MessageBox.Show($"Durasi di baris {i} tidak valid (bukan angka). Melewati baris ini.", "Peringatan Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            failedCount++;
                                            continue;
                                        }

                                        // Validasi link harus diawali dengan http:// atau https://
                                        if (!link.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !link.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                                        {
                                            MessageBox.Show($"Link video di baris {i} tidak valid (harus diawali http:// atau https://). Melewati baris ini.", "Peringatan Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            failedCount++;
                                            continue;
                                        }

                                        // Validasi apakah link adalah link YouTube yang valid
                                        string youtubeUrlPattern = @"^(https?:\/\/)?(www\.)?(youtube\.com|youtu\.be)\/.+$";
                                        if (!Regex.IsMatch(link, youtubeUrlPattern, RegexOptions.IgnoreCase))
                                        {
                                            MessageBox.Show($"Link video di baris {i} bukan link YouTube yang valid. Melewati baris ini.", "Peringatan Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            failedCount++;
                                            continue;
                                        }


                                        using (SqlCommand cmd = new SqlCommand("EXEC InsertMateri @k, @j, @l, @u, @d", conn, trans))
                                        {
                                            cmd.Parameters.AddWithValue("@k", cmbKursus.SelectedValue);
                                            cmd.Parameters.AddWithValue("@j", judul);
                                            cmd.Parameters.AddWithValue("@l", link);
                                            cmd.Parameters.AddWithValue("@u", urutan);
                                            cmd.Parameters.AddWithValue("@d", durasi);
                                            cmd.ExecuteNonQuery();
                                            importedCount++;
                                        }
                                    }
                                    trans.Commit(); // Commit transaksi jika semua baris berhasil diproses
                                    MessageBox.Show($"Import berhasil. {importedCount} materi ditambahkan. {failedCount} baris gagal diimport.", "Import Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (SqlException sqlEx)
                                {
                                    if (trans != null) trans.Rollback(); // Rollback hanya jika ada error SQL di dalam loop
                                    MessageBox.Show("Gagal import data ke database: " + sqlEx.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    // Log 'sqlEx'
                                }
                                catch (Exception innerEx) // Menangkap pengecualian umum lainnya di dalam transaksi
                                {
                                    if (trans != null) trans.Rollback(); // Rollback jika ada error lain di dalam loop
                                    MessageBox.Show("Terjadi kesalahan saat memproses data Excel: " + innerEx.Message, "Kesalahan Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    // Log 'innerEx'
                                }
                            } // end using SqlTransaction
                        } // end using SqlConnection
                    } // end using ExcelPackage
                }
                catch (IOException ioEx)
                {
                    MessageBox.Show("Gagal membaca file Excel. Pastikan file tidak sedang dibuka oleh aplikasi lain: " + ioEx.Message, "Kesalahan File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Log 'ioEx'
                }
                catch (System.InvalidOperationException opex) // Ini adalah perbaikan utamanya!
                {
                    MessageBox.Show("Format file Excel tidak valid atau sheet tidak ditemukan. Error: " + opex.Message, "Kesalahan Format Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Log 'opex'
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan tak terduga saat import: " + ex.Message, "Kesalahan Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Log 'ex'
                }

                LoadMateri(); // Refresh data di DataGridView setelah import
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
                MessageBox.Show("Tidak ada data untuk dicetak.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                FileName = "MateriKursus.pdf",
                Title = "Simpan Materi Kursus sebagai PDF"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        Document doc = new Document(PageSize.A4.Rotate()); // Contoh: A4 landscape jika banyak kolom
                        PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                        doc.Open();

                        // Tambahkan judul dokumen - PERBAIKAN AMBIGUITAS FONT DI SINI
                        Paragraph title = new Paragraph("Daftar Materi Kursus", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD));
                        title.Alignment = Element.ALIGN_CENTER;
                        doc.Add(title);
                        doc.Add(new Paragraph(" ")); // Spasi

                        // Filter hanya kolom yang terlihat di DataGridView
                        int visibleColumnCount = 0;
                        foreach (DataGridViewColumn col in dgvMateri.Columns)
                        {
                            if (col.Visible)
                            {
                                visibleColumnCount++;
                            }
                        }

                        if (visibleColumnCount == 0)
                        {
                            MessageBox.Show("Tidak ada kolom yang terlihat di tabel untuk dicetak.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            doc.Close(); // Pastikan dokumen ditutup
                            return;
                        }

                        PdfPTable table = new PdfPTable(visibleColumnCount);
                        table.WidthPercentage = 100; // Tabel mengisi lebar halaman
                        table.SetWidths(GetColumnWidths(dgvMateri)); // Atur lebar kolom berdasarkan DGV

                        // Tambahkan header kolom (hanya yang terlihat)
                        foreach (DataGridViewColumn col in dgvMateri.Columns)
                        {
                            if (col.Visible)
                            {
                                // PERBAIKAN AMBIGUITAS FONT DI SINI
                                PdfPCell headerCell = new PdfPCell(new Phrase(col.HeaderText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD)));
                                headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.AddCell(headerCell);
                            }
                        }

                        // Tambahkan data baris (hanya dari kolom yang terlihat)
                        foreach (DataGridViewRow row in dgvMateri.Rows)
                        {
                            foreach (DataGridViewColumn col in dgvMateri.Columns)
                            {
                                if (col.Visible)
                                {
                                    // PERBAIKAN AMBIGUITAS FONT DI SINI
                                    table.AddCell(new Phrase(row.Cells[col.Index].Value?.ToString() ?? string.Empty, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8)));
                                }
                            }
                        }

                        doc.Add(table);
                        doc.Close();

                        MessageBox.Show("PDF berhasil dibuat dan disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (IOException ioEx)
                {
                    MessageBox.Show("Gagal menyimpan file PDF. Pastikan file tidak sedang dibuka oleh aplikasi lain dan Anda memiliki izin menulis: " + ioEx.Message, "Kesalahan File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Log 'ioEx'
                }
                catch (DocumentException docEx)
                {
                    MessageBox.Show("Terjadi masalah saat membuat dokumen PDF: " + docEx.Message, "Kesalahan PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Log 'docEx'
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan tak terduga saat mencetak PDF: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Log 'ex'
                }
            }
        }

        // Tambahkan metode helper ini untuk mengatur lebar kolom PDF agar sesuai dengan DataGridView
        private float[] GetColumnWidths(DataGridView dgv)
        {
            float[] widths = new float[dgv.Columns.GetColumnCount(DataGridViewElementStates.Visible)];
            int visibleIndex = 0;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (dgv.Columns[i].Visible)
                {
                    // Konversi lebar piksel DataGridView ke unit iTextSharp (relatif)
                    // Ini adalah estimasi, mungkin perlu penyesuaian manual jika hasilnya tidak ideal
                    widths[visibleIndex] = dgv.Columns[i].Width;
                    visibleIndex++;
                }
            }
            return widths;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            previousForm.Show();
            this.Close();
        }
    }
}
