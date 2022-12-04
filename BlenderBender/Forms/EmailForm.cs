using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlenderBender.Class;

namespace BlenderBender
{
    public partial class EmailForm : Form
    {
        public Form mf;
        public UserClass user;
        public DateClass dtto;
        public string tod = "καλησπέρα σας.";
        public string tod2 = "καλησπέρα σας.";
        public string emailmsg = "\r\n\r\nΘα θέλαμε να σας ενημερώσουμε ότι η παραγγελία σας βρίσκεται στο κατάστημά μας.\r\nΜπορείτε να περάσετε να την παραλάβετε.\r\n";
        public EmailForm(Form mf)
        {
            InitializeComponent();
            this.mf = mf;
        }
        public string lastclip = null;
        public string lastorder = null;
        public string lastemail = null;
        public string lastname = null;
        public string lastprice = null;
        public int _ccounter = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_emailaid.Checked == true)
            {
                var iData = Clipboard.GetDataObject();
                if ((Clipboard.GetDataObject() != null) && ((string)iData.GetData(DataFormats.Text) != lastclip))
                {
                    // Is Data Text?
                    if (iData.GetDataPresent(DataFormats.Text))
                    {
                        string data = (string)iData.GetData(DataFormats.Text);
                        string gtext = Clipboard.GetText(TextDataFormat.UnicodeText);
                        //Console.WriteLine(data);
                        //if ((data.Contains("---")) && (data.Contains("_")))
                        if (Regex.IsMatch(data, @"^\d{2}_\d{2}_\d{2}-\d{2}_\d{2}_\d{2}---\d{1,3}_\d{1,3}_\d{1,3}_\d{1,3}$") || Regex.IsMatch(data, @"^\d{3}-\d{2}_\d{2}_\d{2}-\d{2}_\d{2}_\d{2}---\d{1,3}_\d{1,3}_\d{1,3}_\d{1,3}$"))
                        {
                            if (data != lastorder)
                            {
                                textBox54.Text = data;
                                lastorder = data;
                                _ccounter += 1;
                            }
                        }
                        if (data.Contains("@"))
                        {
                            if (Regex.IsMatch(data, @"^.+\@.+\.\w{2,3}$"))
                            {
                                if (data != lastemail)
                                {
                                    textBox53.Text = data;
                                    lastemail = data;
                                    _ccounter += 1;
                                }
                            }
                        }
                        if ((!data.Contains("---")) && (!data.Contains("_")) && (!data.Contains("@")))
                        {
                            if (Regex.IsMatch(gtext, @"^\w+$"))
                            {
                                if (gtext != lastname)
                                {
                                    textBox55.Text = gtext;
                                    lastname = gtext;
                                    _ccounter += 1;
                                }
                            }
                        }
                        lastclip = (string)iData.GetData(DataFormats.Text);
                        if (_ccounter == 3)
                        {
                            this.WindowState = FormWindowState.Normal;
                            this.BringToFront();
                            this.TopMost = true;
                            this.Focus();
                            _ccounter = 0;
                            _emailaid.Checked = false;
                            this.TopMost = false;
                        }
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(user.GetRegKey<string>("MAIL_ADDRESS")))
            {
                if (signChk.Checked) emailmsg += Properties.Settings.Default.Signature;
                var message = comboBox3.Text + " " + textBox55.Text + " " + tod2 + " " + emailmsg;
                System.Console.WriteLine(message);
                if (user.GetRegKey<bool>("REPLACE_ON_MAIL"))
                {
                    message = message.Replace("\r\n", "%0D%0A");
                    message = message.Replace(" ", "%20");
                }

                var mailing_add = textBox53.Text;
                richTextBox5.Text = "E-mail Address:" + mailing_add + "\r\n" +
                                    "Παραγγελία: " + textBox54.Text + "\r\n" +
                                    $"{message}";
                var mailto = string.Format("mailto:{0}?Subject={1}&BCC={2}&Body={3}", mailing_add,
                    "Παραγγελία: " + textBox54.Text, user.GetRegKey<string>("MAIL_ADDRESS"), message);
                Process.Start(mailto);
                button28.PerformClick();
                _emailaid.Checked = false;
            }
            else
            {
                MessageBox.Show("Δεν έχει οριστεί το e-mail του καταστήματος. Go to Settings -> E-mail address");
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
                Clipboard.SetText($"**Αποτυχία 1ου SMS - Αποστάλθηκε E-mail {user.DateTimeNUser()}");
            if (radioButton6.Checked)
            {
                int extra = (int)extraUpDown.Value;
                string doh = dtto.DateTo("excludeSunday", extra);
                Clipboard.SetText($"**Αποστάλθηκε 2o E-mail {user.DateTimeNUser()} ότι θα παραμείνει μέχρι και {doh}");
            }
            if (radioButton7.Checked)
                Clipboard.SetText($"**Αποτυχία 1ου SMS - Αποστάλθηκε E-mail {user.DateTimeNUser()}");
            if (radioButton8.Checked)
                Clipboard.SetText($"**Αποστάλθηκε E-mail υπενθύμισης {user.DateTimeNUser()}");
            if (radioButton9.Checked)
                Clipboard.SetText($"**Αποστάλθηκε E-mail ώστε να επικοινωνήσει ο πελάτης μαζί μας. {user.DateTimeNUser()}");
            if (radioButton1.Checked)
                Clipboard.SetText($"**Αποστάλθηκε E-mail ώστε να επικοινωνήσει ο πελάτης μαζί μας (διευκρινίσεις). {user.DateTimeNUser()}");
            if (radioButton12.Checked)
                Clipboard.SetText($"**Αποστάλθηκε E-mail ώστε να επικοινωνήσει ο πελάτης μαζί μας. {user.DateTimeNUser()}");
        }
    }
}
