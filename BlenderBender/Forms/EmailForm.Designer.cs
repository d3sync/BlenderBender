namespace BlenderBender
{
    partial class EmailForm
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.signChk = new System.Windows.Forms.CheckBox();
            this._emailaid = new System.Windows.Forms.CheckBox();
            this.button28 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.cmbExtraDays = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbEmailText = new System.Windows.Forms.ComboBox();
            this.textBox55 = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.textBox54 = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.textBox53 = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.signChk);
            this.splitContainer1.Panel1.Controls.Add(this._emailaid);
            this.splitContainer1.Panel1.Controls.Add(this.button28);
            this.splitContainer1.Panel1.Controls.Add(this.button18);
            this.splitContainer1.Panel1.Controls.Add(this.checkBox6);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox3);
            this.splitContainer1.Panel1.Controls.Add(this.button7);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox12);
            this.splitContainer1.Panel1.Controls.Add(this.textBox55);
            this.splitContainer1.Panel1.Controls.Add(this.label50);
            this.splitContainer1.Panel1.Controls.Add(this.textBox54);
            this.splitContainer1.Panel1.Controls.Add(this.label49);
            this.splitContainer1.Panel1.Controls.Add(this.textBox53);
            this.splitContainer1.Panel1.Controls.Add(this.label48);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox5);
            this.splitContainer1.Size = new System.Drawing.Size(784, 391);
            this.splitContainer1.SplitterDistance = 373;
            this.splitContainer1.TabIndex = 1;
            // 
            // signChk
            // 
            this.signChk.AutoSize = true;
            this.signChk.Location = new System.Drawing.Point(238, 125);
            this.signChk.Margin = new System.Windows.Forms.Padding(2);
            this.signChk.Name = "signChk";
            this.signChk.Size = new System.Drawing.Size(92, 19);
            this.signChk.TabIndex = 14;
            this.signChk.Text = "Υπογραφή";
            this.signChk.UseVisualStyleBackColor = true;
            // 
            // _emailaid
            // 
            this._emailaid.AutoSize = true;
            this._emailaid.Location = new System.Drawing.Point(0, 5);
            this._emailaid.Name = "_emailaid";
            this._emailaid.Size = new System.Drawing.Size(112, 19);
            this._emailaid.TabIndex = 13;
            this._emailaid.Text = "Clipboard Aid";
            this._emailaid.UseVisualStyleBackColor = true;
            this._emailaid.CheckedChanged += new System.EventHandler(this._emailaid_CheckedChanged);
            // 
            // button28
            // 
            this.button28.Location = new System.Drawing.Point(5, 350);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(361, 27);
            this.button28.TabIndex = 12;
            this.button28.Text = "Αυτόματο μήνυμα για Admin";
            this.button28.UseVisualStyleBackColor = true;
            this.button28.Click += new System.EventHandler(this.button28_Click);
            // 
            // button18
            // 
            this.button18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button18.Location = new System.Drawing.Point(334, 105);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(32, 32);
            this.button18.TabIndex = 11;
            this.button18.Text = "C";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox6.Location = new System.Drawing.Point(238, 110);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(83, 17);
            this.checkBox6.TabIndex = 5;
            this.checkBox6.Text = "Καλημέρα";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Αξιότιμε κύριε",
            "Αξιότιμη κυρία"});
            this.comboBox3.Location = new System.Drawing.Point(31, 106);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(201, 23);
            this.comboBox3.TabIndex = 4;
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(5, 283);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(361, 61);
            this.button7.TabIndex = 7;
            this.button7.Text = "Construct E-mail";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.cmbExtraDays);
            this.groupBox12.Controls.Add(this.flowLayoutPanel1);
            this.groupBox12.Location = new System.Drawing.Point(5, 139);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(362, 138);
            this.groupBox12.TabIndex = 6;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Επιλογές";
            // 
            // cmbExtraDays
            // 
            this.cmbExtraDays.DisplayMember = "0";
            this.cmbExtraDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExtraDays.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbExtraDays.FormattingEnabled = true;
            this.cmbExtraDays.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "5",
            "7",
            "10"});
            this.cmbExtraDays.Location = new System.Drawing.Point(305, 20);
            this.cmbExtraDays.Name = "cmbExtraDays";
            this.cmbExtraDays.Size = new System.Drawing.Size(57, 23);
            this.cmbExtraDays.TabIndex = 5;
            this.cmbExtraDays.ValueMember = "0";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmbEmailText);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 20);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(293, 112);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // cmbEmailText
            // 
            this.cmbEmailText.FormattingEnabled = true;
            this.cmbEmailText.Location = new System.Drawing.Point(3, 3);
            this.cmbEmailText.Name = "cmbEmailText";
            this.cmbEmailText.Size = new System.Drawing.Size(290, 23);
            this.cmbEmailText.TabIndex = 0;
            this.cmbEmailText.SelectedIndexChanged += new System.EventHandler(this.cmbEmailText_SelectedIndexChanged);
            // 
            // textBox55
            // 
            this.textBox55.Location = new System.Drawing.Point(117, 80);
            this.textBox55.Name = "textBox55";
            this.textBox55.Size = new System.Drawing.Size(250, 21);
            this.textBox55.TabIndex = 3;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(8, 83);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(98, 15);
            this.label50.TabIndex = 4;
            this.label50.Text = "Όνομα Πελάτη";
            // 
            // textBox54
            // 
            this.textBox54.Location = new System.Drawing.Point(117, 51);
            this.textBox54.Name = "textBox54";
            this.textBox54.Size = new System.Drawing.Size(250, 21);
            this.textBox54.TabIndex = 2;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(23, 54);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(83, 15);
            this.label49.TabIndex = 2;
            this.label49.Text = "Παραγγελία";
            // 
            // textBox53
            // 
            this.textBox53.Location = new System.Drawing.Point(117, 24);
            this.textBox53.Name = "textBox53";
            this.textBox53.Size = new System.Drawing.Size(250, 21);
            this.textBox53.TabIndex = 1;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(7, 27);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(99, 15);
            this.label48.TabIndex = 0;
            this.label48.Text = "Ε-mail Πελάτη";
            // 
            // richTextBox5
            // 
            this.richTextBox5.Location = new System.Drawing.Point(6, 6);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.ReadOnly = true;
            this.richTextBox5.Size = new System.Drawing.Size(382, 374);
            this.richTextBox5.TabIndex = 0;
            this.richTextBox5.Text = "";
            // 
            // EmailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 391);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EmailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Αποστολή Ηλεκτρονικής Αλληλογραφίας";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox signChk;
        private System.Windows.Forms.CheckBox _emailaid;
        private System.Windows.Forms.Button button28;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox textBox55;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TextBox textBox54;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox textBox53;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.RichTextBox richTextBox5;
        private System.Windows.Forms.ComboBox cmbExtraDays;
        private System.Windows.Forms.ComboBox cmbEmailText;
    }
}