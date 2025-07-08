using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    public partial class FormDashboard : Form
    {
        //Koneksi kn = new Koneksi();
        //string strKonek = "";
        //private string strKonek;
        public string FullName { get; set; }
        public int UserID { get; set; }

        public FormDashboard()
        {
            InitializeComponent();
            // strKonek = kn.connectionString();
            // strKonek = Koneksi.GetConnectionString();
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

        private string GetUserName(int userId)
        {
            string result = "Pengguna Tidak Dikenal"; // Nilai default yang lebih deskriptif

            // Tambahkan validasi eksplisit untuk userId sebelum mencoba koneksi DB
            if (userId <= 0)
            {
                return result; // Langsung kembalikan default jika UserID tidak valid
            }

            string currentConnString = Koneksi.GetConnectionString();
            if (string.IsNullOrEmpty(currentConnString))
            {
                MessageBox.Show("String koneksi database tidak valid. Mohon periksa pengaturan IP.", "Kesalahan Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return result; // Kembalikan default jika string koneksi tidak valid
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(currentConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetFullNameByUserId", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", userId); // Pastikan parameter ini sesuai dengan nama parameter di SP Anda (@UserID, bukan @id)

                        object dbResult = cmd.ExecuteScalar();

                        if (dbResult != null && dbResult != DBNull.Value)
                        {
                            result = dbResult.ToString();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Gagal mengambil nama pengguna dari database: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log 'ex' di sini
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga saat mengambil nama pengguna: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log 'ex' di sini
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

            string currentConnString = Koneksi.GetConnectionString();
            if (string.IsNullOrEmpty(currentConnString))
            {
                MessageBox.Show("String koneksi database tidak valid. Mohon periksa pengaturan IP.", "Kesalahan Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0; // Kembalikan default jika string koneksi tidak valid
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(currentConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetLatestCertificateIdForUser", conn)) // Panggil SP baru
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
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
