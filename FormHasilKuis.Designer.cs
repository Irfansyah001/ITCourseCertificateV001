namespace ITCourseCertificateV001
{
    partial class FormHasilKuis
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblJudul = new System.Windows.Forms.Label();
            this.lblNilai = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnKembali = new System.Windows.Forms.Button();
            this.btnCetak = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblJudul
            // 
            this.lblJudul.AutoSize = true;
            this.lblJudul.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblJudul.Location = new System.Drawing.Point(0, 40);
            this.lblJudul.Name = "lblJudul";
            this.lblJudul.Size = new System.Drawing.Size(167, 41);
            this.lblJudul.TabIndex = 0;
            this.lblJudul.Text = "Hasil Kuis:";
            this.lblJudul.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNilai
            // 
            this.lblNilai.AutoSize = true;
            this.lblNilai.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.lblNilai.Location = new System.Drawing.Point(0, 100);
            this.lblNilai.Name = "lblNilai";
            this.lblNilai.Size = new System.Drawing.Size(188, 30);
            this.lblNilai.TabIndex = 1;
            this.lblNilai.Text = "Nilai Akhir Anda: 0";
            this.lblNilai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(0, 145);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(141, 28);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status: LULUS";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnKembali
            // 
            this.btnKembali.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnKembali.Location = new System.Drawing.Point(0, 200);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(150, 35);
            this.btnKembali.TabIndex = 3;
            this.btnKembali.Text = "Back to Menu";
            this.btnKembali.UseVisualStyleBackColor = true;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // btnCetak
            // 
            this.btnCetak.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCetak.Location = new System.Drawing.Point(0, 250);
            this.btnCetak.Name = "btnCetak";
            this.btnCetak.Size = new System.Drawing.Size(150, 35);
            this.btnCetak.TabIndex = 4;
            this.btnCetak.Text = "Certificate";
            this.btnCetak.UseVisualStyleBackColor = true;
            this.btnCetak.Visible = false;
            this.btnCetak.Click += new System.EventHandler(this.btnCetak_Click);
            // 
            // FormHasilKuis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.btnCetak);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblNilai);
            this.Controls.Add(this.lblJudul);
            this.Name = "FormHasilKuis";
            this.Text = "Hasil Kuis";
            this.Load += new System.EventHandler(this.FormHasilKuis_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Label lblNilai;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.Button btnCetak;
    }
}
