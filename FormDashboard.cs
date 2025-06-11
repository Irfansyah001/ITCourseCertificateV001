using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

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
            // Tampilkan informasi pengguna
            lblWelcome.Text = $"Selamat datang, {FullName}";
            lblUserID.Text = $"ID Anda: {UserID}";

            // Pastikan tombol logout tampil paling atas
            btnLogout.BringToFront();

            // Atur posisi awal tombol logout
            FormDashboard_Resize(sender, e);
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
            string connStr = @"Data Source=LAPTOPGW1;Initial Catalog=CertificateCourseDB;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT TOP 1 CertificateID FROM Certificate0 WHERE UserID = @UserID ORDER BY TanggalDapat DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);

                conn.Open();
                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    certId = Convert.ToInt32(result);
                }
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
