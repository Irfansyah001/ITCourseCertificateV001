using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    public partial class FormLogin : Form
    {

        // Koneksi kn = new Koneksi(); // Koneksi ke database menggunakan class Koneksi
        // string strKonek = ""; // string koneksi ke database
        private string strKonek;

        public FormLogin()
        {
            InitializeComponent();
            strKonek = Koneksi.GetConnectionString();
            this.WindowState = FormWindowState.Maximized;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = false; 
            this.FormBorderStyle = FormBorderStyle.None;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(20, 40, 30);
        }


        private void FormLogin_Load(object sender, EventArgs e)
        {
            CenterLoginPanel();
            this.Resize += (s, ev) => CenterLoginPanel();
        }

        private void CenterLoginPanel()
        {
            panelContainer.Left = (this.ClientSize.Width - panelContainer.Width) / 2;
            panelContainer.Top = (this.ClientSize.Height - panelContainer.Height) / 2;
        }

        // string connString = "Data Source=localhost;Initial Catalog=CertificateCourseDB;Integrated Security=True";
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username dan Password wajib diisi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (username.Length > 20) // Periksa ini, apakah panjang username di DB Anda <= 20?
            {
                MessageBox.Show("Username tidak boleh lebih dari 20 karakter.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            strKonek = Koneksi.GetConnectionString(); // Pastikan selalu mendapatkan string koneksi terbaru dari cache/file

            if (string.IsNullOrEmpty(strKonek)) // Jika string koneksi kosong (misal gagal dari Koneksi.cs)
            {
                // Pesan error sudah ditampilkan oleh Koneksi.cs, jadi cukup return di sini.
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(strKonek))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("AuthenticateUser", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure; // Penting: Menentukan bahwa ini adalah Stored Procedure
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password); // Password masih teks biasa sesuai diskusi kita

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                string fullName = reader["FullName"].ToString();
                                int userId = Convert.ToInt32(reader["UserID"]);

                                MessageBox.Show("Login berhasil. Selamat datang, " + fullName, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                FormDashboard dashboard = new FormDashboard
                                {
                                    FullName = fullName,
                                    UserID = userId,
                                    StartPosition = FormStartPosition.CenterScreen,
                                    WindowState = FormWindowState.Maximized
                                };

                                this.Hide();
                                dashboard.Show();
                            }
                            else
                            {
                                MessageBox.Show("Username atau password salah.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }

            catch (SqlException ex) // Tangkap exception spesifik untuk masalah SQL Server
            {
                MessageBox.Show("Terjadi masalah koneksi database atau query: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Anda bisa log 'ex' di sini untuk debugging yang lebih detail
                // Console.WriteLine(ex.ToString()); // Untuk debug di output window
            }
            catch (Exception ex) // Tangkap exception umum lainnya
            {
                MessageBox.Show("Terjadi kesalahan tak terduga: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Console.WriteLine(ex.ToString()); // Untuk debug di output window
            }
        }

        private void btnPengaturanIP_Click(object sender, EventArgs e)
        {
            FormPengaturanIP form = new FormPengaturanIP();
            form.ShowDialog();
            Koneksi.RefreshConnectionString();
            MessageBox.Show("Pengaturan IP telah diperbarui. Silahkan untuk mencoba login.", "IP Diperbarui", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Apakah Anda yakin ingin keluar dari aplikasi?",
                "Konfirmasi Keluar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

    }
}
