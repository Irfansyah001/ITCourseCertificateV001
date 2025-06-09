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
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Selamat datang, {FullName}";
        }

        private void btnMengerjakanKuis_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Menuju ke fitur Mengerjakan Kuis.");
            // Bisa arahkan ke FormKuis langsung
        }

        private void btnMengelolaMateri_Click(object sender, EventArgs e)
        {
            FormMenu menu = new FormMenu
            {
                LoggedInName = this.FullName,
                LoggedInUserID = this.UserID,
                WindowState = FormWindowState.Maximized
            };
            this.Hide();
            menu.Show();
        }

        private void btnMengelolaKuis_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fitur Mengelola Kuis masih dalam pengembangan.");
        }

        private void btnSertifikat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fitur Sertifikat belum tersedia.");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Yakin ingin logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.StartPosition = FormStartPosition.CenterScreen;
                login.Show();
                this.Close();
            }
        }
    }
}
