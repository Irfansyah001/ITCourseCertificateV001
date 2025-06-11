using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    public partial class FormRiwayatKuis : Form
    {
        public int UserID { get; set; }

        public FormRiwayatKuis()
        {
            InitializeComponent();
        }

        private void FormRiwayatKuis_Load(object sender, EventArgs e)
        {
            LoadRiwayatKuis();
        }

        private void LoadRiwayatKuis()
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=LAPTOPGW1;Initial Catalog=CertificateCourseDB;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetRiwayatKuisLengkapByUserID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }

            dgvRiwayatKuis.DataSource = dt;

            // Tambahkan tombol "Ulangi Kuis" hanya jika belum ada
            if (!dgvRiwayatKuis.Columns.Contains("Aksi"))
            {
                DataGridViewButtonColumn btnUlangi = new DataGridViewButtonColumn
                {
                    HeaderText = "Aksi",
                    Name = "Aksi",
                    Text = "Ulangi Kuis",
                    UseColumnTextForButtonValue = true
                };
                dgvRiwayatKuis.Columns.Add(btnUlangi);
            }

            // Format kolom tanggal
            if (dgvRiwayatKuis.Columns.Contains("TanggalDikerjakan"))
            {
                dgvRiwayatKuis.Columns["TanggalDikerjakan"].DefaultCellStyle.Format = "dd MMM yyyy HH:mm";
            }

            // Atur tampilan tabel agar rapi
            dgvRiwayatKuis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRiwayatKuis.RowHeadersVisible = false;
            dgvRiwayatKuis.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRiwayatKuis.ReadOnly = true;
        }

        private void dgvRiwayatKuis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvRiwayatKuis.Columns[e.ColumnIndex].Name == "Aksi")
            {
                int kursusID = Convert.ToInt32(dgvRiwayatKuis.Rows[e.RowIndex].Cells["KursusID"].Value);
                string namaKuis = dgvRiwayatKuis.Rows[e.RowIndex].Cells["NamaKuis"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    $"Yakin ingin mengulangi kuis \"{namaKuis}\"?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    FormTampilanKerjaKuis ulangKuis = new FormTampilanKerjaKuis
                    {
                        UserID = this.UserID,
                        StartPosition = FormStartPosition.CenterScreen,
                        WindowState = FormWindowState.Maximized
                    };

                    this.Hide();
                    ulangKuis.Show();
                }
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            FormDashboard dashboard = new FormDashboard
            {
                UserID = this.UserID,
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Maximized
            };

            this.Hide();
            dashboard.Show();
        }
    }
}
