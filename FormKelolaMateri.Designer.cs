using System.Windows.Forms;

namespace ITCourseCertificateV001
{
    partial class FormKelolaMateri
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbKursus = new System.Windows.Forms.ComboBox();
            this.labelJudul = new System.Windows.Forms.Label();
            this.txtJudul = new System.Windows.Forms.TextBox();
            this.labelLink = new System.Windows.Forms.Label();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.labelDurasi = new System.Windows.Forms.Label();
            this.txtDurasi = new System.Windows.Forms.TextBox();
            this.labelUrutan = new System.Windows.Forms.Label();
            this.txtUrutan = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnPrintPDF = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.dgvMateri = new System.Windows.Forms.DataGridView();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateri)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.cmbKursus);
            this.panelMain.Controls.Add(this.labelJudul);
            this.panelMain.Controls.Add(this.txtJudul);
            this.panelMain.Controls.Add(this.labelLink);
            this.panelMain.Controls.Add(this.txtLink);
            this.panelMain.Controls.Add(this.labelDurasi);
            this.panelMain.Controls.Add(this.txtDurasi);
            this.panelMain.Controls.Add(this.labelUrutan);
            this.panelMain.Controls.Add(this.txtUrutan);
            this.panelMain.Controls.Add(this.btnCreate);
            this.panelMain.Controls.Add(this.btnUpdate);
            this.panelMain.Controls.Add(this.btnRefresh);
            this.panelMain.Controls.Add(this.btnDelete);
            this.panelMain.Controls.Add(this.btnExport);
            this.panelMain.Controls.Add(this.btnImport);
            this.panelMain.Controls.Add(this.btnPrintPDF);
            this.panelMain.Controls.Add(this.btnBack);
            this.panelMain.Controls.Add(this.dgvMateri);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1924, 1055);
            this.panelMain.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(580, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pilih Kursus:";
            // 
            // cmbKursus
            // 
            this.cmbKursus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKursus.Location = new System.Drawing.Point(680, 27);
            this.cmbKursus.Name = "cmbKursus";
            this.cmbKursus.Size = new System.Drawing.Size(250, 24);
            this.cmbKursus.TabIndex = 1;
            this.cmbKursus.SelectionChangeCommitted += new System.EventHandler(this.cmbKursus_SelectionChangeCommitted);
            // 
            // labelJudul
            // 
            this.labelJudul.ForeColor = System.Drawing.Color.White;
            this.labelJudul.Location = new System.Drawing.Point(30, 30);
            this.labelJudul.Name = "labelJudul";
            this.labelJudul.Size = new System.Drawing.Size(100, 23);
            this.labelJudul.TabIndex = 2;
            this.labelJudul.Text = "Judul Materi:";
            // 
            // txtJudul
            // 
            this.txtJudul.Location = new System.Drawing.Point(150, 27);
            this.txtJudul.Name = "txtJudul";
            this.txtJudul.Size = new System.Drawing.Size(380, 22);
            this.txtJudul.TabIndex = 3;
            // 
            // labelLink
            // 
            this.labelLink.ForeColor = System.Drawing.Color.White;
            this.labelLink.Location = new System.Drawing.Point(30, 65);
            this.labelLink.Name = "labelLink";
            this.labelLink.Size = new System.Drawing.Size(100, 23);
            this.labelLink.TabIndex = 4;
            this.labelLink.Text = "Link Video:";
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(150, 62);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(380, 22);
            this.txtLink.TabIndex = 5;
            // 
            // labelDurasi
            // 
            this.labelDurasi.ForeColor = System.Drawing.Color.White;
            this.labelDurasi.Location = new System.Drawing.Point(30, 100);
            this.labelDurasi.Name = "labelDurasi";
            this.labelDurasi.Size = new System.Drawing.Size(100, 23);
            this.labelDurasi.TabIndex = 6;
            this.labelDurasi.Text = "Durasi Menit:";
            // 
            // txtDurasi
            // 
            this.txtDurasi.Location = new System.Drawing.Point(150, 97);
            this.txtDurasi.Name = "txtDurasi";
            this.txtDurasi.Size = new System.Drawing.Size(380, 22);
            this.txtDurasi.TabIndex = 7;
            // 
            // labelUrutan
            // 
            this.labelUrutan.ForeColor = System.Drawing.Color.White;
            this.labelUrutan.Location = new System.Drawing.Point(30, 135);
            this.labelUrutan.Name = "labelUrutan";
            this.labelUrutan.Size = new System.Drawing.Size(100, 23);
            this.labelUrutan.TabIndex = 8;
            this.labelUrutan.Text = "Urutan:";
            // 
            // txtUrutan
            // 
            this.txtUrutan.Location = new System.Drawing.Point(150, 132);
            this.txtUrutan.Name = "txtUrutan";
            this.txtUrutan.Size = new System.Drawing.Size(380, 22);
            this.txtUrutan.TabIndex = 9;
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCreate.Location = new System.Drawing.Point(581, 62);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(110, 35);
            this.btnCreate.TabIndex = 10;
            this.btnCreate.Text = "Simpan";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnUpdate.Location = new System.Drawing.Point(697, 62);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(110, 35);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRefresh.Location = new System.Drawing.Point(581, 103);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(110, 35);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDelete.Location = new System.Drawing.Point(697, 103);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 35);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "Hapus";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnExport.Location = new System.Drawing.Point(881, 103);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(110, 35);
            this.btnExport.TabIndex = 14;
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnImport.Location = new System.Drawing.Point(881, 65);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(110, 35);
            this.btnImport.TabIndex = 15;
            this.btnImport.Text = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnPrintPDF
            // 
            this.btnPrintPDF.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintPDF.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPrintPDF.Location = new System.Drawing.Point(997, 65);
            this.btnPrintPDF.Name = "btnPrintPDF";
            this.btnPrintPDF.Size = new System.Drawing.Size(110, 35);
            this.btnPrintPDF.TabIndex = 16;
            this.btnPrintPDF.Text = "Cetak PDF";
            this.btnPrintPDF.Click += new System.EventHandler(this.btnPrintPDF_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Maroon;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnBack.Location = new System.Drawing.Point(597, 144);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(77, 35);
            this.btnBack.TabIndex = 17;
            this.btnBack.Text = "Kembali";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // dgvMateri
            // 
            this.dgvMateri.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMateri.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMateri.BackgroundColor = System.Drawing.Color.White;
            this.dgvMateri.ColumnHeadersHeight = 29;
            this.dgvMateri.Location = new System.Drawing.Point(12, 211);
            this.dgvMateri.Name = "dgvMateri";
            this.dgvMateri.RowHeadersVisible = false;
            this.dgvMateri.RowHeadersWidth = 51;
            this.dgvMateri.Size = new System.Drawing.Size(1900, 756);
            this.dgvMateri.TabIndex = 18;
            this.dgvMateri.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMateri_CellClick);
            this.dgvMateri.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMateri_CellContentClick);
            // 
            // FormKelolaMateri
            // 
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormKelolaMateri";
            this.Text = "Kelola Materi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormKelolaMateri_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateri)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ComboBox cmbKursus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelJudul;
        private System.Windows.Forms.Label labelLink;
        private System.Windows.Forms.Label labelDurasi;
        private System.Windows.Forms.Label labelUrutan;
        private System.Windows.Forms.TextBox txtJudul;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.TextBox txtDurasi;
        private System.Windows.Forms.TextBox txtUrutan;
        private System.Windows.Forms.DataGridView dgvMateri;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnPrintPDF;
        private System.Windows.Forms.Button btnBack;
    }
}
