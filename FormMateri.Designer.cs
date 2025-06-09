namespace ITCourseCertificateV001
{
    partial class FormMateri
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
            this.lblKursus = new System.Windows.Forms.Label();
            this.dgvMateri = new System.Windows.Forms.DataGridView();
            this.txtDeskripsi = new System.Windows.Forms.TextBox();
            this.btnKerjakanKuis = new System.Windows.Forms.Button();
            this.btnUpdateMateri = new System.Windows.Forms.Button();
            this.btnDeleteMateri = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateri)).BeginInit();
            this.SuspendLayout();
            // 
            // lblKursus
            // 
            this.lblKursus.AutoSize = true;
            this.lblKursus.Location = new System.Drawing.Point(12, 10);
            this.lblKursus.Name = "lblKursus";
            this.lblKursus.Size = new System.Drawing.Size(89, 20);
            this.lblKursus.TabIndex = 0;
            this.lblKursus.Text = "Judul Kursus";
            // 
            // dgvMateri
            // 
            this.dgvMateri.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMateri.Location = new System.Drawing.Point(20, 40);
            this.dgvMateri.Name = "dgvMateri";
            this.dgvMateri.RowHeadersWidth = 51;
            this.dgvMateri.Size = new System.Drawing.Size(760, 250);
            this.dgvMateri.TabIndex = 1;
            this.dgvMateri.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMateri_CellDoubleClick);
            // 
            // txtDeskripsi
            // 
            this.txtDeskripsi.Location = new System.Drawing.Point(20, 300);
            this.txtDeskripsi.Multiline = true;
            this.txtDeskripsi.Name = "txtDeskripsi";
            this.txtDeskripsi.Size = new System.Drawing.Size(760, 80);
            this.txtDeskripsi.TabIndex = 2;
            // 
            // btnKerjakanKuis
            // 
            this.btnKerjakanKuis.Location = new System.Drawing.Point(660, 400);
            this.btnKerjakanKuis.Name = "btnKerjakanKuis";
            this.btnKerjakanKuis.Size = new System.Drawing.Size(120, 40);
            this.btnKerjakanKuis.TabIndex = 5;
            this.btnKerjakanKuis.Text = "Kerjakan Kuis";
            this.btnKerjakanKuis.UseVisualStyleBackColor = true;
            this.btnKerjakanKuis.Click += new System.EventHandler(this.btnKerjakanKuis_Click);
            // 
            // btnUpdateMateri
            // 
            this.btnUpdateMateri.Location = new System.Drawing.Point(340, 400);
            this.btnUpdateMateri.Name = "btnUpdateMateri";
            this.btnUpdateMateri.Size = new System.Drawing.Size(120, 40);
            this.btnUpdateMateri.TabIndex = 4;
            this.btnUpdateMateri.Text = "Update Materi";
            this.btnUpdateMateri.UseVisualStyleBackColor = true;
            this.btnUpdateMateri.Click += new System.EventHandler(this.btnUpdateMateri_Click);
            // 
            // btnDeleteMateri
            // 
            this.btnDeleteMateri.Location = new System.Drawing.Point(20, 400);
            this.btnDeleteMateri.Name = "btnDeleteMateri";
            this.btnDeleteMateri.Size = new System.Drawing.Size(120, 40);
            this.btnDeleteMateri.TabIndex = 3;
            this.btnDeleteMateri.Text = "Hapus Materi";
            this.btnDeleteMateri.UseVisualStyleBackColor = true;
            this.btnDeleteMateri.Click += new System.EventHandler(this.btnDeleteMateri_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBack.BackColor = System.Drawing.Color.LightGray;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBack.Location = new System.Drawing.Point(350, 500);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(120, 35);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "← Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // FormMateri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnKerjakanKuis);
            this.Controls.Add(this.btnUpdateMateri);
            this.Controls.Add(this.btnDeleteMateri);
            this.Controls.Add(this.txtDeskripsi);
            this.Controls.Add(this.dgvMateri);
            this.Controls.Add(this.lblKursus);
            
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FormMateri";
            this.Text = "Materi Kursus";
            this.Load += new System.EventHandler(this.FormMateri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateri)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
         
        }

        #endregion

        private System.Windows.Forms.Label lblKursus;
        private System.Windows.Forms.DataGridView dgvMateri;
        private System.Windows.Forms.TextBox txtDeskripsi;
        private System.Windows.Forms.Button btnKerjakanKuis;
        private System.Windows.Forms.Button btnUpdateMateri;
        private System.Windows.Forms.Button btnDeleteMateri;
        private System.Windows.Forms.Button btnBack;
    }
}
