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
                    //string query = "SELECT TOP 1 CertificateID FROM Certificate0 WHERE UserID = @uid AND KursusID = @kid ORDER BY TanggalDapat DESC";
                    // Menggunakan Stored Procedure untuk mengambil CertificateID
                    using (SqlCommand cmd = new SqlCommand("GetLatestCertificateIdForUser", conn)) // Tambahkan 'using' untuk SqlCommand
                    {
                        cmd.CommandType = CommandType.StoredProcedure; // Penting: Menentukan bahwa ini adalah Stored Procedure
                        cmd.Parameters.AddWithValue("@UserID", userId); // @UserID sesuai dengan SP
                        // Parameter @kid tidak diperlukan di SP GetLatestCertificateIdForUser
                        // Jika SP GetLatestCertificateIdForUser hanya berdasarkan UserID
                        // Namun, jika Anda memiliki SP yang spesifik berdasarkan UserID dan KursusID, gunakan itu.
                        // Untuk saat ini, kita ikuti SP GetLatestCertificateIdForUser yang hanya butuh UserID.

                        object dbResult = cmd.ExecuteScalar();

                        if (dbResult != null && dbResult != DBNull.Value)
                        {
                            result = Convert.ToInt32(dbResult);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Terjadi masalah database saat mengambil ID Sertifikat: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log 'ex' di sini untuk debugging lebih lanjut
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga saat mengambil ID Sertifikat: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log 'ex' di sini
            }
            return result;
        }

        //private string GetUserName(int userId)
        //{
        //    string result = "Pengguna Tidak Dikenal"; // Nilai default yang lebih deskriptif
        //    //string connString = ITCourseCertificateV001.Properties.Settings.Default.CertificateCourseDBConnectionString;

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(strKonek))
        //        {
        //            conn.Open();
        //            string query = "SELECT FullName FROM Users WHERE UserID = @id";
        //            using (SqlCommand cmd = new SqlCommand(query, conn)) // Tambahkan 'using' untuk SqlCommand
        //            {
        //                cmd.Parameters.AddWithValue("@id", userId);
        //                object dbResult = cmd.ExecuteScalar(); // Gunakan ExecuteScalar

        //                if (dbResult != null && dbResult != DBNull.Value)
        //                {
        //                    result = dbResult.ToString();
        //                }
        //            } // end using SqlCommand
        //        } // end using SqlConnection
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show("Terjadi masalah database saat mengambil nama pengguna: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        // Log 'ex' di sini
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Terjadi kesalahan tak terduga saat mengambil nama pengguna: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        // Log 'ex' di sini
        //    }
        //    return result;
    }
}
