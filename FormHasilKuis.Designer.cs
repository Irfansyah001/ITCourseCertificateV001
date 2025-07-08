namespace ITCourseCertificateV001
{
    partial class FormHasilKuis
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Label lblNilai;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.Button btnLihatSertifikat;
        private System.Windows.Forms.Label lblPesanBcktoMnu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblJudul = new System.Windows.Forms.Label();
            this.lblNilai = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnKembali = new System.Windows.Forms.Button();
            this.btnLihatSertifikat = new System.Windows.Forms.Button();
            this.lblPesanBcktoMnu = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblJudul
            // 
            this.lblJudul.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblJudul.Location = new System.Drawing.Point(3, 0);
            this.lblJudul.Name = "lblJudul";
            this.lblJudul.Size = new System.Drawing.Size(794, 60);
            this.lblJudul.TabIndex = 0;
            this.lblJudul.Text = "Hasil Kuis: [Nama Materi]";
            this.lblJudul.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNilai
            // 
            this.lblNilai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNilai.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.lblNilai.Location = new System.Drawing.Point(3, 60);
            this.lblNilai.Name = "lblNilai";
            this.lblNilai.Size = new System.Drawing.Size(794, 40);
            this.lblNilai.TabIndex = 1;
            this.lblNilai.Text = "Nilai Akhir Anda: 100";
            this.lblNilai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.Green;
            this.lblStatus.Location = new System.Drawing.Point(3, 100);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(794, 40);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status: LULUS ✅";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnKembali
            // 
            this.btnKembali.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnKembali.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnKembali.Location = new System.Drawing.Point(325, 177);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(150, 35);
            this.btnKembali.TabIndex = 4;
            this.btnKembali.Text = "Back to Menu";
            this.btnKembali.UseVisualStyleBackColor = true;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // btnLihatSertifikat
            // 
            this.btnLihatSertifikat.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLihatSertifikat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLihatSertifikat.Location = new System.Drawing.Point(305, 292);
            this.btnLihatSertifikat.Name = "btnLihatSertifikat";
            this.btnLihatSertifikat.Size = new System.Drawing.Size(190, 35);
            this.btnLihatSertifikat.TabIndex = 5;
            this.btnLihatSertifikat.Text = "Lihat Sertifikat";
            this.btnLihatSertifikat.UseVisualStyleBackColor = true;
            this.btnLihatSertifikat.Visible = false;
            this.btnLihatSertifikat.Click += new System.EventHandler(this.btnLihatSertifikat_Click);
            // 
            // lblPesanBcktoMnu
            // 
            this.lblPesanBcktoMnu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPesanBcktoMnu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblPesanBcktoMnu.ForeColor = System.Drawing.Color.Maroon;
            this.lblPesanBcktoMnu.Location = new System.Drawing.Point(3, 140);
            this.lblPesanBcktoMnu.Name = "lblPesanBcktoMnu";
            this.lblPesanBcktoMnu.Size = new System.Drawing.Size(794, 30);
            this.lblPesanBcktoMnu.TabIndex = 3;
            this.lblPesanBcktoMnu.Text = "Silakan coba lagi nanti atau pelajari materi terlebih dahulu.";
            this.lblPesanBcktoMnu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPesanBcktoMnu.Visible = false;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.lblJudul, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.lblNilai, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.lblStatus, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.lblPesanBcktoMnu, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.btnKembali, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.btnLihatSertifikat, 0, 5);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 6;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(800, 400);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // FormHasilKuis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "FormHasilKuis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hasil Kuis";
            this.Load += new System.EventHandler(this.FormHasilKuis_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
