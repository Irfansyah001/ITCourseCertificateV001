using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            // Menempatkan panel login di tengah form
            panelContainer.Anchor = AnchorStyles.None;
            panelContainer.Left = (this.ClientSize.Width - panelContainer.Width) / 2;
            panelContainer.Top = (this.ClientSize.Height - panelContainer.Height) / 2;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validasi input kosong
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username dan Password wajib diisi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (username.Length > 20)
            {
                MessageBox.Show("Username tidak boleh lebih dari 20 karakter.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connString = "Data Source=localhost;Initial Catalog=CertificateCourseDB;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string fullName = reader["FullName"].ToString();
                    int userId = Convert.ToInt32(reader["UserID"]);

                    MessageBox.Show("Login berhasil. Selamat datang, " + fullName, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // GANTI KE FORM DASHBOARD
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

        private void lblTitle_Click(object sender, EventArgs e)
        {
            // opsional
        }

        private void pictureBoxLogo_Click(object sender, EventArgs e)
        {
            // opsional
        }
    }
}
