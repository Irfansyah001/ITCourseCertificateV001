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
        private int certificateId;

        // Properti tambahan jika ingin kembali ke dashboard pengguna tertentu
        public int UserID { get; set; }

        public FormCertificateViewer(int certId)
        {
            InitializeComponent();
            certificateId = certId;
        }

        private void FormCertificateViewer_Load(object sender, EventArgs e)
        {
            LoadCertificateReport(certificateId);
        }

        private void LoadCertificateReport(int certificateId)
        {
            string connectionString = @"Data Source=LAPTOPGW1;Initial Catalog=CertificateCourseDB;Integrated Security=True;";
            string query = @"
                SELECT 
                    c.CertificateID,
                    u.FullName AS NamaPeserta,
                    dk.JudulKursus,
                    c.Nilai,
                    c.TanggalDapat,
                    m.JudulMateri,
                    m.LinkVideo,
                    m.Urutan,
                    m.DurasiMenit
                FROM Certificate0 c
                JOIN Users u ON c.UserID = u.UserID
                JOIN DataKursus dk ON c.KursusID = dk.KursusID
                LEFT JOIN Materi m ON m.KursusID = dk.KursusID
                WHERE c.CertificateID = @CertificateID;
            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@CertificateID", certificateId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Data sertifikat tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Mengatur path absolut berdasarkan direktori startup
                string rdlcPath = Path.Combine(Application.StartupPath, "Sertifikat.rdlc");
                if (!File.Exists(rdlcPath))
                {
                    MessageBox.Show($"File RDLC tidak ditemukan di: {rdlcPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                reportViewer1.LocalReport.ReportPath = rdlcPath;
                reportViewer1.RefreshReport();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Kembali ke FormDashboard (atau halaman lain sesuai alur)
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
