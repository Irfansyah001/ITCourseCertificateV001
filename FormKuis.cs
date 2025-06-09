using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    public partial class FormKuis : Form
    {
        public int UserID { get; set; }
        public int KursusID { get; set; }
        public string KursusJudul { get; set; }
        public string CertificateIDuniq { get; set; }


        private int skor = 0;
        private int indexSoal = 0;
        private DataTable soalTable;
        private int waktuTersisa = 60; // dalam detik
        private string[] jawabanUserArray;

        public FormKuis()
        {
            InitializeComponent();
        }

        private void FormKuis_Load(object sender, EventArgs e)
        {
            LoadSoal();
            jawabanUserArray = new string[soalTable.Rows.Count];
            TampilkanSoal();

            quizTimer.Interval = 1000; // 1 detik
            quizTimer.Start();

            // Pusatkan komponen
            CenterLayout();
        }

        private void LoadSoal()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=CertificateCourseDB;Integrated Security=True"))
            {
                conn.Open();
                string query = "SELECT * FROM Kuis WHERE KursusID = @id";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@id", KursusID);
                soalTable = new DataTable();
                da.Fill(soalTable);
            }
        }

        private void TampilkanSoal()
        {
            if (indexSoal < soalTable.Rows.Count)
            {
                DataRow row = soalTable.Rows[indexSoal];
                lblPertanyaan.Text = row["Pertanyaan"].ToString();
                rdoA.Text = "A. " + row["PilihanA"].ToString();
                rdoB.Text = "B. " + row["PilihanB"].ToString();
                rdoC.Text = "C. " + row["PilihanC"].ToString();
                rdoD.Text = "D. " + row["PilihanD"].ToString();

                // Tampilkan jawaban sebelumnya jika ada
                string jawabanTersimpan = jawabanUserArray[indexSoal];
                rdoA.Checked = jawabanTersimpan == "A";
                rdoB.Checked = jawabanTersimpan == "B";
                rdoC.Checked = jawabanTersimpan == "C";
                rdoD.Checked = jawabanTersimpan == "D";
            }
            else
            {
                SelesaiKuis();
            }
        }

        private void SimpanJawabanSementara()
        {
            string jawaban = rdoA.Checked ? "A" :
                             rdoB.Checked ? "B" :
                             rdoC.Checked ? "C" :
                             rdoD.Checked ? "D" : "";

            jawabanUserArray[indexSoal] = jawaban;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (indexSoal >= soalTable.Rows.Count) return;

            SimpanJawabanSementara();

            string jawabanBenar = soalTable.Rows[indexSoal]["JawabanBenar"].ToString();
            string jawabanUser = jawabanUserArray[indexSoal];

            bool benar = jawabanUser == jawabanBenar;
            if (benar) skor += 20;

            SimpanJawaban(jawabanUser, benar);

            if (indexSoal == soalTable.Rows.Count - 1)
            {
                DialogResult confirm = MessageBox.Show(
                    "Ini adalah soal terakhir. Yakin ingin submit jawaban Anda sekarang?",
                    "Konfirmasi Submit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm != DialogResult.Yes)
                {
                    return; // batalkan submit jika user klik "No"
                }
            }

            indexSoal++;
            TampilkanSoal();
        }

        private void SimpanJawaban(string jawaban, bool benar)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=CertificateCourseDB;Integrated Security=True"))
            {
                conn.Open();
                string query = "INSERT INTO JawabanUser (UserID, KuisID, Jawaban, IsCorrect) VALUES (@user, @kuis, @jawaban, @correct)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", UserID);
                cmd.Parameters.AddWithValue("@kuis", soalTable.Rows[indexSoal]["KuisID"]);
                cmd.Parameters.AddWithValue("@jawaban", jawaban);
                cmd.Parameters.AddWithValue("@correct", benar);
                cmd.ExecuteNonQuery();
            }
        }

        private void quizTimer_Tick(object sender, EventArgs e)
        {
            waktuTersisa--;
            lblTimer.Text = $"Waktu: {waktuTersisa} detik";

            if (waktuTersisa <= 0)
            {
                quizTimer.Stop();
                MessageBox.Show("Waktu habis! Anda akan kembali ke halaman materi.", "Waktu Habis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                FormMateri formMateri = new FormMateri
                {
                    KursusID = this.KursusID,
                    UserID = this.UserID,
                    KursusJudul = this.KursusJudul,
                    WindowState = FormWindowState.Maximized,
                    StartPosition = FormStartPosition.CenterScreen
                };

                formMateri.Show();
                this.Close();
            }
        }

        // FormKuis.cs (Bagian akhir kuis, pastikan mengirim KursusJudul)
        private void SelesaiKuis()
        {
            quizTimer.Stop();

            int totalBenar = 0;
            for (int i = 0; i < soalTable.Rows.Count; i++)
            {
                string jawabanUser = jawabanUserArray[i];
                string jawabanBenar = soalTable.Rows[i]["JawabanBenar"].ToString();
                if (jawabanUser == jawabanBenar)
                {
                    totalBenar++;
                }
            }

            int skor = (int)Math.Round((double)totalBenar / soalTable.Rows.Count * 100);

            string certID = ""; // inisialisasi dulu di luar blok if

            string pesan = skor >= 80
                ? "Selamat! Anda lulus dan mendapatkan sertifikat."
                : "Maaf, nilai Anda kurang dari 80.";

            MessageBox.Show($"Nilai Anda: {skor}\n{pesan}", "Hasil Kuis", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (skor >= 80)
            {
                certID = "CERT-" + DateTime.Now.ToString("yyyyMMdd") + "-" + new Random().Next(1000, 9999);

                using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=CertificateCourseDB;Integrated Security=True"))
                {
                    conn.Open();
                    string query = "INSERT INTO Certificate0 (UserID, KursusID, Nilai, CertificateIDuniq) " +
                                   "VALUES (@user, @kursus, @nilai, @certid)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", UserID);
                    cmd.Parameters.AddWithValue("@kursus", KursusID);
                    cmd.Parameters.AddWithValue("@nilai", skor);
                    cmd.Parameters.AddWithValue("@certid", certID);
                    cmd.ExecuteNonQuery();
                }
            }

            FormHasilKuis hasil = new FormHasilKuis
            {
                UserID = this.UserID,
                KursusID = this.KursusID,
                KursusJudul = this.KursusJudul,
                NilaiAkhir = skor,
                CertificateIDuniq = certID,
                WindowState = FormWindowState.Maximized
            };

            hasil.Show();
            this.Close();
        }


        private void CenterLayout()
        {
            lblPertanyaan.Left = (this.ClientSize.Width - lblPertanyaan.Width) / 2;
            grpJawaban.Left = (this.ClientSize.Width - grpJawaban.Width) / 2;
            btnSubmit.Left = (this.ClientSize.Width - btnSubmit.Width) / 2;
        }

        private void btnBackSoal_Click(object sender, EventArgs e)
        {
            if (indexSoal > 0)
            {
                SimpanJawabanSementara();
                indexSoal--;
                TampilkanSoal();
            }
            else
            {
                MessageBox.Show("Ini adalah soal pertama.");
            }
        }

        private void btnSkipSoal_Click(object sender, EventArgs e)
        {
            if (indexSoal < soalTable.Rows.Count - 1)
            {
                SimpanJawabanSementara();
                indexSoal++;
                TampilkanSoal();
            }
            else
            {
                MessageBox.Show("Ini adalah soal terakhir.");
            }
        }

        private void FormKuis_Resize(object sender, EventArgs e)
        {
            // Posisi tengah komponen utama
            lblPertanyaan.Left = (this.ClientSize.Width - lblPertanyaan.Width) / 2;
            grpJawaban.Left = (this.ClientSize.Width - grpJawaban.Width) / 2;
            btnSubmit.Left = (this.ClientSize.Width - btnSubmit.Width) / 2;

            // Posisi tombol navigasi
            btnBackSoal.Left = grpJawaban.Left;
            btnSkipSoal.Left = grpJawaban.Right - btnSkipSoal.Width;

            // Posisi waktu di pojok kanan atas
            lblTimer.Top = 10;
            lblTimer.Left = this.ClientSize.Width - lblTimer.Width - 10;
        }


    }
}
