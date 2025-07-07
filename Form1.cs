using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace ITCourseCertificateV001
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();


            this.WindowState = FormWindowState.Maximized;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(20, 40, 30);
        }


        private void FormLogin_Load(object sender, EventArgs e)
        {
            CenterLoginPanel();
            this.Resize += (s, ev) => CenterLoginPanel(); // Supaya tetap di tengah saat resize
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

            // Ambil connection string dari App.config
            string connString = ITCourseCertificateV001.Properties.Settings.Default.CertificateCourseDBConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT UserID, FullName FROM Users WHERE Username = @username AND Password = @password";
                    using (SqlCommand cmd = new SqlCommand(query, conn)) // Tambahkan 'using' untuk SqlCommand juga
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password); // Peringatan: Password masih teks biasa, akan kita hash nanti!

                        using (SqlDataReader reader = cmd.ExecuteReader()) // Tambahkan 'using' untuk SqlDataReader
                        {
                            if (reader.Read())
                            {
                                string fullName = reader["FullName"].ToString();
                                int userId = Convert.ToInt32(reader["UserID"]);

                                MessageBox.Show("Login berhasil. Selamat datang, " + fullName, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Simpan FullName di tempat yang bisa diakses global (misalnya class Program)
                                // Ini akan kita bahas lebih lanjut nanti, untuk saat ini FormDashboard akan mengambil lagi
                                // ITCourseCertificateV001.Program.LoggedInUserFullName = fullName; // Jika kita membuat properti ini

                                FormDashboard dashboard = new FormDashboard
                                {
                                    FullName = fullName, // Teruskan FullName ke Dashboard
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
                        } // end using SqlDataReader
                    } // end using SqlCommand
                } // end using SqlConnection
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
    }
}
