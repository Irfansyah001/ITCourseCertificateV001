using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace ITCourseCertificateV001
{
    public partial class FormDashboard : Form
    {
        public string FullName { get; set; }
        public int UserID { get; set; }

        public FormDashboard()
        {
            InitializeComponent();

            // Pastikan form dalam mode fullscreen dan tidak bisa di-minimize
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = true;
            this.MinimizeBox = false;
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            // Jika FullName belum diatur (misalnya, saat kembali dari form lain), ambil dari DB
            // Pastikan UserID juga valid
            if (string.IsNullOrEmpty(this.FullName) && this.UserID > 0)
            {
                // Panggil fungsi helper untuk mendapatkan nama
                this.FullName = GetUserName(this.UserID);
            }
            // Tampilkan informasi pengguna
            lblWelcome.Text = $"Selamat datang, {FullName}";
            lblUserID.Text = $"ID Anda: {UserID}";

            // Pastikan tombol logout tampil paling atas
            btnLogout.BringToFront();

            // Atur posisi awal tombol logout
            FormDashboard_Resize(sender, e);
        }

        // Tambahkan fungsi helper ini di dalam kelas FormDashboard
        // Fungsi ini akan mengambil nama pengguna dari database
        private string GetUserName(int userId)
        {
            string result = "Pengguna"; // Nilai default jika nama tidak ditemukan atau error
            string connString = ITCourseCertificateV001.Properties.Settings.Default.CertificateCourseDBConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT FullName FROM Users WHERE UserID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", userId);
                        object dbResult = cmd.ExecuteScalar(); // Gunakan ExecuteScalar karena hanya mengambil satu nilai

                        if (dbResult != null)
                        {
                            result = dbResult.ToString();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Gagal mengambil nama pengguna dari database: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Pertimbangkan untuk melakukan logging error di sini
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga saat mengambil nama pengguna: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Pertimbangkan untuk melakukan logging error di sini
            }
            return result;
        }

        private void btnMengelolaMateri_Click(object sender, EventArgs e)
        {
            FormKelolaMateri kelola = new FormKelolaMateri(this.UserID, this);
            this.Hide();
            kelola.Show();
        }

        private void btnMengerjakanKuis_Click(object sender, EventArgs e)
        {
            FormTampilanKerjaKuis kerjaKuis = new FormTampilanKerjaKuis
            {
                UserID = this.UserID,
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Maximized
            };

            this.Hide();
            kerjaKuis.Show();
        }

        private void btnMengelolaKuis_Click(object sender, EventArgs e)
        {
            FormRiwayatKuis riwayat = new FormRiwayatKuis
            {
                UserID = this.UserID,
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Maximized
            };

            this.Hide();
            riwayat.Show();
        }

        private void btnSertifikat_Click(object sender, EventArgs e)
        {
            int certificateId = GetCertificateIdForUser(UserID);

            if (certificateId > 0)
            {
                FormCertificateViewer viewer = new FormCertificateViewer(certificateId)
                {
                    UserID = this.UserID,
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };

                this.Hide();
                viewer.Show();
            }
            else
            {
                MessageBox.Show("Sertifikat belum tersedia untuk akun Anda. Silakan selesaikan kuis terlebih dahulu.",
                                "Sertifikat Tidak Ditemukan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private int GetCertificateIdForUser(int userId)
        {
            int certId = 0;
            // Ambil connection string dari App.config
            string connString = ITCourseCertificateV001.Properties.Settings.Default.CertificateCourseDBConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT TOP 1 CertificateID FROM Certificate0 WHERE UserID = @UserID ORDER BY TanggalDapat DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        var result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            certId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Tangani error database, misalnya koneksi gagal atau query bermasalah
                MessageBox.Show("Terjadi masalah koneksi database saat mengambil sertifikat: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Anda bisa log ex di sini
            }
            catch (Exception ex)
            {
                // Tangani error umum lainnya
                MessageBox.Show("Terjadi kesalahan tak terduga saat mengambil sertifikat: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Anda bisa log ex di sini
            }

            return certId;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Yakin ingin logout?",
                "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                FormLogin login = new FormLogin
                {
                    StartPosition = FormStartPosition.CenterScreen
                };
                login.Show();
                this.Close();
            }
        }

        private void FormDashboard_Resize(object sender, EventArgs e)
        {
            // Reposisi tombol logout di tengah bawah saat resize
            btnLogout.Location = new Point(
                (panelMenu.Width - btnLogout.Width) / 2,
                panelMenu.Height - btnLogout.Height - 30
            );
        }
    }
}
