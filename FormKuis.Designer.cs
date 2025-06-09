namespace ITCourseCertificateV001
{
    partial class FormKuis
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblPertanyaan = new System.Windows.Forms.Label();
            this.grpJawaban = new System.Windows.Forms.GroupBox();
            this.rdoA = new System.Windows.Forms.RadioButton();
            this.rdoB = new System.Windows.Forms.RadioButton();
            this.rdoC = new System.Windows.Forms.RadioButton();
            this.rdoD = new System.Windows.Forms.RadioButton();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnBackSoal = new System.Windows.Forms.Button();
            this.btnSkipSoal = new System.Windows.Forms.Button();
            this.lblTimer = new System.Windows.Forms.Label();
            this.quizTimer = new System.Windows.Forms.Timer(this.components);
            this.grpJawaban.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPertanyaan
            // 
            this.lblPertanyaan.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPertanyaan.Location = new System.Drawing.Point(30, 20);
            this.lblPertanyaan.Name = "lblPertanyaan";
            this.lblPertanyaan.Size = new System.Drawing.Size(740, 60);
            this.lblPertanyaan.TabIndex = 0;
            this.lblPertanyaan.Text = "Pertanyaan akan muncul di sini";
            this.lblPertanyaan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpJawaban
            // 
            this.grpJawaban.Controls.Add(this.rdoA);
            this.grpJawaban.Controls.Add(this.rdoB);
            this.grpJawaban.Controls.Add(this.rdoC);
            this.grpJawaban.Controls.Add(this.rdoD);
            this.grpJawaban.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.grpJawaban.Location = new System.Drawing.Point(80, 100);
            this.grpJawaban.Name = "grpJawaban";
            this.grpJawaban.Size = new System.Drawing.Size(640, 160);
            this.grpJawaban.TabIndex = 1;
            this.grpJawaban.TabStop = false;
            this.grpJawaban.Text = "Pilihan Jawaban";
            // 
            // rdoA
            // 
            this.rdoA.AutoSize = true;
            this.rdoA.Location = new System.Drawing.Point(20, 30);
            this.rdoA.Name = "rdoA";
            this.rdoA.Size = new System.Drawing.Size(49, 29);
            this.rdoA.TabIndex = 0;
            this.rdoA.TabStop = true;
            this.rdoA.Text = "A.";
            this.rdoA.UseVisualStyleBackColor = true;
            // 
            // rdoB
            // 
            this.rdoB.AutoSize = true;
            this.rdoB.Location = new System.Drawing.Point(20, 60);
            this.rdoB.Name = "rdoB";
            this.rdoB.Size = new System.Drawing.Size(48, 29);
            this.rdoB.TabIndex = 1;
            this.rdoB.TabStop = true;
            this.rdoB.Text = "B.";
            this.rdoB.UseVisualStyleBackColor = true;
            // 
            // rdoC
            // 
            this.rdoC.AutoSize = true;
            this.rdoC.Location = new System.Drawing.Point(20, 90);
            this.rdoC.Name = "rdoC";
            this.rdoC.Size = new System.Drawing.Size(49, 29);
            this.rdoC.TabIndex = 2;
            this.rdoC.TabStop = true;
            this.rdoC.Text = "C.";
            this.rdoC.UseVisualStyleBackColor = true;
            // 
            // rdoD
            // 
            this.rdoD.AutoSize = true;
            this.rdoD.Location = new System.Drawing.Point(20, 120);
            this.rdoD.Name = "rdoD";
            this.rdoD.Size = new System.Drawing.Size(50, 29);
            this.rdoD.TabIndex = 3;
            this.rdoD.TabStop = true;
            this.rdoD.Text = "D.";
            this.rdoD.UseVisualStyleBackColor = true;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.Location = new System.Drawing.Point(310, 280);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(180, 40);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit Jawaban";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnBackSoal
            // 
            this.btnBackSoal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBackSoal.Location = new System.Drawing.Point(80, 280);
            this.btnBackSoal.Name = "btnBackSoal";
            this.btnBackSoal.Size = new System.Drawing.Size(120, 40);
            this.btnBackSoal.TabIndex = 4;
            this.btnBackSoal.Text = "← Sebelumnya";
            this.btnBackSoal.UseVisualStyleBackColor = true;
            this.btnBackSoal.Click += new System.EventHandler(this.btnBackSoal_Click);
            // 
            // btnSkipSoal
            // 
            this.btnSkipSoal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSkipSoal.Location = new System.Drawing.Point(600, 280);
            this.btnSkipSoal.Name = "btnSkipSoal";
            this.btnSkipSoal.Size = new System.Drawing.Size(120, 40);
            this.btnSkipSoal.TabIndex = 5;
            this.btnSkipSoal.Text = "Lewati →";
            this.btnSkipSoal.UseVisualStyleBackColor = true;
            this.btnSkipSoal.Click += new System.EventHandler(this.btnSkipSoal_Click);
            // 
            // lblTimer
            // 
            this.lblTimer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimer.ForeColor = System.Drawing.Color.Red;
            this.lblTimer.Location = new System.Drawing.Point(650, 10);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(150, 30);
            this.lblTimer.TabIndex = 3;
            this.lblTimer.Text = "Waktu: 60 detik";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // quizTimer
            // 
            this.quizTimer.Interval = 1000;
            this.quizTimer.Tick += new System.EventHandler(this.quizTimer_Tick);
            // 
            // FormKuis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 350);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnBackSoal);
            this.Controls.Add(this.btnSkipSoal);
            this.Controls.Add(this.grpJawaban);
            this.Controls.Add(this.lblPertanyaan);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormKuis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kuis";
            this.Load += new System.EventHandler(this.FormKuis_Load);
            this.Resize += new System.EventHandler(this.FormKuis_Resize);
            this.grpJawaban.ResumeLayout(false);
            this.grpJawaban.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPertanyaan;
        private System.Windows.Forms.GroupBox grpJawaban;
        private System.Windows.Forms.RadioButton rdoA;
        private System.Windows.Forms.RadioButton rdoB;
        private System.Windows.Forms.RadioButton rdoC;
        private System.Windows.Forms.RadioButton rdoD;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnBackSoal;
        private System.Windows.Forms.Button btnSkipSoal;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Timer quizTimer;
    }
}
