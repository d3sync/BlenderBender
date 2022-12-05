using System;
using System.Collections;
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
using Microsoft.Extensions.Configuration;
using Humanizer;

namespace BlenderBender
{
    public partial class EmailForm : Form
    {
        public Form mf;
        public UserClass user = new UserClass();
        public DateClass dtto = new DateClass();
        public string tod2 = "καλησπέρα σας.";
        public string emailmsg = "\r\n\r\nΘα θέλαμε να σας ενημερώσουμε ότι η παραγγελία σας βρίσκεται στο κατάστημά μας.\r\nΜπορείτε να περάσετε να την παραλάβετε.\r\n";
        public EmailForm(Form mf)
        {
            InitializeComponent();
            PopulateCmbWithPresets();
            this.mf = mf;
            cmbExtraDays.SelectedIndex = 0;
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

        private void PopulateCmbWithPresets()
        {
            //Reading from appsettings file
            var config = new ConfigurationBuilder()
                .AddJsonFile(@"default.json", optional: false)
                .Build();
            //return config.GetValue<string>("Logging:FilePath");
            try
            {
                var data = config.GetRequiredSection("EmailPresets").AsEnumerable();
                var i = 0;
                foreach (var item in data)
                {
                    if (item.Key != "EmailPresets")
                    {
                        var d = new ComboboxItem();
                        d.Text = item.Key.Replace("EmailPresets:",$"[{i}]Preset:").Humanize().Transform(To.TitleCase);
                        d.Value = item.Value;
                        cmbEmailText.Items.Add(d);
                        Console.WriteLine("{0} - {1}", d.Text, d.Value);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(user.GetRegKey<String>("MAIL_ADDRESS")))
            {

                var message = comboBox3.Text + " " + textBox55.Text + " " + tod2 + " " + emailmsg;
                System.Console.WriteLine(message);
                if (user.GetRegKey<bool>("REPLACE_ON_MAIL"))
                {
                    message = message.Replace("\r\n", "%0D%0A");
                    message = message.Replace(" ", "%20");
                    message = message.Replace("[Phone]", user.GetRegKey<String>("Phone"));
                    message = message.Replace("[fdate]", MakeDate());
                    message = message.Replace("[user]", user.CurrentUser());
                    if (signChk.Checked) message += Properties.Settings.Default.Signature;
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

        private string MakeDate()
        {
            int extra = 0;
            extra += Int32.Parse(cmbExtraDays.SelectedItem.ToString());
            string doh = dtto.DateTo("excludeSunday", extra);
            return doh;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            int extra = 0;
            extra += Int32.Parse(cmbExtraDays.SelectedItem.ToString());
            string doh = dtto.DateTo("excludeSunday", extra);
            switch (cmbEmailText.SelectedIndex)
            {
                case 4:
                    Clipboard.SetText($"**Αποτυχία 1ου SMS - Αποστάλθηκε E-mail {user.DateTimeNUser()}");
                    break;
                case 3:
                    Clipboard.SetText($"**Αποτυχία 1ου SMS - Αποστάλθηκε E-mail {user.DateTimeNUser()}");
                    break;
                case 1:
                    Clipboard.SetText(
                        $"**Αποστάλθηκε 2o E-mail {user.DateTimeNUser()} ότι θα παραμείνει μέχρι και {doh}");
                    break;
                case 0:
                    Clipboard.SetText($"**Αποστάλθηκε E-mail υπενθύμισης {user.DateTimeNUser()}");
                    break;
                case 6:
                    Clipboard.SetText(
                        $"**Αποστάλθηκε E-mail ώστε να επικοινωνήσει ο πελάτης μαζί μας. {user.DateTimeNUser()}");
                    break;
                case 5:
                    Clipboard.SetText(
                        $"**Αποστάλθηκε E-mail ώστε να επικοινωνήσει ο πελάτης μαζί μας. {user.DateTimeNUser()}");
                    break;
                case 2:
                    Clipboard.SetText(
                        $"**Αποστάλθηκε E-mail ώστε να επικοινωνήσει ο πελάτης μαζί μας (διευκρινίσεις). {user.DateTimeNUser()}");
                    break;
                default:
                    Clipboard.SetText(
                        $"**Αποστάλθηκε E-mail {cmbEmailText.SelectedItem.ToString()} {user.DateTimeNUser()}");
                    break;
            }
        }

        public void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = controls =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void button18_Click(object sender, EventArgs e) => ClearTextBoxes();

        private void _emailaid_CheckedChanged(object sender, EventArgs e)
        {
            Clipboard.Clear();
            lastname = lastemail = lastorder = lastclip = null;
            _ccounter = 0;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
                tod2 = "καλημέρα σας.";
            else
                tod2 = "καλησπέρα σας.";
        }

        private void cmbEmailText_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboboxItem cb = cmbEmailText.SelectedItem as ComboboxItem;
            emailmsg = cb.Value.ToString();
        }
    }
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
