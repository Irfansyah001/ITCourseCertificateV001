using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    public partial class FormTampilanKerjaKuis : Form
    {
        private List<Soal> daftarSoal = new List<Soal>();
        private int indeksSoalSaatIni = 0;
        private int durasiDetik = 60;
        private Timer timer;
        private string connectionString = @"Data Source=LAPTOPGW1;Initial Catalog=CertificateCourseDB;Integrated Security=True";
        private bool kuisSedangBerlangsung = false;
        private bool sudahTampilkanWaktuHabis = false;

        public int UserID { get; set; } // ID user dari Form Login atau MainMenu

        public FormTampilanKerjaKuis()
        {
            InitializeComponent();
        }

        private void FormTampilanKerjaKuis_Load(object sender, EventArgs e)
        {
            MuatKursusDariDatabase();
            SetKontrolAwal();
        }

        private void MuatKursusDariDatabase()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT KursusID, JudulKursus FROM DataKursus";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                comboBox1.DisplayMember = "JudulKursus";
                comboBox1.ValueMember = "KursusID";
                comboBox1.DataSource = dt;
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

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Pertanyaan, PilihanA, PilihanB, PilihanC, PilihanD, JawabanBenar FROM Kuis WHERE KursusID = @kursusID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kursusID", kursusID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string jawabanBenarChar = reader.GetString(5).ToUpper();
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
                        Pertanyaan = reader.GetString(0),
                        Pilihan = new string[]
                        {
                            reader.IsDBNull(1) ? "" : reader.GetString(1),
                            reader.IsDBNull(2) ? "" : reader.GetString(2),
                            reader.IsDBNull(3) ? "" : reader.GetString(3),
                            reader.IsDBNull(4) ? "" : reader.GetString(4)
                        },
                        JawabanBenarIndex = jawabanBenarIndex,
                        JawabanUserIndex = null
                    });
                }
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

            // Generate CertificateID hanya jika lulus
            string certificateID = null;
            if (nilaiAkhir >= 80)
            {
                certificateID = GenerateCertificateID(); // pastikan metode ini tersedia di kelas

                // ⬇️ Tambahkan kode ini
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Certificate0 (UserID, KursusID, Nilai, TanggalDapat, IsPrinted) 
                         VALUES (@UserID, @KursusID, @Nilai, @TanggalDapat, @IsPrinted)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@KursusID", kursusId);
                    cmd.Parameters.AddWithValue("@Nilai", nilaiAkhir);
                    cmd.Parameters.AddWithValue("@TanggalDapat", DateTime.Now);
                    cmd.Parameters.AddWithValue("@IsPrinted", 0);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // Panggil FormHasilKuis dengan data lengkap
            FormHasilKuis hasilForm = new FormHasilKuis
            {
                UserID = this.UserID,
                KursusID = kursusId,
                KursusJudul = comboBox1.Text,
                NilaiAkhir = nilaiAkhir,
                CertificateIDuniq = certificateID
            };

            hasilForm.WindowState = FormWindowState.Maximized;
            hasilForm.Show();

            this.Close(); // Tutup form kuis saat selesai
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
            return "CERT-" + DateTime.Now.ToString("yyyyMMdd") + "-" + new Random().Next(1000, 9999);
        }

        private class Soal
        {
            public string Pertanyaan { get; set; }
            public string[] Pilihan { get; set; }
            public int JawabanBenarIndex { get; set; }
            public int? JawabanUserIndex { get; set; }
            public int? ReplaceKursusID { get; set; } = null;
        }

        private void radioButtonA_CheckedChanged(object sender, EventArgs e) { }
        private void radioButtonB_CheckedChanged(object sender, EventArgs e) { }
        private void radioButtonC_CheckedChanged(object sender, EventArgs e) { }
        private void radioButtonD_CheckedChanged(object sender, EventArgs e) { }
    }
}
