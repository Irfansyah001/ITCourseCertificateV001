using System;
using System.Drawing;
using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    public partial class FormMenu : Form
    {
        public string LoggedInName { get; set; }
        public int LoggedInUserID { get; set; }

        public FormMenu()
        {
            InitializeComponent();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Selamat datang, " + LoggedInName;

            AddKursusButton(1, "Dasar Pemrograman");
            AddKursusButton(2, "Jaringan Komputer");
            AddKursusButton(3, "UI/UX Design");
            AddKursusButton(4, "Database");
        }

        private void AddKursusButton(int id, string title)
        {
            Button btn = new Button();
            btn.Text = title;
            btn.Tag = id;
            btn.Width = 240;
            btn.Height = 55;
            btn.Margin = new Padding(10);
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn.BackColor = Color.SteelBlue;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Cursor = Cursors.Hand;

            btn.Click += KursusButton_Click;

            flowButtons.Controls.Add(btn);
        }

        private void KursusButton_Click(object sender, EventArgs e)
        {
            Button clicked = sender as Button;
            int kursusID = (int)clicked.Tag;
            string judulKursus = clicked.Text;

            FormMateri materiForm = new FormMateri
            {
                KursusID = kursusID,
                KursusJudul = judulKursus,
                UserID = LoggedInUserID,
                WindowState = FormWindowState.Maximized
            };

            this.Hide();
            materiForm.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Anda yakin ingin kembali ke halaman login?",
                "Konfirmasi Kembali",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

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

        private void panelTOP_Paint(object sender, PaintEventArgs e)
        {
            // Kosongkan atau bisa dihapus jika tidak digunakan
        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {
            // Tidak perlu aksi khusus untuk label
        }

        private void tableLayout_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
