namespace ITCourseCertificateV001
{
    partial class FormDashboard
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
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnMengerjakanKuis = new System.Windows.Forms.Button();
            this.btnMengelolaMateri = new System.Windows.Forms.Button();
            this.btnMengelolaKuis = new System.Windows.Forms.Button();
            this.btnSertifikat = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.tableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.lblWelcome, 0, 0);
            this.tableLayout.Controls.Add(this.btnMengerjakanKuis, 0, 1);
            this.tableLayout.Controls.Add(this.btnMengelolaMateri, 0, 2);
            this.tableLayout.Controls.Add(this.btnMengelolaKuis, 0, 3);
            this.tableLayout.Controls.Add(this.btnSertifikat, 0, 4);
            this.tableLayout.Controls.Add(this.btnLogout, 0, 5);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 6;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Size = new System.Drawing.Size(800, 400);
            this.tableLayout.TabIndex = 0;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(250, 15);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(300, 32);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Selamat datang, [Name]";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMengerjakanKuis
            // 
            this.btnMengerjakanKuis.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMengerjakanKuis.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnMengerjakanKuis.Location = new System.Drawing.Point(270, 65);
            this.btnMengerjakanKuis.Name = "btnMengerjakanKuis";
            this.btnMengerjakanKuis.Size = new System.Drawing.Size(260, 45);
            this.btnMengerjakanKuis.TabIndex = 1;
            this.btnMengerjakanKuis.Text = "Mengerjakan Kuis";
            this.btnMengerjakanKuis.UseVisualStyleBackColor = true;
            this.btnMengerjakanKuis.Click += new System.EventHandler(this.btnMengerjakanKuis_Click);
            // 
            // btnMengelolaMateri
            // 
            this.btnMengelolaMateri.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMengelolaMateri.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnMengelolaMateri.Location = new System.Drawing.Point(270, 125);
            this.btnMengelolaMateri.Name = "btnMengelolaMateri";
            this.btnMengelolaMateri.Size = new System.Drawing.Size(260, 45);
            this.btnMengelolaMateri.TabIndex = 2;
            this.btnMengelolaMateri.Text = "Mengelola Materi";
            this.btnMengelolaMateri.UseVisualStyleBackColor = true;
            this.btnMengelolaMateri.Click += new System.EventHandler(this.btnMengelolaMateri_Click);
            // 
            // btnMengelolaKuis
            // 
            this.btnMengelolaKuis.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMengelolaKuis.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnMengelolaKuis.Location = new System.Drawing.Point(270, 185);
            this.btnMengelolaKuis.Name = "btnMengelolaKuis";
            this.btnMengelolaKuis.Size = new System.Drawing.Size(260, 45);
            this.btnMengelolaKuis.TabIndex = 3;
            this.btnMengelolaKuis.Text = "Mengelola Kuis";
            this.btnMengelolaKuis.UseVisualStyleBackColor = true;
            this.btnMengelolaKuis.Click += new System.EventHandler(this.btnMengelolaKuis_Click);
            // 
            // btnSertifikat
            // 
            this.btnSertifikat.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSertifikat.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSertifikat.Location = new System.Drawing.Point(270, 245);
            this.btnSertifikat.Name = "btnSertifikat";
            this.btnSertifikat.Size = new System.Drawing.Size(260, 45);
            this.btnSertifikat.TabIndex = 4;
            this.btnSertifikat.Text = "Sertifikat";
            this.btnSertifikat.UseVisualStyleBackColor = true;
            this.btnSertifikat.Click += new System.EventHandler(this.btnSertifikat_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLogout.BackColor = System.Drawing.Color.DarkGray;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.Location = new System.Drawing.Point(340, 320);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(120, 40);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // FormDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.tableLayout);
            this.Name = "FormDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.FormDashboard_Load);
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnMengerjakanKuis;
        private System.Windows.Forms.Button btnMengelolaMateri;
        private System.Windows.Forms.Button btnMengelolaKuis;
        private System.Windows.Forms.Button btnSertifikat;
        private System.Windows.Forms.Button btnLogout;
    }
}
