using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    public partial class FormHasilKuis : Form
    {
        //Koneksi kn = new Koneksi();
        //string strKonek = "";

        public int CertificateID { get; set; }
        public int KursusID { get; set; }
        public string KursusJudul { get; set; }
        public int UserID { get; set; }
        public int NilaiAkhir { get; set; }
        public string CertificateIDuniq { get; set; }
        public string NamaLengkapUser { get; set; }

        public FormHasilKuis()
        {
            InitializeComponent();
            //strKonek = kn.connectionString();
        }

        private void FormHasilKuis_Load(object sender, EventArgs e)
        {

            lblJudul.Text = "Hasil Kuis: " + KursusJudul;
            lblNilai.Text = $"Nilai Akhir Anda: {NilaiAkhir}";

            bool lulus = NilaiAkhir >= 80;
            lblStatus.Text = lulus ? "Status: LULUS ✅" : "Status: GAGAL ❌";
            lblStatus.ForeColor = lulus ? Color.DarkGreen : Color.Maroon;
            this.BackColor = lulus ? Color.Honeydew : Color.MistyRose;
            btnLihatSertifikat.Visible = lulus && (this.CertificateID > 0);
            lblPesanBcktoMnu.Visible = !lulus;
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            bool lulus = NilaiAkhir >= 80;

            FormDashboard dashboard = new FormDashboard
            {
                UserID = this.UserID,
                FullName = this.NamaLengkapUser,
                WindowState = FormWindowState.Maximized
            };

            if (lulus)
            {
                MessageBox.Show("Selamat! Sertifikat Anda tersedia di menu Sertifikat.", "Lulus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dashboard.Show();
            this.Close();
        }

        private void btnLihatSertifikat_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(CertificateIDuniq)) tidak lagi relevan jika mengandalkan CertificateID
            //{
            //    MessageBox.Show("ID Sertifikat tidak ditemukan.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            // Ambil CertificateID sebenarnya berdasarkan UserID & KursusID jika tidak disimpan langsung
            //int certificateId = GetCertificateIDByUserKursus(UserID, KursusID); Ini sekarang tidak lagi diperlukan karena CertificateID sudah diteruskan
            int certificateId = this.CertificateID;

            if (certificateId == 0)
            {
                MessageBox.Show("Sertifikat tidak ditemukan di database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FormCertificateViewer viewer = new FormCertificateViewer(certificateId)
            {
                UserID = this.UserID,
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Maximized
            };
            this.Hide();
            viewer.Show();
        }
    }
}
