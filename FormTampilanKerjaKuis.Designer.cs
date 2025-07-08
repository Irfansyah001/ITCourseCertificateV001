namespace ITCourseCertificateV001
{
    partial class FormTampilanKerjaKuis
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblSoalTampil = new System.Windows.Forms.Label();
            this.groupBoxJawaban = new System.Windows.Forms.GroupBox();
            this.radioButtonD = new System.Windows.Forms.RadioButton();
            this.radioButtonC = new System.Windows.Forms.RadioButton();
            this.radioButtonB = new System.Windows.Forms.RadioButton();
            this.radioButtonA = new System.Windows.Forms.RadioButton();
            this.btnMulai = new System.Windows.Forms.Button();
            this.btnSelanjutnya = new System.Windows.Forms.Button();
            this.btnKembali = new System.Windows.Forms.Button();
            this.lblTimerKuis = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxJawaban.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.comboBox1, 2);
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Location = new System.Drawing.Point(3, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(714, 28);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lblSoalTampil
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.lblSoalTampil, 3);
            this.lblSoalTampil.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoalTampil.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSoalTampil.Location = new System.Drawing.Point(3, 40);
            this.lblSoalTampil.Name = "lblSoalTampil";
            this.lblSoalTampil.Size = new System.Drawing.Size(894, 40);
            this.lblSoalTampil.TabIndex = 2;
            this.lblSoalTampil.Text = "Klik \'Mulai\' untuk mengerjakan kuis.";
            this.lblSoalTampil.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxJawaban
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.groupBoxJawaban, 3);
            this.groupBoxJawaban.Controls.Add(this.radioButtonD);
            this.groupBoxJawaban.Controls.Add(this.radioButtonC);
            this.groupBoxJawaban.Controls.Add(this.radioButtonB);
            this.groupBoxJawaban.Controls.Add(this.radioButtonA);
            this.groupBoxJawaban.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxJawaban.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxJawaban.Location = new System.Drawing.Point(3, 83);
            this.groupBoxJawaban.Name = "groupBoxJawaban";
            this.groupBoxJawaban.Size = new System.Drawing.Size(894, 374);
            this.groupBoxJawaban.TabIndex = 3;
            this.groupBoxJawaban.TabStop = false;
            this.groupBoxJawaban.Text = "Pilih Jawaban Anda";
            // 
            // radioButtonD
            // 
            this.radioButtonD.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonD.Location = new System.Drawing.Point(3, 98);
            this.radioButtonD.Name = "radioButtonD";
            this.radioButtonD.Size = new System.Drawing.Size(888, 24);
            this.radioButtonD.TabIndex = 0;
            this.radioButtonD.Text = "Opsi pilihan D akan muncul di sini";
            this.radioButtonD.CheckedChanged += new System.EventHandler(this.radioButtonD_CheckedChanged);
            // 
            // radioButtonC
            // 
            this.radioButtonC.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonC.Location = new System.Drawing.Point(3, 74);
            this.radioButtonC.Name = "radioButtonC";
            this.radioButtonC.Size = new System.Drawing.Size(888, 24);
            this.radioButtonC.TabIndex = 1;
            this.radioButtonC.Text = "Opsi pilihan C akan muncul di sini";
            this.radioButtonC.CheckedChanged += new System.EventHandler(this.radioButtonC_CheckedChanged);
            // 
            // radioButtonB
            // 
            this.radioButtonB.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonB.Location = new System.Drawing.Point(3, 50);
            this.radioButtonB.Name = "radioButtonB";
            this.radioButtonB.Size = new System.Drawing.Size(888, 24);
            this.radioButtonB.TabIndex = 2;
            this.radioButtonB.Text = "Opsi pilihan B akan muncul di sini";
            this.radioButtonB.CheckedChanged += new System.EventHandler(this.radioButtonB_CheckedChanged);
            // 
            // radioButtonA
            // 
            this.radioButtonA.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonA.Location = new System.Drawing.Point(3, 26);
            this.radioButtonA.Name = "radioButtonA";
            this.radioButtonA.Size = new System.Drawing.Size(888, 24);
            this.radioButtonA.TabIndex = 3;
            this.radioButtonA.Text = "Opsi pilihan A akan muncul di sini";
            this.radioButtonA.CheckedChanged += new System.EventHandler(this.radioButtonA_CheckedChanged);
            // 
            // btnMulai
            // 
            this.btnMulai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMulai.Location = new System.Drawing.Point(723, 3);
            this.btnMulai.Name = "btnMulai";
            this.btnMulai.Size = new System.Drawing.Size(174, 34);
            this.btnMulai.TabIndex = 1;
            this.btnMulai.Text = "Mulai";
            this.btnMulai.Click += new System.EventHandler(this.btnMulai_Click);
            // 
            // btnSelanjutnya
            // 
            this.btnSelanjutnya.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelanjutnya.Location = new System.Drawing.Point(183, 503);
            this.btnSelanjutnya.Name = "btnSelanjutnya";
            this.btnSelanjutnya.Size = new System.Drawing.Size(534, 44);
            this.btnSelanjutnya.TabIndex = 5;
            this.btnSelanjutnya.Text = "Selanjutnya";
            this.btnSelanjutnya.Click += new System.EventHandler(this.btnSelanjutnya_Click);
            // 
            // btnKembali
            // 
            this.btnKembali.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnKembali.Location = new System.Drawing.Point(3, 503);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(174, 44);
            this.btnKembali.TabIndex = 4;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // lblTimerKuis
            // 
            this.lblTimerKuis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTimerKuis.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTimerKuis.Location = new System.Drawing.Point(723, 500);
            this.lblTimerKuis.Name = "lblTimerKuis";
            this.lblTimerKuis.Size = new System.Drawing.Size(174, 50);
            this.lblTimerKuis.TabIndex = 6;
            this.lblTimerKuis.Text = "01:00";
            this.lblTimerKuis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelMain.Controls.Add(this.comboBox1, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.btnMulai, 2, 0);
            this.tableLayoutPanelMain.Controls.Add(this.lblSoalTampil, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxJawaban, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.btnKembali, 0, 4);
            this.tableLayoutPanelMain.Controls.Add(this.btnSelanjutnya, 1, 4);
            this.tableLayoutPanelMain.Controls.Add(this.lblTimerKuis, 2, 4);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 6;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(900, 600);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // FormTampilanKerjaKuis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormTampilanKerjaKuis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kerjakan Kuis";
            this.Load += new System.EventHandler(this.FormTampilanKerjaKuis_Load);
            this.groupBoxJawaban.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblSoalTampil;
        private System.Windows.Forms.GroupBox groupBoxJawaban;
        private System.Windows.Forms.RadioButton radioButtonA;
        private System.Windows.Forms.RadioButton radioButtonB;
        private System.Windows.Forms.RadioButton radioButtonC;
        private System.Windows.Forms.RadioButton radioButtonD;
        private System.Windows.Forms.Button btnMulai;
        private System.Windows.Forms.Button btnSelanjutnya;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.Label lblTimerKuis;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
    }
}
