using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    public partial class FormHasilKuis : Form
    {
        public int KursusID { get; set; }
        public string KursusJudul { get; set; }
        public int UserID { get; set; }
        public int NilaiAkhir { get; set; }
        public string CertificateIDuniq { get; set; }

        private string NamaLengkapUser;

        public FormHasilKuis()
        {
            InitializeComponent();
        }

        private void FormHasilKuis_Load(object sender, EventArgs e)
        {
            NamaLengkapUser = GetUserName(UserID);

            lblJudul.Text = "Hasil Kuis: " + KursusJudul;
            lblNilai.Text = $"Nilai Akhir Anda: {NilaiAkhir}";

            bool lulus = NilaiAkhir >= 80;
            lblStatus.Text = lulus ? "Status: LULUS ✅" : "Status: GAGAL ❌";
            lblStatus.ForeColor = lulus ? Color.DarkGreen : Color.Maroon;
            this.BackColor = lulus ? Color.Honeydew : Color.MistyRose;

            btnLihatSertifikat.Visible = lulus && !string.IsNullOrEmpty(CertificateIDuniq);
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
            if (string.IsNullOrEmpty(CertificateIDuniq))
            {
                MessageBox.Show("ID Sertifikat tidak ditemukan.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ambil CertificateID sebenarnya berdasarkan UserID & KursusID jika tidak disimpan langsung
            int certificateId = GetCertificateIDByUserKursus(UserID, KursusID);
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

        private int GetCertificateIDByUserKursus(int userId, int kursusId)
        {
            int result = 0;
            string connString = "Data Source=LAPTOPGW1;Initial Catalog=CertificateCourseDB;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 CertificateID FROM Certificate0 WHERE UserID = @uid AND KursusID = @kid ORDER BY TanggalDapat DESC", conn);
                cmd.Parameters.AddWithValue("@uid", userId);
                cmd.Parameters.AddWithValue("@kid", kursusId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result = Convert.ToInt32(reader["CertificateID"]);
                }
            }
            return result;
        }

        private string GetUserName(int userId)
        {
            string result = "";
            string connString = "Data Source=LAPTOPGW1;Initial Catalog=CertificateCourseDB;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT FullName FROM Users WHERE UserID = @id", conn);
                cmd.Parameters.AddWithValue("@id", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result = reader["FullName"].ToString();
                }
            }
            return result;
        }
    }
}
