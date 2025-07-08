using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    public partial class FormRiwayatKuis : Form
    {
        //Koneksi kn = new Koneksi();
        //string strKonek = "";

        public int UserID { get; set; }

        public FormRiwayatKuis()
        {
            InitializeComponent();
            //strKonek = kn.connectionString();
        }

        private void FormRiwayatKuis_Load(object sender, EventArgs e)
        {
            LoadRiwayatKuis();
        }

        //string connectionString = "Data Source=LAPTOPGW1;Initial Catalog=CertificateCourseDB;Integrated Security=True";
        private void LoadRiwayatKuis()
        {
            DataTable dt = new DataTable();
            //string connString = ITCourseCertificateV001.Properties.Settings.Default.CertificateCourseDBConnectionString;

            string currentConnString = Koneksi.GetConnectionString();
            if (string.IsNullOrEmpty(currentConnString))
            {
                MessageBox.Show("String koneksi database tidak valid. Mohon periksa pengaturan IP.", "Kesalahan Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(currentConnString))
                {
                    conn.Open();
                    // Stored Procedure sp_GetRiwayatKuisLengkapByUserID sudah kita beri TRY-CATCH di SQL Server
                    // Jadi di sini kita menangani error dari sisi koneksi atau parameter.
                    using (SqlCommand cmd = new SqlCommand("sp_GetRiwayatKuisLengkapByUserID", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", UserID);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
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
                        UseColumnTextForButtonValue = true,
                        FlatStyle = FlatStyle.Flat, // Tambahkan style agar terlihat lebih modern
                        DefaultCellStyle = {
                    BackColor = System.Drawing.Color.LightBlue, // Warna latar belakang tombol
                    ForeColor = System.Drawing.Color.Black // Warna teks tombol
                }
                    };
                    dgvRiwayatKuis.Columns.Add(btnUlangi);
                }

                // Format kolom tanggal
                if (dgvRiwayatKuis.Columns.Contains("TanggalDikerjakan"))
                {
                    dgvRiwayatKuis.Columns["TanggalDikerjakan"].DefaultCellStyle.Format = "dd MMM yyyy HH:mm"; // Perbaiki format string
                }

                // Atur tampilan tabel agar rapi
                dgvRiwayatKuis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvRiwayatKuis.RowHeadersVisible = false;
                dgvRiwayatKuis.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvRiwayatKuis.ReadOnly = true;

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Terjadi masalah database saat memuat riwayat kuis: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga saat memuat riwayat kuis: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvRiwayatKuis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            // Tambahkan try-catch untuk penanganan error pada pengambilan nilai sel
            try
            {
                if (dgvRiwayatKuis.Columns[e.ColumnIndex].Name == "Aksi")
                {
                    // Gunakan operator null-conditional dan null-coalescing untuk keamanan
                    int kursusID = Convert.ToInt32(dgvRiwayatKuis.Rows[e.RowIndex].Cells["KursusID"].Value ?? 0); // Default ke 0 jika null
                    string namaKuis = dgvRiwayatKuis.Rows[e.RowIndex].Cells["NamaKuis"].Value?.ToString() ?? "Kuis Tidak Dikenal";

                    // Perlu mendapatkan FullName pengguna untuk diteruskan ke FormTampilanKerjaKuis
                    // Ini bisa dilakukan dengan memanggil GetUserName() atau menyimpannya di properti global
                    // Untuk saat ini, kita biarkan saja UserID yang diteruskan, dan FormTampilanKerjaKuis akan mengambil nama
                    // atau kita akan memodifikasi FormTampilanKerjaKuis untuk mengambil FullName dari FormDashboard
                    // yang akan meneruskannya ke FormHasilKuis.

                    DialogResult result = MessageBox.Show(
                        $"Yakin ingin mengulangi kuis \"{namaKuis}\"?",
                        "Konfirmasi Mulai Ulang Kuis", // Ubah judul konfirmasi agar lebih jelas
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        FormTampilanKerjaKuis ulangKuis = new FormTampilanKerjaKuis
                        {
                            UserID = this.UserID,
                            // Kita akan mengatur properti FullName di FormTampilanKerjaKuis nanti
                            // jika diperlukan untuk ditampilkan di sana sebelum kuis dimulai.
                            StartPosition = FormStartPosition.CenterScreen,
                            WindowState = FormWindowState.Maximized
                        };

                        this.Hide();
                        ulangKuis.Show();
                    }
                }
            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show("Kesalahan konversi data saat membaca riwayat kuis: " + ex.Message, "Kesalahan Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log 'ex'
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga saat memproses klik riwayat kuis: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log 'ex'
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
