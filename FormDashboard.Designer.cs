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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.buttonLayout = new System.Windows.Forms.TableLayoutPanel();
            this.btnMengelolaMateri = new System.Windows.Forms.Button();
            this.btnMengerjakanKuis = new System.Windows.Forms.Button();
            this.btnMengelolaKuis = new System.Windows.Forms.Button();
            this.btnSertifikat = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.buttonLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(60)))), ((int)(((byte)(45)))));
            this.panelTop.Controls.Add(this.lblWelcome);
            this.panelTop.Controls.Add(this.lblUserID);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1152, 100);
            this.panelTop.TabIndex = 0;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(22, 13);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(900, 44);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Selamat datang, [nama]";
            // 
            // lblUserID
            // 
            this.lblUserID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblUserID.ForeColor = System.Drawing.Color.LightGray;
            this.lblUserID.Location = new System.Drawing.Point(28, 56);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(338, 32);
            this.lblUserID.TabIndex = 1;
            this.lblUserID.Text = "ID Anda: ";
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(30)))));
            this.panelMenu.Controls.Add(this.buttonLayout);
            this.panelMenu.Controls.Add(this.btnLogout);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMenu.Location = new System.Drawing.Point(0, 100);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(1152, 700);
            this.panelMenu.TabIndex = 1;
            // 
            // buttonLayout
            // 
            this.buttonLayout.ColumnCount = 1;
            this.buttonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttonLayout.Controls.Add(this.btnMengelolaMateri, 0, 0);
            this.buttonLayout.Controls.Add(this.btnMengerjakanKuis, 0, 1);
            this.buttonLayout.Controls.Add(this.btnMengelolaKuis, 0, 2);
            this.buttonLayout.Controls.Add(this.btnSertifikat, 0, 3);
            this.buttonLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLayout.Location = new System.Drawing.Point(0, 0);
            this.buttonLayout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonLayout.Name = "buttonLayout";
            this.buttonLayout.Padding = new System.Windows.Forms.Padding(338, 49, 338, 125);
            this.buttonLayout.RowCount = 4;
            this.buttonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.buttonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.buttonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.buttonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.buttonLayout.Size = new System.Drawing.Size(1152, 700);
            this.buttonLayout.TabIndex = 0;
            // 
            // btnMengelolaMateri
            // 
            this.btnMengelolaMateri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(100)))));
            this.btnMengelolaMateri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMengelolaMateri.FlatAppearance.BorderSize = 0;
            this.btnMengelolaMateri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMengelolaMateri.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMengelolaMateri.ForeColor = System.Drawing.Color.White;
            this.btnMengelolaMateri.Location = new System.Drawing.Point(341, 53);
            this.btnMengelolaMateri.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMengelolaMateri.Name = "btnMengelolaMateri";
            this.btnMengelolaMateri.Size = new System.Drawing.Size(470, 123);
            this.btnMengelolaMateri.TabIndex = 0;
            this.btnMengelolaMateri.Text = "Mengelola Materi";
            this.btnMengelolaMateri.UseVisualStyleBackColor = false;
            this.btnMengelolaMateri.Click += new System.EventHandler(this.btnMengelolaMateri_Click);
            // 
            // btnMengerjakanKuis
            // 
            this.btnMengerjakanKuis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(100)))));
            this.btnMengerjakanKuis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMengerjakanKuis.FlatAppearance.BorderSize = 0;
            this.btnMengerjakanKuis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMengerjakanKuis.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMengerjakanKuis.ForeColor = System.Drawing.Color.White;
            this.btnMengerjakanKuis.Location = new System.Drawing.Point(341, 184);
            this.btnMengerjakanKuis.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMengerjakanKuis.Name = "btnMengerjakanKuis";
            this.btnMengerjakanKuis.Size = new System.Drawing.Size(470, 123);
            this.btnMengerjakanKuis.TabIndex = 1;
            this.btnMengerjakanKuis.Text = "Mulai Mengerjakan Kuis";
            this.btnMengerjakanKuis.UseVisualStyleBackColor = false;
            this.btnMengerjakanKuis.Click += new System.EventHandler(this.btnMengerjakanKuis_Click);
            // 
            // btnMengelolaKuis
            // 
            this.btnMengelolaKuis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(100)))));
            this.btnMengelolaKuis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMengelolaKuis.FlatAppearance.BorderSize = 0;
            this.btnMengelolaKuis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMengelolaKuis.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMengelolaKuis.ForeColor = System.Drawing.Color.White;
            this.btnMengelolaKuis.Location = new System.Drawing.Point(341, 315);
            this.btnMengelolaKuis.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMengelolaKuis.Name = "btnMengelolaKuis";
            this.btnMengelolaKuis.Size = new System.Drawing.Size(470, 123);
            this.btnMengelolaKuis.TabIndex = 2;
            this.btnMengelolaKuis.Text = "History Kuis";
            this.btnMengelolaKuis.UseVisualStyleBackColor = false;
            this.btnMengelolaKuis.Click += new System.EventHandler(this.btnMengelolaKuis_Click);
            // 
            // btnSertifikat
            // 
            this.btnSertifikat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(100)))));
            this.btnSertifikat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSertifikat.FlatAppearance.BorderSize = 0;
            this.btnSertifikat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSertifikat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSertifikat.ForeColor = System.Drawing.Color.White;
            this.btnSertifikat.Location = new System.Drawing.Point(341, 446);
            this.btnSertifikat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSertifikat.Name = "btnSertifikat";
            this.btnSertifikat.Size = new System.Drawing.Size(470, 125);
            this.btnSertifikat.TabIndex = 3;
            this.btnSertifikat.Text = "Lihat Sertifikat Terbaru";
            this.btnSertifikat.UseVisualStyleBackColor = false;
            this.btnSertifikat.Click += new System.EventHandler(this.btnSertifikat_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.DarkRed;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(0, 0);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(169, 49);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // FormDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1152, 800);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard - Certificate Course";
            this.Load += new System.EventHandler(this.FormDashboard_Load);
            this.Resize += new System.EventHandler(this.FormDashboard_Resize);
            this.panelTop.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.buttonLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.TableLayoutPanel buttonLayout;
        private System.Windows.Forms.Button btnMengelolaMateri;
        private System.Windows.Forms.Button btnMengerjakanKuis;
        private System.Windows.Forms.Button btnMengelolaKuis;
        private System.Windows.Forms.Button btnSertifikat;
        private System.Windows.Forms.Button btnLogout;
    }
}
