using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    public partial class FormCertificateViewer : Form
    {
        //Koneksi kn = new Koneksi();
        //string strKonek = "";

        private int certificateId;

        public int UserID { get; set; }

        public FormCertificateViewer(int certId)
        {
            InitializeComponent();
            // strKonek = kn.connectionString(); // Ambil koneksi dari Koneksi.cs
            certificateId = certId;
        }

        private void FormCertificateViewer_Load(object sender, EventArgs e)
        {
            LoadCertificateReport(certificateId);
        }

        private void LoadCertificateReport(int certificateId)
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
                    using (SqlCommand cmd = new SqlCommand("GetCertificateDetails", conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CertificateID", certificateId);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Data sertifikat tidak ditemukan untuk ID yang diberikan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        ReportDataSource rds = new ReportDataSource("DataSet1", dt); // "DataSet1" harus sesuai dengan nama DataSet di Sertifikat.rdlc
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(rds);

                        // Mengatur path absolut berdasarkan direktori startup
                        string rdlcPath = Path.Combine(Application.StartupPath, "Sertifikat.rdlc");
                        if (!File.Exists(rdlcPath))
                        {
                            MessageBox.Show($"File laporan Sertifikat.rdlc tidak ditemukan di: {rdlcPath}", "Error File RDLC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        reportViewer1.LocalReport.ReportPath = rdlcPath;
                        reportViewer1.RefreshReport();

                        this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                        this.reportViewer1.ZoomPercent = 150;

                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Terjadi masalah database saat memuat data sertifikat: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga saat memuat data sertifikat: " + ex.Message, "Kesalahan Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Kembali ke FormDashboard (atau halaman lain sesuai alur)
            // Tidak perlu lagi mengambil FullName di sini,
            // FormDashboard akan mengambilnya sendiri jika kosong.
            FormRiwayatKuis riwayatkuis = new FormRiwayatKuis
            {
                UserID = this.UserID,
                // FullName tidak perlu diteruskan jika FormDashboard bisa mengambil sendiri
                // FullName = this.FullName, // Baris ini bisa dihapus atau dikomentari
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Maximized
            };

            this.Hide();
            riwayatkuis.Show();
            
        }

        private void btnBackToDashboard_Click(object sender, EventArgs e)
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


        private void tableLayoutPanelMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
