using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    public partial class FormTampilanKerjaKuis : Form
    {
        //Koneksi kn = new Koneksi();
        //string strKonek = "";

        private List<Soal> daftarSoal = new List<Soal>();
        private int indeksSoalSaatIni = 0;
        private int durasiDetik = 60;
        private Timer timer;
        //private string connectionString = @"Data Source=LAPTOPGW1;Initial Catalog=CertificateCourseDB;Integrated Security=True";
        //string connString = ITCourseCertificateV001.Properties.Settings.Default.CertificateCourseDBConnectionString;
        private bool kuisSedangBerlangsung = false;
        private bool sudahTampilkanWaktuHabis = false;

        public int UserID { get; set; } // ID user dari Form Login atau MainMenu
        public int PreviousCertificateID { get; set; }

        public FormTampilanKerjaKuis()
        {
            InitializeComponent();
            //strKonek = kn.connectionString(); // Ambil koneksi dari Koneksi.cs
        }

        private void FormTampilanKerjaKuis_Load(object sender, EventArgs e)
        {
            MuatKursusDariDatabase();
            SetKontrolAwal();
        }

        private void MuatKursusDariDatabase()
        {

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
                    using (SqlCommand cmd = new SqlCommand("SELECT KursusID, JudulKursus FROM DataKursus", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        comboBox1.DisplayMember = "JudulKursus";
                        comboBox1.ValueMember = "KursusID";
                        comboBox1.DataSource = dt;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Terjadi masalah database saat memuat daftar kursus: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log 'ex' di sini
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga saat memuat daftar kursus: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log 'ex' di sini
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            daftarSoal.Clear();
            indeksSoalSaatIni = 0;
        }

        private void btnMulai_Click(object sender, EventArgs e)
        {
            if (kuisSedangBerlangsung)
            {
                MessageBox.Show("Anda sedang dalam kondisi mengerjakan kuis.\nSilakan selesaikan kuis yang sedang berlangsung terlebih dahulu.",
                                "Kuis Sedang Berlangsung", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Silakan pilih kursus terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult konfirmasi = MessageBox.Show(
                "Apakah yakin Anda ingin mengerjakan tugasnya?\nDurasi pengerjaan: 1 menit.",
                "Konfirmasi Mulai Kuis",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (konfirmasi == DialogResult.No)
                return;

            int kursusID = Convert.ToInt32(comboBox1.SelectedValue);
            LoadSoalDariDatabase(kursusID);

            if (daftarSoal.Count == 0)
            {
                MessageBox.Show("Belum ada soal untuk kursus ini.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            indeksSoalSaatIni = 0;
            TampilkanSoal();

            groupBoxJawaban.Enabled = true;
            btnSelanjutnya.Enabled = true;
            btnKembali.Enabled = true;

            durasiDetik = 60;
            sudahTampilkanWaktuHabis = false;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            kuisSedangBerlangsung = true;
        }

        private void LoadSoalDariDatabase(int kursusID)
        {
            daftarSoal.Clear();

            string currentConnString = Koneksi.GetConnectionString();
            if (string.IsNullOrEmpty(currentConnString))
            {
                MessageBox.Show("String koneksi database tidak valid. Mohon periksa pengaturan IP.", "Kesalahan Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                daftarSoal.Clear(); // Pastikan daftar soal kosong jika gagal dimuat
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(currentConnString))
                {
                    conn.Open();
                    string query = "SELECT Pertanyaan, PilihanA, PilihanB, PilihanC, PilihanD, JawabanBenar, KuisID FROM Kuis WHERE KursusID = @kursusID"; // Tambah KuisID di SELECT
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kursusID", kursusID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Pastikan kolom KuisID ada dan diambil
                                int kuisID = reader.GetInt32(reader.GetOrdinal("KuisID"));

                                string jawabanBenarChar = reader.GetString(reader.GetOrdinal("JawabanBenar")).ToUpper();
                                int jawabanBenarIndex = -1;

                                switch (jawabanBenarChar)
                                {
                                    case "A": jawabanBenarIndex = 0; break;
                                    case "B": jawabanBenarIndex = 1; break;
                                    case "C": jawabanBenarIndex = 2; break;
                                    case "D": jawabanBenarIndex = 3; break;
                                }

                                daftarSoal.Add(new Soal
                                {
                                    KuisID = kuisID, // Simpan KuisID di objek Soal
                                    Pertanyaan = reader.GetString(reader.GetOrdinal("Pertanyaan")),
                                    Pilihan = new string[]
                                    {
                                reader.IsDBNull(reader.GetOrdinal("PilihanA")) ? "" : reader.GetString(reader.GetOrdinal("PilihanA")),
                                reader.IsDBNull(reader.GetOrdinal("PilihanB")) ? "" : reader.GetString(reader.GetOrdinal("PilihanB")),
                                reader.IsDBNull(reader.GetOrdinal("PilihanC")) ? "" : reader.GetString(reader.GetOrdinal("PilihanC")),
                                reader.IsDBNull(reader.GetOrdinal("PilihanD")) ? "" : reader.GetString(reader.GetOrdinal("PilihanD"))
                                    },
                                    JawabanBenarIndex = jawabanBenarIndex,
                                    JawabanUserIndex = null
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Terjadi masalah database saat memuat soal kuis: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log 'ex' di sini
                daftarSoal.Clear(); // Pastikan daftar soal kosong jika gagal dimuat
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga saat memuat soal kuis: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log 'ex' di sini
                daftarSoal.Clear();
            }
        }

        private void TampilkanSoal()
        {
            if (indeksSoalSaatIni >= daftarSoal.Count)
            {
                SelesaikanKuis();
                return;
            }

            Soal soal = daftarSoal[indeksSoalSaatIni];
            lblSoalTampil.Text = soal.Pertanyaan;
            radioButtonA.Text = soal.Pilihan[0];
            radioButtonB.Text = soal.Pilihan[1];
            radioButtonC.Text = soal.Pilihan[2];
            radioButtonD.Text = soal.Pilihan[3];

            radioButtonA.Checked = soal.JawabanUserIndex == 0;
            radioButtonB.Checked = soal.JawabanUserIndex == 1;
            radioButtonC.Checked = soal.JawabanUserIndex == 2;
            radioButtonD.Checked = soal.JawabanUserIndex == 3;
        }

        private void btnSelanjutnya_Click(object sender, EventArgs e)
        {
            SimpanJawabanSaatIni();

            if (indeksSoalSaatIni == daftarSoal.Count - 1)
            {
                if (AdaSoalYangBelumDijawab())
                {
                    MessageBox.Show("Masih ada soal yang belum Anda jawab.\nSilakan pastikan semua soal terisi sebelum mengumpulkan jawaban.",
                                    "Jawaban Belum Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult konfirmasi = MessageBox.Show(
                    "Apakah Anda sudah yakin untuk menyimpan semua jawaban?",
                    "Konfirmasi Pengumpulan Jawaban",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (konfirmasi == DialogResult.Yes)
                {
                    SelesaikanKuis();
                }
                return;
            }

            indeksSoalSaatIni++;
            TampilkanSoal();
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            if (indeksSoalSaatIni > 0)
            {
                SimpanJawabanSaatIni();
                indeksSoalSaatIni--;
                TampilkanSoal();
            }
            else
            {
                MessageBox.Show("Ini adalah soal pertama.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            durasiDetik--;
            lblTimerKuis.Text = string.Format("{0:D2}:{1:D2}", durasiDetik / 60, durasiDetik % 60);

            if (durasiDetik <= 0 && !sudahTampilkanWaktuHabis)
            {
                timer.Stop();
                sudahTampilkanWaktuHabis = true;

                if (AdaSoalYangBelumDijawab())
                {
                    MessageBox.Show("Waktu habis! Masih ada soal yang belum dijawab.",
                        "Waktu Habis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                SelesaikanKuis();
            }
        }

        private void SelesaikanKuis()
        {
            timer?.Stop();

            int jawabanBenar = 0;
            foreach (var soal in daftarSoal)
            {
                if (soal.JawabanUserIndex.HasValue && soal.JawabanUserIndex.Value == soal.JawabanBenarIndex)
                {
                    jawabanBenar++;
                }
            }

            int nilaiAkhir = (int)(((double)jawabanBenar / daftarSoal.Count) * 100);
            kuisSedangBerlangsung = false;
            SetKontrolAwal();

            // Validasi comboBox1 (Kursus)
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Kursus tidak valid. Silakan pilih ulang.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int kursusId;
            try
            {
                kursusId = Convert.ToInt32(comboBox1.SelectedValue);
            }
            catch
            {
                MessageBox.Show("Gagal mengonversi ID kursus.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        foreach (var soal in daftarSoal)
                        {
                            // Hanya simpan jawaban jika pengguna benar-benar menjawab soal tersebut
                            if (soal.JawabanUserIndex.HasValue)
                            {
                                using (SqlCommand cmd = new SqlCommand("InsertJawabanUser", conn, trans))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                                    cmd.Parameters.AddWithValue("@KuisID", soal.KuisID); // Ambil KuisID dari objek soal

                                    // Konversi JawabanUserIndex (int) ke CHAR(1) 'A', 'B', 'C', 'D'
                                    char jawabanChar;
                                    switch (soal.JawabanUserIndex.Value)
                                    {
                                        case 0: jawabanChar = 'A'; break;
                                        case 1: jawabanChar = 'B'; break;
                                        case 2: jawabanChar = 'C'; break;
                                        case 3: jawabanChar = 'D'; break;
                                        default: jawabanChar = 'X'; break; // Placeholder jika ada index di luar A-D
                                    }

                                    cmd.Parameters.AddWithValue("@Jawaban", jawabanChar.ToString()); // Convert char to string for parameter
                                    cmd.Parameters.AddWithValue("@IsCorrect", soal.JawabanUserIndex.Value == soal.JawabanBenarIndex);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        trans.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Gagal menyimpan jawaban kuis ke database: " + ex.Message, "Kesalahan Database Penyimpanan Jawaban", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log 'ex' di sini
                // Jika penyimpanan jawaban gagal, ini bisa menjadi masalah serius,
                // pertimbangkan apakah Anda tetap ingin menyimpan sertifikat atau menghentikan alur
                return; // Menghentikan alur jika penyimpanan jawaban gagal
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga saat menyimpan jawaban kuis: " + ex.Message, "Kesalahan Umum Penyimpanan Jawaban", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log 'ex' di sini
                return; // Menghentikan alur jika penyimpanan jawaban gagal
            }

            // Generate CertificateID hanya jika lulus
            string certificateID = null;
            int actualCertificateID = 0;
            if (nilaiAkhir >= 80)
            {
                certificateID = GenerateCertificateID();

                try
                {
                    using (SqlConnection conn = new SqlConnection(currentConnString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SaveCertificate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Teruskan PreviousCertificateID jika ini adalah operasi UPDATE
                            if (this.PreviousCertificateID > 0)
                            {
                                cmd.Parameters.AddWithValue("@CertificateID", this.PreviousCertificateID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CertificateID", DBNull.Value); // Untuk INSERT baru, set NULL
                            }

                            cmd.Parameters.AddWithValue("@UserID", this.UserID);
                            cmd.Parameters.AddWithValue("@KursusID", kursusId);
                            cmd.Parameters.AddWithValue("@Nilai", nilaiAkhir);
                            cmd.Parameters.AddWithValue("@TanggalDapat", DateTime.Now);
                            cmd.Parameters.AddWithValue("@IsPrinted", 0);
                            cmd.Parameters.AddWithValue("@CertificateIDuniq", certificateID);

                            // Tambahkan parameter output untuk mendapatkan kembali ID yang diupdate/dibuat
                            SqlParameter outputCertIdParam = new SqlParameter("@OutputCertificateID", SqlDbType.Int);
                            outputCertIdParam.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(outputCertIdParam);

                            cmd.ExecuteNonQuery();

                            if (outputCertIdParam.Value != DBNull.Value && outputCertIdParam.Value != null)

                                // Ambil CertificateID yang baru/diupdate dari parameter output
                                if (outputCertIdParam.Value != DBNull.Value)
                            {
                                this.PreviousCertificateID = Convert.ToInt32(outputCertIdParam.Value); // Update previousCertID dengan ID yang baru/diupdate
                            }
                            else
                            {
                                // Handle case where output is null (e.g., if SP had a problem)
                                MessageBox.Show("SP SaveCertificate tidak mengembalikan CertificateID. Terjadi masalah.", "Error SP Output", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Gagal menyimpan data sertifikat ke database: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Log 'ex' di sini
                    certificateID = null; // Set null lagi agar tidak ada ilusi sertifikat tersimpan
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan tak terduga saat menyimpan sertifikat: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Log 'ex' di sini
                    certificateID = null;
                }
            }

            // Panggil FormHasilKuis dengan data lengkap
            FormHasilKuis hasilForm = new FormHasilKuis
            {
                UserID = this.UserID,
                KursusID = kursusId,
                KursusJudul = comboBox1.Text,
                NilaiAkhir = nilaiAkhir,
                CertificateIDuniq = certificateID,
                NamaLengkapUser = GetUserNameForHasilKuis(this.UserID),
                CertificateID = actualCertificateID
            };

            hasilForm.WindowState = FormWindowState.Maximized;
            hasilForm.Show();

            this.Close(); // Tutup form kuis saat selesai
        }

        private string GetUserNameForHasilKuis(int userId)
        {
            string result = "Pengguna Tidak Dikenal";
            string currentConnString = Koneksi.GetConnectionString();

            if (string.IsNullOrEmpty(currentConnString))
            {
                MessageBox.Show("String koneksi database tidak valid. Mohon periksa pengaturan IP.", "Kesalahan Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return result;
            }
            if (userId <= 0) return result;

            try
            {
                using (SqlConnection conn = new SqlConnection(currentConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetFullNameByUserId", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        object dbResult = cmd.ExecuteScalar();
                        if (dbResult != null && dbResult != DBNull.Value)
                        {
                            result = dbResult.ToString();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Gagal mengambil nama pengguna untuk hasil kuis: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga saat mengambil nama pengguna untuk hasil kuis: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        private void SetKontrolAwal()
        {
            btnSelanjutnya.Enabled = false;
            btnKembali.Enabled = false;
            groupBoxJawaban.Enabled = false;
            lblSoalTampil.Text = "Klik 'Mulai' untuk mengerjakan kuis.";
            lblTimerKuis.Text = "01:00";
        }

        private void SimpanJawabanSaatIni()
        {
            int jawabanDipilih = -1;
            if (radioButtonA.Checked) jawabanDipilih = 0;
            else if (radioButtonB.Checked) jawabanDipilih = 1;
            else if (radioButtonC.Checked) jawabanDipilih = 2;
            else if (radioButtonD.Checked) jawabanDipilih = 3;

            if (jawabanDipilih != -1)
            {
                daftarSoal[indeksSoalSaatIni].JawabanUserIndex = jawabanDipilih;
            }
        }

        private bool AdaSoalYangBelumDijawab()
        {
            foreach (var soal in daftarSoal)
            {
                if (!soal.JawabanUserIndex.HasValue)
                    return true;
            }
            return false;
        }

        private string GenerateCertificateID()
        {
            return "CERT - " + DateTime.Now.ToString("yyyyMMddHHmmss") + " - " + Guid.NewGuid().ToString().Substring(0, 4);
        }

        private class Soal
        {
            public int KuisID { get; set; }
            public string Pertanyaan { get; set; }
            public string[] Pilihan { get; set; }
            public int JawabanBenarIndex { get; set; }
            public int? JawabanUserIndex { get; set; }
        }

        private void radioButtonA_CheckedChanged(object sender, EventArgs e) { }
        private void radioButtonB_CheckedChanged(object sender, EventArgs e) { }
        private void radioButtonC_CheckedChanged(object sender, EventArgs e) { }
        private void radioButtonD_CheckedChanged(object sender, EventArgs e) { }
    }
}
