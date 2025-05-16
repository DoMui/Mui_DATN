namespace DATN
{
    partial class Form2
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
            this.txtDLDV = new System.Windows.Forms.TextBox();
            this.btnSinhTest = new System.Windows.Forms.Button();
            this.lblDSPT = new System.Windows.Forms.Label();
            this.rtbDSPT = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtDLDV
            // 
            this.txtDLDV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDLDV.Location = new System.Drawing.Point(638, 83);
            this.txtDLDV.Multiline = true;
            this.txtDLDV.Name = "txtDLDV";
            this.txtDLDV.Size = new System.Drawing.Size(645, 414);
            this.txtDLDV.TabIndex = 0;
            this.txtDLDV.TextChanged += new System.EventHandler(this.txtDLDV_TextChanged);
            // 
            // btnSinhTest
            // 
            this.btnSinhTest.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnSinhTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSinhTest.ForeColor = System.Drawing.Color.Transparent;
            this.btnSinhTest.Location = new System.Drawing.Point(364, 534);
            this.btnSinhTest.Name = "btnSinhTest";
            this.btnSinhTest.Size = new System.Drawing.Size(481, 62);
            this.btnSinhTest.TabIndex = 2;
            this.btnSinhTest.Text = "Sinh Tests";
            this.btnSinhTest.UseVisualStyleBackColor = false;
            this.btnSinhTest.Click += new System.EventHandler(this.btnSinhTest_Click);
            // 
            // lblDSPT
            // 
            this.lblDSPT.AutoSize = true;
            this.lblDSPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDSPT.Location = new System.Drawing.Point(29, 40);
            this.lblDSPT.Name = "lblDSPT";
            this.lblDSPT.Size = new System.Drawing.Size(192, 29);
            this.lblDSPT.TabIndex = 3;
            this.lblDSPT.Text = "Các phương thức";
            // 
            // rtbDSPT
            // 
            this.rtbDSPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbDSPT.Location = new System.Drawing.Point(33, 83);
            this.rtbDSPT.Name = "rtbDSPT";
            this.rtbDSPT.Size = new System.Drawing.Size(548, 414);
            this.rtbDSPT.TabIndex = 4;
            this.rtbDSPT.Text = "";
            this.rtbDSPT.TextChanged += new System.EventHandler(this.rtbDSPT_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(634, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nhập dữ liệu:";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1322, 630);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbDSPT);
            this.Controls.Add(this.lblDSPT);
            this.Controls.Add(this.btnSinhTest);
            this.Controls.Add(this.txtDLDV);
            this.Name = "Form2";
            this.Text = "Nhập dữ liệu testcase";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDLDV;
        private System.Windows.Forms.Button btnSinhTest;
        private System.Windows.Forms.Label lblDSPT;
        private System.Windows.Forms.RichTextBox rtbDSPT;
        private System.Windows.Forms.Label label1;
    }
}