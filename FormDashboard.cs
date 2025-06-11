using System;
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
            FormKelolaMateri formMateri = new FormKelolaMateri
            {
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Maximized
            };

            this.Hide();
            formMateri.Show();
        }

        private void btnMengerjakanKuis_Click(object sender, EventArgs e)
        {
            FormKuis formKuis = new FormKuis
            {
                UserID = this.UserID,
                KursusID = 1,
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Maximized
            };

            this.Hide();
            formKuis.Show();
        }

        private void btnMengelolaKuis_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fitur Mengelola Kuis masih dalam pengembangan.",
                "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSertifikat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fitur Sertifikat belum tersedia.",
                "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
