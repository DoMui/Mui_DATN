namespace DATN
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtChontep = new System.Windows.Forms.TextBox();
            this.btnChontep = new System.Windows.Forms.Button();
            this.btnSinhtest = new System.Windows.Forms.Button();
            this.btnChayTest = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.trackBarTestCount = new System.Windows.Forms.TrackBar();
            this.lblTestCount = new System.Windows.Forms.Label();
            this.cbbDL = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTestCount)).BeginInit();
            this.SuspendLayout();
            // 
            // txtChontep
            // 
            this.txtChontep.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChontep.Location = new System.Drawing.Point(156, 33);
            this.txtChontep.Multiline = true;
            this.txtChontep.Name = "txtChontep";
            this.txtChontep.Size = new System.Drawing.Size(921, 47);
            this.txtChontep.TabIndex = 0;
            this.txtChontep.TextChanged += new System.EventHandler(this.txtChontep_TextChanged);
            // 
            // btnChontep
            // 
            this.btnChontep.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnChontep.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChontep.ForeColor = System.Drawing.Color.Transparent;
            this.btnChontep.Location = new System.Drawing.Point(1200, 18);
            this.btnChontep.Name = "btnChontep";
            this.btnChontep.Size = new System.Drawing.Size(285, 67);
            this.btnChontep.TabIndex = 1;
            this.btnChontep.Text = "CHỌN TỆP";
            this.btnChontep.UseVisualStyleBackColor = false;
            this.btnChontep.Click += new System.EventHandler(this.btnChontep_Click);
            // 
            // btnSinhtest
            // 
            this.btnSinhtest.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnSinhtest.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSinhtest.ForeColor = System.Drawing.Color.Transparent;
            this.btnSinhtest.Location = new System.Drawing.Point(1200, 167);
            this.btnSinhtest.Name = "btnSinhtest";
            this.btnSinhtest.Size = new System.Drawing.Size(260, 95);
            this.btnSinhtest.TabIndex = 2;
            this.btnSinhtest.Text = "Sinh testcase";
            this.btnSinhtest.UseVisualStyleBackColor = false;
            this.btnSinhtest.Click += new System.EventHandler(this.btnSinhtest_Click);
            // 
            // btnChayTest
            // 
            this.btnChayTest.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnChayTest.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChayTest.ForeColor = System.Drawing.Color.Transparent;
            this.btnChayTest.Location = new System.Drawing.Point(1489, 167);
            this.btnChayTest.Name = "btnChayTest";
            this.btnChayTest.Size = new System.Drawing.Size(286, 95);
            this.btnChayTest.TabIndex = 3;
            this.btnChayTest.Text = "Chạy Tests";
            this.btnChayTest.UseVisualStyleBackColor = false;
            this.btnChayTest.Click += new System.EventHandler(this.btnChayTest_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(32, 380);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1803, 598);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // trackBarTestCount
            // 
            this.trackBarTestCount.Location = new System.Drawing.Point(491, 234);
            this.trackBarTestCount.Maximum = 15;
            this.trackBarTestCount.Minimum = 1;
            this.trackBarTestCount.Name = "trackBarTestCount";
            this.trackBarTestCount.Size = new System.Drawing.Size(279, 69);
            this.trackBarTestCount.TabIndex = 6;
            this.trackBarTestCount.Value = 2;
            this.trackBarTestCount.Scroll += new System.EventHandler(this.trackBarTestCount_Scroll);
            // 
            // lblTestCount
            // 
            this.lblTestCount.AutoSize = true;
            this.lblTestCount.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestCount.Location = new System.Drawing.Point(868, 222);
            this.lblTestCount.Name = "lblTestCount";
            this.lblTestCount.Size = new System.Drawing.Size(209, 27);
            this.lblTestCount.TabIndex = 7;
            this.lblTestCount.Text = "Số lượng test case: 2";
            // 
            // cbbDL
            // 
            this.cbbDL.FormattingEnabled = true;
            this.cbbDL.Items.AddRange(new object[] {
            "RANDOM",
            "NHẬP TỪ BÀN PHÍM",
            "TẢI DỮ LIỆU TỪ FILE"});
            this.cbbDL.Location = new System.Drawing.Point(32, 234);
            this.cbbDL.Name = "cbbDL";
            this.cbbDL.Size = new System.Drawing.Size(344, 28);
            this.cbbDL.TabIndex = 8;
            this.cbbDL.SelectedIndexChanged += new System.EventHandler(this.cbbDL_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 27);
            this.label1.TabIndex = 10;
            this.label1.Text = "Chọn kiểu dữ liệu đầu vào:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(486, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(316, 27);
            this.label2.TabIndex = 11;
            this.label2.Text = "Số lượng testcase được sinh ra: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1879, 1018);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbDL);
            this.Controls.Add(this.lblTestCount);
            this.Controls.Add(this.trackBarTestCount);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnChayTest);
            this.Controls.Add(this.btnSinhtest);
            this.Controls.Add(this.btnChontep);
            this.Controls.Add(this.txtChontep);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "Form1";
            this.Text = "Sinh testcase";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTestCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtChontep;
        private System.Windows.Forms.Button btnChontep;
        private System.Windows.Forms.Button btnSinhtest;
        private System.Windows.Forms.Button btnChayTest;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TrackBar trackBarTestCount;
        private System.Windows.Forms.Label lblTestCount;
        private System.Windows.Forms.ComboBox cbbDL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}