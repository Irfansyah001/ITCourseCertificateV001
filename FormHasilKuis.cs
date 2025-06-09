// FormHasilKuis.cs
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    public partial class FormHasilKuis : Form
    {
        public int KursusID { get; set; }
        public string KursusJudul { get; set; }
        public int UserID { get; set; }
        public int NilaiAkhir { get; set; }
        public string CertificateIDuniq { get; set; }

        private PrintDocument printDocument1 = new PrintDocument();
        private string NamaLengkapUser;

        public FormHasilKuis()
        {
            InitializeComponent();
            printDocument1.PrintPage += new PrintPageEventHandler(PrintDocument1_PrintPage);
            this.Resize += new EventHandler(FormHasilKuis_Resize);
        }

        private void FormHasilKuis_Load(object sender, EventArgs e)
        {
            lblJudul.Text = "Hasil Kuis: " + KursusJudul;
            lblNilai.Text = $"Nilai Akhir Anda: {NilaiAkhir}";

            bool lulus = NilaiAkhir >= 80;
            lblStatus.Text = lulus ? "Status: LULUS \u2705" : "Status: GAGAL \u274C";
            lblStatus.ForeColor = lulus ? Color.DarkGreen : Color.Maroon;
            this.BackColor = lulus ? Color.Honeydew : Color.MistyRose;

            btnCetak.Visible = lulus && !string.IsNullOrEmpty(CertificateIDuniq);
            NamaLengkapUser = GetUserName(UserID);
            CenterControls();
        }

        private void CenterControls()
        {
            int centerX = (this.ClientSize.Width) / 2;
            lblJudul.Left = centerX - lblJudul.Width / 2;
            lblNilai.Left = centerX - lblNilai.Width / 2;
            lblStatus.Left = centerX - lblStatus.Width / 2;
            btnKembali.Left = centerX - btnKembali.Width / 2;
            btnCetak.Left = centerX - btnCetak.Width / 2;
        }

        private void FormHasilKuis_Resize(object sender, EventArgs e)
        {
            CenterControls();
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            FormMenu menu = new FormMenu
            {
                LoggedInUserID = this.UserID,
                LoggedInName = NamaLengkapUser,
                WindowState = FormWindowState.Maximized
            };
            menu.Show();
            this.Close();
        }

        private void btnCetak_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument1
            };
            preview.ShowDialog();
        }

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int marginLeft = 80;
            int y = 100;

            Font titleFont = new Font("Segoe UI", 24, FontStyle.Bold);
            Font normalFont = new Font("Segoe UI", 14);
            Font boldFont = new Font("Segoe UI", 16, FontStyle.Bold);
            Font smallFont = new Font("Segoe UI", 10);
            Brush blackBrush = Brushes.Black;

            string certID = "CERT-" + DateTime.Now.ToString("yyyyMMdd") + "-" + new Random().Next(1000, 9999);

            Pen borderPen = new Pen(Color.Gray, 2);
            Rectangle border = new Rectangle(50, 50, e.PageBounds.Width - 100, e.PageBounds.Height - 100);
            g.DrawRectangle(borderPen, border);

            try
            {
                Image logo = Image.FromFile("logo_certificate.png");
                g.DrawImage(logo, e.PageBounds.Width / 2 - 40, 60, 80, 80);
                y += 100;
            }
            catch { }

            g.DrawString("Certificate of Completion", titleFont, blackBrush, new PointF(marginLeft + 100, y));
            y += 80;

            g.DrawString("Certificate ID: " + CertificateIDuniq, smallFont, blackBrush, new PointF(marginLeft, y));
            y += 40;

            g.DrawString("This is to certify that:", normalFont, blackBrush, new PointF(marginLeft, y));
            y += 30;

            g.DrawString(NamaLengkapUser, boldFont, blackBrush, new PointF(marginLeft, y));
            y += 40;

            g.DrawString("Has successfully completed the course:", normalFont, blackBrush, new PointF(marginLeft, y));
            y += 30;

            g.DrawString($"\"{KursusJudul}\"", boldFont, blackBrush, new PointF(marginLeft, y));
            y += 40;

            g.DrawString($"With a final score of {NilaiAkhir}/100", normalFont, blackBrush, new PointF(marginLeft, y));
            y += 60;

            string statement = "This certificate is issued as proof of successful completion of all required assessments under the ITCourseCertificate Program.";
            g.DrawString(statement, normalFont, blackBrush, new RectangleF(marginLeft, y, 600, 100));
            y += 100;

            g.DrawString("Date: " + DateTime.Now.ToString("dd MMMM yyyy"), normalFont, blackBrush, new PointF(marginLeft, y));

            g.DrawString("_______________________", normalFont, blackBrush, new PointF(marginLeft, y + 80));
            g.DrawString("Instructor Signature", smallFont, blackBrush, new PointF(marginLeft, y + 100));

            g.DrawString("_______________________", normalFont, blackBrush, new PointF(marginLeft + 350, y + 80));
            g.DrawString("Official Seal", smallFont, blackBrush, new PointF(marginLeft + 390, y + 100));

        }

        private string GetUserName(int userId)
        {
            string result = "";
            string connString = "Data Source=localhost;Initial Catalog=CertificateCourseDB;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT FullName FROM Users WHERE UserID = @id", conn);
                cmd.Parameters.AddWithValue("@id", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result = reader["FullName"].ToString();
                }
            }
            return result;
        }
    }
}
