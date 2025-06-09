using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    public partial class FormMateri : Form
    {
        public int KursusID { get; set; }
        public string KursusJudul { get; set; }
        public int UserID { get; set; }

        public FormMateri()
        {
            InitializeComponent();
            this.Resize += FormMateri_Resize;
        }

        private void FormMateri_Load(object sender, EventArgs e)
        {
            lblKursus.Text = KursusJudul;
            lblKursus.TextAlign = ContentAlignment.MiddleCenter;
            lblKursus.Font = new Font("Segoe UI", 16, FontStyle.Bold);

            dgvMateri.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMateri.ReadOnly = true;

            txtDeskripsi.ReadOnly = true;
            txtDeskripsi.Multiline = true;
            txtDeskripsi.ScrollBars = ScrollBars.Vertical;

            FormMateri_Resize(this, EventArgs.Empty);
            LoadMateri();
        }

        private void FormMateri_Resize(object sender, EventArgs e)
        {
            int formPadding = 20;

            dgvMateri.Width = this.ClientSize.Width - (2 * formPadding);
            dgvMateri.Left = formPadding;

            txtDeskripsi.Width = dgvMateri.Width;
            txtDeskripsi.Left = dgvMateri.Left;
            txtDeskripsi.Top = dgvMateri.Bottom + 10;

            btnDeleteMateri.Left = txtDeskripsi.Left;
            btnUpdateMateri.Left = (this.ClientSize.Width - btnUpdateMateri.Width) / 2;
            btnKerjakanKuis.Left = txtDeskripsi.Right - btnKerjakanKuis.Width;

            int buttonRowTop = txtDeskripsi.Bottom + 15;
            btnDeleteMateri.Top = btnUpdateMateri.Top = btnKerjakanKuis.Top = buttonRowTop;

            // ⬇ Tambahkan ini untuk posisikan tombol Back di tengah bawah
            btnBack.Left = (this.ClientSize.Width - btnBack.Width) / 2;
            btnBack.Top = this.ClientSize.Height - btnBack.Height - 20; // 20 px dari bawah
        }


        private void LoadMateri()
        {
            string connString = "Data Source=localhost;Initial Catalog=CertificateCourseDB;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT MateriID, JudulMateri, LinkVideo, DurasiMenit FROM Materi WHERE KursusID = @id ORDER BY Urutan ASC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@id", KursusID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvMateri.DataSource = dt;

                dgvMateri.Columns["MateriID"].Visible = false;
                dgvMateri.Columns["JudulMateri"].HeaderText = "Judul Materi";
                dgvMateri.Columns["LinkVideo"].HeaderText = "Link Video";
                dgvMateri.Columns["DurasiMenit"].HeaderText = "Durasi (menit)";
            }
        }

        private void dgvMateri_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvMateri.Columns[e.ColumnIndex].Name == "LinkVideo")
            {
                string link = dgvMateri.Rows[e.RowIndex].Cells["LinkVideo"].Value.ToString();
                Process.Start(new ProcessStartInfo { FileName = link, UseShellExecute = true });
            }

            if (e.RowIndex >= 0)
            {
                string judul = dgvMateri.Rows[e.RowIndex].Cells["JudulMateri"].Value.ToString();
                txtDeskripsi.Text = $"Deskripsi Materi: {judul}\r\n\nLink tersedia, klik dua kali pada kolom link untuk membuka video.";
            }
        }

        private void btnDeleteMateri_Click(object sender, EventArgs e)
        {
            if (dgvMateri.CurrentRow == null) return;

            int materiId = Convert.ToInt32(dgvMateri.CurrentRow.Cells["MateriID"].Value);
            string judul = dgvMateri.CurrentRow.Cells["JudulMateri"].Value.ToString();

            DialogResult confirm = MessageBox.Show($"Yakin ingin menghapus materi \"{judul}\"?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=CertificateCourseDB;Integrated Security=True"))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Materi WHERE MateriID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", materiId);
                    cmd.ExecuteNonQuery();
                }

                LoadMateri();
            }
        }

        private void btnUpdateMateri_Click(object sender, EventArgs e)
        {
            if (dgvMateri.CurrentRow == null)
            {
                MessageBox.Show("Pilih baris materi yang ingin diperbarui.");
                return;
            }

            int materiId = Convert.ToInt32(dgvMateri.CurrentRow.Cells["MateriID"].Value);
            string judulLama = dgvMateri.CurrentRow.Cells["JudulMateri"].Value.ToString();
            string linkLama = dgvMateri.CurrentRow.Cells["LinkVideo"].Value.ToString();
            int durasiLama = Convert.ToInt32(dgvMateri.CurrentRow.Cells["DurasiMenit"].Value);

            DialogResult confirm = MessageBox.Show(
                $"Apakah Anda yakin ingin mengubah materi \"{judulLama}\"?",
                "Konfirmasi Update",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirm != DialogResult.Yes) return;

            string judulBaru = Microsoft.VisualBasic.Interaction.InputBox("Judul baru:", "Update", judulLama);
            if (string.IsNullOrWhiteSpace(judulBaru))
            {
                MessageBox.Show("Judul tidak boleh kosong.");
                return;
            }
            if (judulBaru.Length > 100)
            {
                MessageBox.Show("Judul tidak boleh lebih dari 100 karakter.");
                return;
            }

            string linkBaru = Microsoft.VisualBasic.Interaction.InputBox("Link video baru:", "Update", linkLama);
            if (string.IsNullOrWhiteSpace(linkBaru))
            {
                MessageBox.Show("Link tidak boleh kosong.");
                return;
            }
            if (!linkBaru.Contains("youtube.com") && !linkBaru.Contains("youtu.be"))
            {
                MessageBox.Show("Link harus berasal dari YouTube.");
                return;
            }

            string durasiInput = Microsoft.VisualBasic.Interaction.InputBox("Durasi (menit):", "Update", durasiLama.ToString());
            if (!int.TryParse(durasiInput, out int durasiBaru) || durasiBaru <= 0)
            {
                MessageBox.Show("Durasi harus berupa angka lebih dari 0.");
                return;
            }
            if (durasiBaru > 300)
            {
                MessageBox.Show("Durasi maksimal adalah 300 menit (5 jam).");
                return;
            }

            // Validasi tidak ada perubahan
            if (judulBaru == judulLama && linkBaru == linkLama && durasiBaru == durasiLama)
            {
                MessageBox.Show("Tidak ada perubahan yang dilakukan.");
                return;
            }

            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=CertificateCourseDB;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Materi SET JudulMateri=@judul, LinkVideo=@link, DurasiMenit=@durasi WHERE MateriID=@id", conn);
                cmd.Parameters.AddWithValue("@judul", judulBaru);
                cmd.Parameters.AddWithValue("@link", linkBaru);
                cmd.Parameters.AddWithValue("@durasi", durasiBaru);
                cmd.Parameters.AddWithValue("@id", materiId);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Materi berhasil diperbarui.");
            LoadMateri();
        }

        private void btnKerjakanKuis_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Sudah yakin ingin mengerjakan kuis?\nDurasi mengerjakan: 1 menit.",
                "Konfirmasi Kerjakan Kuis",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            FormKuis kuis = new FormKuis
            {
                KursusID = this.KursusID,
                KursusJudul = this.KursusJudul,
                UserID = this.UserID,
                WindowState = FormWindowState.Maximized
            };

            this.Hide();
            kuis.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Anda yakin ingin kembali ke halaman menu?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                FormMenu menu = new FormMenu
                {
                    LoggedInName = this.KursusJudul, // atau simpan sebelumnya
                    LoggedInUserID = this.UserID
                };
                menu.WindowState = FormWindowState.Maximized;
                menu.StartPosition = FormStartPosition.CenterScreen;
                menu.Show();
                this.Close();
            }
        }

    }
}
