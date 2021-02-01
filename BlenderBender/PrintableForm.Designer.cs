namespace BlenderBender
{
    partial class PrintableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintableForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this._txtTo = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._txtAA = new System.Windows.Forms.TextBox();
            this._txtDate = new System.Windows.Forms.TextBox();
            this.txtAddressTo = new System.Windows.Forms.TextBox();
            this.txtAddressFrom = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.button1 = new System.Windows.Forms.Button();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this._labelNprint = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this._txtPhone = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this._txtTotal = new System.Windows.Forms.NumericUpDown();
            this._txtCurrent = new System.Windows.Forms.NumericUpDown();
            this._pall = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._txtTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtCurrent)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(59, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Αποστολή από";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(676, 526);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 37);
            this.label2.TabIndex = 1;
            this.label2.Text = "Προς:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtFrom
            // 
            this.txtFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrom.Location = new System.Drawing.Point(299, 58);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(136, 37);
            this.txtFrom.TabIndex = 2;
            // 
            // _txtTo
            // 
            this._txtTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._txtTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtTo.Location = new System.Drawing.Point(785, 526);
            this._txtTo.Name = "_txtTo";
            this._txtTo.Size = new System.Drawing.Size(123, 37);
            this._txtTo.TabIndex = 3;
            this._txtTo.Text = "Μενίδι";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(6, 20);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(498, 119);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(686, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 29);
            this.label3.TabIndex = 5;
            this.label3.Text = "Αριθμός Αποστολής";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(686, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 29);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ημερομηνία";
            // 
            // _txtAA
            // 
            this._txtAA.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._txtAA.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtAA.Location = new System.Drawing.Point(940, 102);
            this._txtAA.Name = "_txtAA";
            this._txtAA.Size = new System.Drawing.Size(149, 28);
            this._txtAA.TabIndex = 7;
            this._txtAA.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // _txtDate
            // 
            this._txtDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtDate.Location = new System.Drawing.Point(940, 58);
            this._txtDate.Name = "_txtDate";
            this._txtDate.Size = new System.Drawing.Size(149, 28);
            this._txtDate.TabIndex = 8;
            // 
            // txtAddressTo
            // 
            this.txtAddressTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddressTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddressTo.Location = new System.Drawing.Point(523, 566);
            this.txtAddressTo.Name = "txtAddressTo";
            this.txtAddressTo.Size = new System.Drawing.Size(570, 37);
            this.txtAddressTo.TabIndex = 9;
            this.txtAddressTo.Text = "ΑΝΕΜΩΝΗΣ 6. ΤΚ 13671, ΑΧΑΡΝΑΙ";
            this.txtAddressTo.TextChanged += new System.EventHandler(this.txtAddressTo_TextChanged);
            // 
            // txtAddressFrom
            // 
            this.txtAddressFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddressFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddressFrom.Location = new System.Drawing.Point(66, 102);
            this.txtAddressFrom.Name = "txtAddressFrom";
            this.txtAddressFrom.Size = new System.Drawing.Size(486, 37);
            this.txtAddressFrom.TabIndex = 10;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(783, 134);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(199, 86);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 34);
            this.button1.TabIndex = 12;
            this.button1.Text = "Print";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this._txtPhone);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.txtFrom);
            this.panel1.Controls.Add(this.txtAddressFrom);
            this.panel1.Controls.Add(this._txtTo);
            this.panel1.Controls.Add(this.txtAddressTo);
            this.panel1.Controls.Add(this._txtDate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this._txtAA);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1158, 800);
            this.panel1.TabIndex = 13;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this._labelNprint);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(439, 665);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(312, 70);
            this.panel2.TabIndex = 15;
            // 
            // _labelNprint
            // 
            this._labelNprint.AutoSize = true;
            this._labelNprint.Location = new System.Drawing.Point(174, 18);
            this._labelNprint.Name = "_labelNprint";
            this._labelNprint.Size = new System.Drawing.Size(123, 37);
            this._labelNprint.TabIndex = 16;
            this._labelNprint.Text = "__ / __";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(12, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(156, 37);
            this.textBox1.TabIndex = 14;
            this.textBox1.Text = "ΔΕΜΑΤΑ:";
            // 
            // _txtPhone
            // 
            this._txtPhone.AutoSize = true;
            this._txtPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtPhone.Location = new System.Drawing.Point(59, 142);
            this._txtPhone.Name = "_txtPhone";
            this._txtPhone.Size = new System.Drawing.Size(98, 37);
            this._txtPhone.TabIndex = 13;
            this._txtPhone.Text = "ΤΗΛ.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(91, 236);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 171);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "(Σφραγίδα και Υπογραφή)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(195, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 24);
            this.label5.TabIndex = 14;
            this.label5.Text = "Τρέχ. Δέμα:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(406, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 24);
            this.label6.TabIndex = 16;
            this.label6.Text = "Σ. Δεμάτων:";
            // 
            // _txtTotal
            // 
            this._txtTotal.Location = new System.Drawing.Point(540, 10);
            this._txtTotal.Name = "_txtTotal";
            this._txtTotal.Size = new System.Drawing.Size(52, 29);
            this._txtTotal.TabIndex = 17;
            this._txtTotal.ValueChanged += new System.EventHandler(this._txtTotal_ValueChanged);
            // 
            // _txtCurrent
            // 
            this._txtCurrent.Location = new System.Drawing.Point(320, 10);
            this._txtCurrent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._txtCurrent.Name = "_txtCurrent";
            this._txtCurrent.Size = new System.Drawing.Size(52, 29);
            this._txtCurrent.TabIndex = 18;
            this._txtCurrent.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._txtCurrent.ValueChanged += new System.EventHandler(this._txtCurrent_ValueChanged);
            // 
            // _pall
            // 
            this._pall.AutoSize = true;
            this._pall.Enabled = false;
            this._pall.Location = new System.Drawing.Point(137, 18);
            this._pall.Name = "_pall";
            this._pall.Size = new System.Drawing.Size(15, 14);
            this._pall.TabIndex = 19;
            this._pall.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this._pall);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this._txtCurrent);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this._txtTotal);
            this.panel3.Location = new System.Drawing.Point(3, -1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1155, 56);
            this.panel3.TabIndex = 20;
            // 
            // PrintableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1158, 861);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "PrintableForm";
            this.Text = "PrintableForm";
            this.Load += new System.EventHandler(this.PrintableForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._txtTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtCurrent)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox _txtTo;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _txtAA;
        private System.Windows.Forms.TextBox _txtDate;
        private System.Windows.Forms.TextBox txtAddressTo;
        private System.Windows.Forms.TextBox txtAddressFrom;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label _txtPhone;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label _labelNprint;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown _txtTotal;
        private System.Windows.Forms.NumericUpDown _txtCurrent;
        private System.Windows.Forms.CheckBox _pall;
        private System.Windows.Forms.Panel panel3;
    }
}