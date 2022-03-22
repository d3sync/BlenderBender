using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;


//using ControlzEx.Standard;

namespace BlenderBender
{
    public partial class Form1 : Form
    {
        public string emailmsg = "\r\n\r\nΘα θέλαμε να σας ενημερώσουμε ότι η παραγγελία σας βρίσκεται στο κατάστημά μας.\r\nΜπορείτε να περάσετε να την παραλάβετε.\r\n";
        private int hold;
        public int i = 0;
        public double sum;
        public string tod = "καλησπέρα σας.";
        public string tod2 = "καλησπέρα σας.";
        DateClass dtto = new DateClass();
        private About about;
        public CultureInfo cCulture = CultureInfo.CurrentCulture;
        public NumberStyles nStyles = NumberStyles.AllowDecimalPoint;
        Cliptool tada;
        public Form1()
        {
            InitializeComponent();
            about = new About();
            fileSystemWatcher1.EnableRaisingEvents = filemonitor.Checked = Properties.Settings.Default.filemonitor;
            label40.Text = Properties.Settings.Default.monitorfolder;
            fileSystemWatcher1.Path = Path.GetFullPath(Properties.Settings.Default.monitorfolder);
            var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\e-ShopAssistant");
            if (key != null)
            {
                if (key.GetValue("Phone") != null)
                    textBox43.Text = key.GetValue("Phone").ToString();
                else
                    textBox43.Text = "2115000500";
                if (key.GetValue("kava") != null)
                    textBox45.Text = textBox46.Text = key.GetValue("kava").ToString();
                else
                    textBox45.Text = textBox46.Text = "0";
                if (key.GetValue("ESHOP_SHOP") != null)
                    textBox48.Text = key.GetValue("ESHOP_SHOP").ToString();
                else
                    textBox48.Text = "ΚΑΤΑΣΤΗΜΑ ΡΟΔΟΥ";
                if (key.GetValue("ESHOP_ONE") != null)
                    textBox6.Text = key.GetValue("ESHOP_ONE").ToString();
                else
                    textBox6.Text = " - Η ΠΑΡΑΓΓΕΛΙΑ ΣΑΣ ΕΙΝΑΙ ΕΤΟΙΜΗ. ΜΠΟΡΕΙΤΕ ΝΑ ΠΕΡΑΣΕΤΕ ΝΑ ΤΗΝ ΠΑΡΑΛΑΒΕΤΕ.";
                if (key.GetValue("Phone") != null)
                    textBox43.Text = key.GetValue("Phone").ToString();
                else
                    textBox43.Text = "2115000500";
                if (key.GetValue("MAIL_ADDRESS") != null) textBox47.Text = key.GetValue("MAIL_ADDRESS").ToString();
                if (key.GetValue("REPLACE_ON_MAIL") != null)
                    checkBox8.Checked = Convert.ToBoolean(key.GetValue("REPLACE_ON_MAIL"));
                else
                    checkBox8.Checked = true;

                key.Close();
            }
            currentUser.Text = Properties.Settings.Default.User;
            numericUpDown1.Value = Properties.Settings.Default.BookDays;
            richTextBox3.Text = Properties.Settings.Default.Signature;
            tabPage7.Visible = true;
            //tabControl1.TabPages.Remove(tabPage7);

            var asm = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            var version = string.Format("{0}", fvi.ProductVersion);
            label31.Text = version;
            //end of e-mail settings
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                toolStripStatusLabel2.Text = GetLocalIPAddress();
            }

            //_txtFrom.Text = Properties.Settings.Default._storeArea;
            _storeAddress.Text = Properties.Settings.Default._storeAddress;
        }
        public int countdown { get; private set; }

        public string CurrentUser()
        {
            string _user = "";
            if (Properties.Settings.Default.User != "")
            {
                _user = Properties.Settings.Default.User;
            }
            return _user;
        }

        public string DateTimeNUser()
        {
            string _userc = CurrentUser();
            if (_userc == "") { _userc = "Άγνωστος Χειριστής"; }
            return $"{DateTime.Now.ToString("dd/MM HH:mm")}/({_userc})";
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        private void notifier(string message)
        {
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "e-Shop Assistant";
            notifyIcon1.BalloonTipText = "Αντιγράφθηκε στο Πρόχειρο: " + message;
            notifyIcon1.ShowBalloonTip(2000);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "") Clipboard.SetText(richTextBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "") Clipboard.SetText(richTextBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox2.Text = "";
            if (checkBox1.Checked)
            {
                if (textBox2.Text != "")
                    richTextBox1.Text =
                        $"$$Το πιστωτικό αξίας {textBox2.Text} ευρώ εκπληρώθηκε με επιστροφή μετρητών. $$";
                else
                    MessageBox.Show("Δεν έχετε εισάγει επαρκεί δεδομένα.");
            }
            else
            {
                if (textBox4.Text != "" && textBox2.Text != "")
                {
                    var diff = double.Parse(textBox4.Text, nStyles, cCulture) -
                               double.Parse(textBox2.Text, nStyles, cCulture);

                    if (diff == 0)
                    {
                        richTextBox1.Text =
                            $"**Το πιστωτικό αξίας {textBox2.Text} ευρώ εκπληρώθηκε στην {textBox3.Text} .## Δεν απομένει υπόλοιπο.##";
                        richTextBox2.Text =
                            $"**Εδώ εκπληρώθηκε το πιστωτικό {textBox1.Text} αξίας {textBox2.Text} ευρώ.**";
                    }
                    else if (diff > 0)
                    {
                        if (radioButton3.Checked)
                        {
                            richTextBox1.Text =
                                $"**Το πιστωτικό αξίας {textBox2.Text} ευρώ εκπληρώθηκε στην {textBox3.Text}.**";
                            richTextBox2.Text =
                                $"**Εδώ εκπληρώθηκε το πιστωτικό {textBox1.Text} αξίας {textBox2.Text} ευρώ. ";
                            if (checkBox4.Checked)
                                richTextBox2.Text = richTextBox2.Text +
                                                    $"## Η διαφορά {diff.ToString("#.##")} ευρώ πληρώθηκε με ΜΕΤΡΗΤΑ.##";
                            if (checkBox5.Checked)
                            {
                                richTextBox1.Text = richTextBox1.Text.Replace("Το πιστωτικό αξίας",
                                    "Μέρος του πιστωτικού αξιας");
                                richTextBox2.Text = richTextBox2.Text.Replace("Εδώ εκπληρώθηκε το πιστωτικό",
                                    "Εδώ εκπληρώθηκε μέρος του πιστωτικού");
                            }
                        }
                        else if (radioButton4.Checked)
                        {
                            richTextBox1.Text =
                                $"**Το πιστωτικό αξίας {textBox2.Text} ευρώ εκπληρώθηκε στην {textBox3.Text}.**";
                            richTextBox2.Text =
                                $"**Εδώ εκπληρώθηκε το πιστωτικό {textBox1.Text} αξίας {textBox2.Text} ευρώ.";
                            if (checkBox4.Checked)
                                richTextBox2.Text = richTextBox2.Text +
                                                    $" ## Η διαφορά {diff.ToString("#.##")} ευρώ πληρώθηκε με ΚΑΡΤΑ.##";
                            if (checkBox5.Checked)
                            {
                                richTextBox1.Text = richTextBox1.Text.Replace("Το πιστωτικό αξίας",
                                    "Μέρος του πιστωτικού αξιας");
                                richTextBox2.Text = richTextBox2.Text.Replace("Εδώ εκπληρώθηκε το πιστωτικό",
                                    "Εδώ εκπληρώθηκε μέρος του πιστωτικού");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Επιλέξτε πώς θα πληρωθεί η διαφορά.");
                        }
                    }
                    else if (diff < 0)
                    {
                        diff = Math.Abs(diff);
                        richTextBox1.Text =
                            $"**Μέρος του πιστωτικού αξίας {textBox4.Text} ευρώ εκπληρώθηκε στην {textBox3.Text}. ## Απομένει υπόλοιπο {diff.ToString("#.##")} ευρώ.##";
                        richTextBox2.Text =
                            $"**Εδώ εκπληρώθηκε μέρος του πιστωτικού {textBox1.Text} αξίας {textBox4.Text} ευρώ.**";
                    }
                    else
                    {
                        MessageBox.Show(
                            "Το πιστωτικό έχει υπόλοιπο. Παρακαλώ επιλέξτε ότι θα χρησιμοποιηθεί μέρος του πιστωτικού.");
                    }
                }
                else
                {
                    MessageBox.Show("Δεν έχετε εισάγει επαρκεί δεδομένα.");
                }
            }
        }
        public string lastclip = null;
        public string lastorder = null;
        public string lastemail = null;
        public string lastname = null;
        public string lastprice = null;

        public int _ccounter = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //toolStripProgressBar1.Value = i++;
            //if (i >= 100) { i = 0; }
            toolStripStatusLabel1.Text = DateTime.Now.ToString();

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
            if (countdown == 100)
            {
                toolStripProgressBar1.Visible = true;
                toolStripProgressBar1.Value = 100;
            }
            if (countdown > 0)
            {
                countdown--;
                toolStripProgressBar1.Value = countdown;
                if (countdown <= 0)
                {
                    hold = 1;
                    toolStripProgressBar1.Value = 0;
                    toolStripProgressBar1.Visible = false;
                    dateTimePicker3.Value = DateTime.Now;
                    Clipboard.Clear();
                }
            }
            hold = 0;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox9.Text != "") textBox24.Text = "" + 500 * int.Parse(textBox9.Text);
            if (textBox10.Text != "") textBox25.Text = "" + 200 * int.Parse(textBox10.Text);
            if (textBox11.Text != "") textBox26.Text = "" + 100 * int.Parse(textBox11.Text);
            if (textBox12.Text != "") textBox27.Text = "" + 50 * int.Parse(textBox12.Text);
            if (textBox13.Text != "") textBox28.Text = "" + 20 * int.Parse(textBox13.Text);
            if (textBox14.Text != "") textBox29.Text = "" + 10 * int.Parse(textBox14.Text);
            if (textBox15.Text != "") textBox30.Text = "" + 5 * int.Parse(textBox15.Text);
            if (textBox16.Text != "") textBox31.Text = "" + 2 * int.Parse(textBox16.Text);
            if (textBox17.Text != "") textBox32.Text = "" + 1 * int.Parse(textBox17.Text);
            if (textBox18.Text != "") textBox33.Text = "" + 0.50 * double.Parse(textBox18.Text, nStyles, cCulture);
            if (textBox19.Text != "") textBox34.Text = "" + 0.20 * double.Parse(textBox19.Text, nStyles, cCulture);
            if (textBox20.Text != "") textBox35.Text = "" + 0.10 * double.Parse(textBox20.Text, nStyles, cCulture);
            if (textBox21.Text != "") textBox36.Text = "" + 0.05 * double.Parse(textBox21.Text, nStyles, cCulture);
            if (textBox22.Text != "") textBox37.Text = "" + 0.02 * double.Parse(textBox22.Text, nStyles, cCulture);
            if (textBox23.Text != "") textBox38.Text = "" + 0.01 * double.Parse(textBox23.Text, nStyles, cCulture);
            sum = double.Parse(textBox24.Text, nStyles, cCulture)
                  + double.Parse(textBox25.Text, nStyles, cCulture)
                  + double.Parse(textBox26.Text, nStyles, cCulture)
                  + double.Parse(textBox27.Text, nStyles, cCulture)
                  + double.Parse(textBox28.Text, nStyles, cCulture)
                  + double.Parse(textBox29.Text, nStyles, cCulture)
                  + double.Parse(textBox30.Text, nStyles, cCulture)
                  + double.Parse(textBox31.Text, nStyles, cCulture)
                  + double.Parse(textBox32.Text, nStyles, cCulture)
                  + double.Parse(textBox33.Text, nStyles, cCulture)
                  + double.Parse(textBox34.Text, nStyles, cCulture)
                  + double.Parse(textBox35.Text, nStyles, cCulture)
                  + double.Parse(textBox36.Text, nStyles, cCulture)
                  + double.Parse(textBox37.Text, nStyles, cCulture)
                  + double.Parse(textBox38.Text, nStyles, cCulture);
            if (sum != 0) textBox80.Text = "" + sum;
            var countit = double.Parse(textBox41.Text, nStyles, cCulture) +
                          double.Parse(textBox44.Text, nStyles, cCulture) +
                          double.Parse(textBox45.Text, nStyles, cCulture);
            var lol = countit -
                      double.Parse(textBox40.Text, nStyles, cCulture);
            var lol1 = double.Parse(textBox80.Text, nStyles, cCulture) - lol;
            lol1 = Math.Round(lol1, 2, MidpointRounding.ToEven);
            textBox81.Text = "" + lol1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox3.Enabled = textBox4.Enabled = false;
            }
            else
            {
                textBox3.Enabled = textBox4.Enabled = true;
            }
        }
        private void textBoxRdots_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBox2.Checked)
                if (e.KeyChar == '.')
                    e.KeyChar = ',';
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TabPage currentTab = tabControl1.SelectedTab;
            for (i = 0; i <= 1; i++)
            {
                foreach (Control control in currentTab.Controls)
                {
                    //Console.WriteLine("Control [{0}] - [{1}]", control.Name, control.GetType().Name);
                    if (control.GetType().Name.ToString() == "TextBox")
                    {
                        control.Text = "0";
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"**Αποτυχία 1ου SMS - Ενημερώθηκε μέσω τηλεφώνου {DateTimeNUser()}");
            notifier("Αποτυχία 1ου [Κλήση]");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int extra = 0;
            if (checkBox11.Checked)
            {
                extra += 5;
            }
            else
            {
                if (checkBox9.Checked) extra += 1;
                if (checkBox10.Checked) extra += 2;
            }
            string doh = dtto.DateTo("excludeSunday", extra);

            Clipboard.SetText($"**2η Ενημέρωση μέσω τηλεφώνου {DateTimeNUser()} ότι θα παραμείνει μέχρι και {doh}");
            notifier("Τηλεφωνική Υπενθύμιση");
        }
        private void button14_Click(object sender, EventArgs e) // Button 2o EPITOPOU
        {
            int extra = 0;            
            if (checkBox11.Checked)
            {
                extra += (int)numericUpDown1.Value;
            }
            else
            {
                if (checkBox9.Checked) extra += 1;
                if (checkBox10.Checked) extra += 2;
            }
            label32.Text = dtto.DateTo("excludeSunday", extra);
            Clipboard.SetText($"{textBox48.Text} - ΣΑΣ ΕΝΗΜΕΡΩΝΟΥΜΕ ΟΤΙ Η ΠΑΡΑΓΓΕΛΙΑ ΣΑΣ ΘΑ ΠΑΡΑΜΕΙΝΕΙ ΣΤΟ ΚΑΤΑΣΤΗΜΑ ΜΑΣ ΕΩΣ {label32.Text.ToUpper()}. ΕΥΧΑΡΙΣΤΟΥΜΕ");
            notifier("2ο ΕΠΙΤΟΠΟΥ");
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            var key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\e-ShopAssistant");
            key.SetValue("Phone", textBox43.Text);
            key.SetValue("kava", textBox46.Text);
            key.SetValue("ESHOP_ONE", textBox6.Text);
            key.SetValue("ESHOP_SHOP", textBox48.Text);
            key.SetValue("MAIL_ADDRESS", textBox47.Text);
            key.SetValue("REPLACE_ON_MAIL", checkBox8.Checked.ToString());
            key.Close();
            Properties.Settings.Default._storeAddress = _storeAddress.Text;
            Properties.Settings.Default.BookDays = numericUpDown1.Value;
            Properties.Settings.Default.Signature = richTextBox3.Text;
            //Properties.Settings.Default._storeArea = _txtFrom.Text;
            Properties.Settings.Default.Save();
            //_txtFrom.Text = _storeAddress.Text;
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            Clipboard.SetText(
                $"**Ζήτησε να παραμείνει στο κατάστημα μέχρι και {dateTimePicker3.Value.ToString("dddd dd/MM")}({CurrentUser()})**");
            if (hold != 1)
                if (dateTimePicker3.Value != DateTime.Now)
                    countdown = 100;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"**Δεν απαντούσε {DateTimeNUser()}");
            notifier("Δεν απαντούσε");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"**Αδυναμία Επικοινωνίας {DateTimeNUser()}");
            notifier("Αδυναμία Επικοινωνίας");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //var client = new WebClient();
            //var stream = client.OpenRead("http://tools.idle.gr/data.txt");
            //var reader = new StreamReader(stream);
            //var content = reader.ReadToEnd();
            //richTextBox5.Text = content;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Process.Start("calc");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("ΑΡΘΡΟ 39Α, ΥΠΟΧΡΕΟΣ ΓΙΑ ΤΗΝ ΚΑΤΑΒΟΛΗ ΤΟΥ ΦΟΡΟΥ ΕΙΝΑΙ Ο ΑΓΟΡΑΣΤΗΣ");
            notifier("Αρθρο 39Α");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            int extra = 0;
            if (checkBox11.Checked)
            {
                extra += 5;
            }
            else
            {
                if (checkBox9.Checked) extra += 1;
                if (checkBox10.Checked) extra += 2;
            }
            label32.Text = dtto.DateTo("excludeSunday", extra);
            Clipboard.SetText(
                $"{textBox48.Text} - ΣΑΣ ΥΠΕΝΘΥΜΙΖΟΥΜΕ ΟΤΙ Η ΠΑΡΑΓΓΕΛΙΑ ΣΑΣ ΕΙΝΑΙ ΕΤΟΙΜΗ ΚΑΙ ΠΡΕΠΕΙ ΝΑ ΠΑΡΑΔΟΘΕΙ ΜΕΧΡΙ {label32.Text.ToUpper()}.");
            notifier("2ο ESHOP");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox48.Text.ToUpper() + " " + textBox6.Text);
            notifier("1o SMS");
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
                groupBox2.Enabled = true;
            else
                groupBox2.Enabled = false;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = richTextBox1.Text = richTextBox2.Text = "";
        }


        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            label8.Text = textBox6.Text.Length.ToString();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (currentUser.Text != "")
            {
                Clipboard.SetText("{Τιμολογήθηκε από " + currentUser.Text + "}");
                notifier("Τιμολ. Από..");
            }
            else
            {
                MessageBox.Show("Όνομα κενό!");
            }
        }
        private void secondEmail()
        {
            int extra = (int)extraUpDown.Value;
            string doh = dtto.DateTo("excludeSunday", extra);
            emailmsg =
                $"\r\n\r\nΘα θέλαμε να σας υπενθυμίσουμε ότι η παραγγελία σας είναι έτοιμη και θα παραμείνει στο κατάστημά μας μέχρι και {doh}. \r\nΜπορείτε να περάσετε να την παραλάβετε. \r\n";
        }
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            button28.Enabled = true;
            if (radioButton6.Checked)
            {
                secondEmail();
            }
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
                emailmsg =
                    "\r\n\r\nΘα θέλαμε να σας ενημερώσουμε ότι η παραγγελία σας είναι έτοιμη. \r\nΜπορείτε να περάσετε να την παραλάβετε. \r\n";
            button28.Enabled = true;
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            Clipboard.Clear();
            extraUpDown.Value = 0;
            _emailaid.Checked = true;
            lastname = lastemail = lastorder = lastclip = null;
            textBox53.Text = textBox54.Text = textBox55.Text = "";
            _ccounter = 0;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox47.Text != "")
            {
                if (signChk.Checked) emailmsg += Properties.Settings.Default.Signature;
                var message = comboBox3.Text + " " + textBox55.Text + " " + tod2 + " " + emailmsg;
                System.Console.WriteLine(message);
                if (checkBox8.Checked)
                {
                    message = message.Replace("\r\n", "%0D%0A");
                    message = message.Replace(" ", "%20");
                }

                var mailing_add = textBox53.Text;
                richTextBox5.Text = "E-mail Address:" + mailing_add + "\r\n" +
                                    "Παραγγελία: " + textBox54.Text + "\r\n" +
                                    $"{message}";
                var mailto = string.Format("mailto:{0}?Subject={1}&BCC={2}&Body={3}", mailing_add,
                    "Παραγγελία: " + textBox54.Text, textBox47.Text, message);
                Process.Start(mailto);
                button28.PerformClick();
                _emailaid.Checked = false;
            }
            else
            {
                MessageBox.Show("Δεν έχει οριστεί το e-mail του καταστήματος. Go to Settings -> E-mail address");
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
                emailmsg = "\r\n\r\nΘα θέλαμε να σας ενημερώσουμε ότι το Service σας είναι έτοιμo. \r\nΜπορείτε να περάσετε να τo παραλάβετε.\r\n";

            button28.Enabled = true;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
                emailmsg = "\r\nΣας υπενθυμίζουμε ότι το Service σας είναι έτοιμo.\r\nΜπορείτε να περάσετε να τo παραλάβετε.\r\n";
            button28.Enabled = true;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
                emailmsg =
                    $"\r\nΠαρακαλούμε όπως επικοινωνήσετε μαζί μας στo τηλέφωνo {textBox43.Text}.\r\n";
            button28.Enabled = true;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
                tod2 = "καλημέρα σας.";
            else
                tod2 = "καλησπέρα σας.";
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
                Clipboard.SetText($"**Αποτυχία 1ου SMS - Αποστάλθηκε E-mail {DateTimeNUser()}");
            if (radioButton6.Checked)
            {
                int extra = (int)extraUpDown.Value;
                string doh = dtto.DateTo("excludeSunday", extra);
                Clipboard.SetText($"**Αποστάλθηκε 2o E-mail {DateTimeNUser()} ότι θα παραμείνει μέχρι και {doh}");
            }
            if (radioButton7.Checked)
                Clipboard.SetText($"**Αποτυχία 1ου SMS - Αποστάλθηκε E-mail {DateTimeNUser()}");
            if (radioButton8.Checked)
                Clipboard.SetText($"**Αποστάλθηκε E-mail υπενθύμισης {DateTimeNUser()}");
            if (radioButton9.Checked)
                Clipboard.SetText($"**Αποστάλθηκε E-mail ώστε να επικοινωνήσει ο πελάτης μαζί μας. {DateTimeNUser()}");
            if (radioButton1.Checked)
                Clipboard.SetText($"**Αποστάλθηκε E-mail ώστε να επικοινωνήσει ο πελάτης μαζί μας (διευκρινίσεις). {DateTimeNUser()}");
            if (radioButton12.Checked)
                Clipboard.SetText($"**Αποστάλθηκε E-mail ώστε να επικοινωνήσει ο πελάτης μαζί μας. {DateTimeNUser()}");
        }
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            Hide();
            WindowState = FormWindowState.Minimized;
            notifier("Ο πιστός βοηθός είναι ακόμα εδώ!!!");
        }

        private void RadioButton12_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked)
                emailmsg =
                    $"\r\nΣας ενημερώνουμε ότι η παραγγελία σας βρίσκεται στο κατάστημα μας.\r\n Παρακαλώ όπως επικοινωνήσετε μαζί μας στο τηλέφωνο {textBox43.Text}\r\n σχετικά με την παράδοση της. \r\n";
            button28.Enabled = true;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(tb, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(tb, false, true, true, true);
            else
                return;
        }
        private void CheckBox11_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked)
            {
                checkBox9.Enabled = checkBox10.Enabled = checkBox9.Checked = checkBox10.Checked = false;
            }
            else
            {
                checkBox9.Enabled = checkBox10.Enabled = true;
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"**Ζήτησε να παραλάβει απ το κατάστημα. {DateTimeNUser()}");
            notifier("Παραλαβή Επιτόπου");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(DateTime.Now.ToString("dd/MM HH:mm"));
        }
        private void _emailaid_CheckedChanged(object sender, EventArgs e)
        {
            Clipboard.Clear();
            lastname = lastemail = lastorder = lastclip = null;
            _ccounter = 0;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            printDocument1.DefaultPageSettings.Landscape = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D1)
            {
                tabControl1.SelectTab(tabPage4);
            }
            else if (e.Control && e.KeyCode == Keys.D2)
            {
                tabControl1.SelectTab(tabPage1);
            }
            else if (e.Control && e.KeyCode == Keys.D3)
            {
                tabControl1.SelectTab(tabPage2);
            }
            else if (e.Control && e.KeyCode == Keys.D4)
            {
                tabControl1.SelectTab(tabPage3);
            }
            else if (e.Control && e.KeyCode == Keys.D5)
            {
                tabControl1.SelectTab(tabPage7);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                button3.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                button1.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                button2.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                if (tabControl1.SelectedTab == tabPage1)
                {
                    button18.PerformClick();
                }
                else if (tabControl1.SelectedTab == tabPage2)
                {
                    button25.PerformClick();
                }
                else if (tabControl1.SelectedTab == tabPage3)
                {
                    clrBtn.PerformClick();
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe");
        }

        private void button40_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ctrl + 1 : Selects Automated Messages Tab\n" +
                            "Ctrl + 2 : Selects E-mail Tab\n" +
                            "Ctrl + 3 : Selects Πιστωτικά Tab\n" +
                            "Ctrl + 4 : Selects Υπολογισμός Χρημάτων Tab\n" +
                            "Ctrl + C : Clears active tabs inputs" +
                            "Alt + Δ : Δεν απαντούσε\n" +
                            "Alt + E : 2ο Μήνυμα για επιτόπου\n" +
                            "Alt + A : Αδυναμία επικοινωνίας\n" +
                            "Alt + Ρ : Ημερομηνία και Ώρα τώρα\n" +
                            "Alt + Τ : Τιμολογήθηκε από ....\n" +
                            "Alt + Z : Ζήτησε κατάστημα\n", "Shortcuts");
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (Regex.IsMatch(tb.Text, @"^\d+$")) button8.PerformClick();
            else
            {
                tb.Text = "0";
                MessageBox.Show("Συμπληρώνουμε μόνο ακέραιους αριθμούς!");
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.User = currentUser.Text;
            Properties.Settings.Default.Save();
        }

        private void extraUpDown_ValueChanged(object sender, EventArgs e)
        {
            secondEmail();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            tada = new Cliptool();
            tada.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                emailmsg =
                    $"\r\nΗ παραγγελία σας βρίσκεται σε αναμονή διευκρινίσεων. Παρακαλούμε όπως επικοινωνήσετε μαζί μας στo τηλέφωνo {textBox43.Text}.\r\n";
            button28.Enabled = true;
        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            //listView1.Items.Add(new ListViewItem(new string[] { "skata", "test", "patates" }));
            //listView1.Items.Add(new ListViewItem(new string[] { e.Name, e.FullPath, e.ChangeType.ToString() }));
        }

        private void fileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            listView1.Items.Add(new ListViewItem(new string[] { e.Name, e.FullPath, e.ChangeType.ToString() }));
        }

        private void fileSystemWatcher1_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            //listView1.Items.Add(new ListViewItem(new string[] { e.Name, e.FullPath, e.ChangeType.ToString() }));
        }

        private void fileSystemWatcher1_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            listView1.Items.Add(new ListViewItem(new string[] { e.Name, e.FullPath, e.ChangeType.ToString(), e.OldName }));
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            //Rename Tautotita
            if (listView1.SelectedItems[0].SubItems[1] != null)
            {
                var extension = Path.GetExtension(listView1.SelectedItems[0].SubItems[1].Text);
                File.Move(listView1.SelectedItems[0].SubItems[1].Text, fileSystemWatcher1.Path + "\\IDENTITY - " + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension);
                listView1.SelectedItems[0].Remove();
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[1] != null)
            {
                var extension = Path.GetExtension(listView1.SelectedItems[0].SubItems[1].Text);
                File.Move(listView1.SelectedItems[0].SubItems[1].Text, fileSystemWatcher1.Path + "\\ΕΞΟΔΟ - " + DateTime.Now.ToString("dd.MM.yyyy-hhmmss") + extension);
                listView1.SelectedItems[0].Remove();
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[1] != null)
            {
                var extension = Path.GetExtension(listView1.SelectedItems[0].SubItems[1].Text);
                File.Move(listView1.SelectedItems[0].SubItems[1].Text, fileSystemWatcher1.Path + "\\TICKET COMPLIMENTS - " + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension);
                listView1.SelectedItems[0].Remove();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[1] != null)
            {
                listView1.SelectedItems[0].Remove();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.monitorfolder = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.Save();
                label40.Text = Properties.Settings.Default.monitorfolder;
                fileSystemWatcher1.Path = Path.GetFullPath(Properties.Settings.Default.monitorfolder);
            }
        }

        private void label30_Click(object sender, EventArgs e)
        {
            about.Show();
        }

        private void filemonitor_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.filemonitor = fileSystemWatcher1.EnableRaisingEvents = filemonitor.Checked;
            Properties.Settings.Default.Save();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            if ((listView1.SelectedItems[0].SubItems[1].Text != "") || (listView1.SelectedItems[0].SubItems[1].Text != null))
            {

                saveFileDialog1.InitialDirectory = Properties.Settings.Default.monitorfolder;
                var extension = Path.GetExtension(listView1.SelectedItems[0].SubItems[1].Text);
                saveFileDialog1.FileName = $"Αρχείο - {DateTime.Now.ToString("ddMMyyyyhhmmss")}";
                saveFileDialog1.DefaultExt = extension;
                saveFileDialog1.AddExtension = true;
                saveFileDialog1.Filter = $"{extension}|{extension}|*.*|(*.*)";
                var result = saveFileDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {
                    File.Move(listView1.SelectedItems[0].SubItems[1].Text, saveFileDialog1.FileName);
                    listView1.SelectedItems[0].Remove();
                }
            }
        }

        private void CopyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView1.SelectedItems[0].SubItems[1].Text);
        }

        private void toolStripSeparator4_Click(object sender, EventArgs e)
        {
            var extension = Path.GetExtension(listView1.SelectedItems[0].SubItems[1].Text);
            File.Move(listView1.SelectedItems[0].SubItems[1].Text, fileSystemWatcher1.Path + "\\ΑΠΟΔΕΙΞΗ ΠΡΟΕΙΣΠΡΑΞΗΣ - " + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension);
            listView1.SelectedItems[0].Remove();
        }

        private void MoveFile(string dstName)
        {
            var directory = fileSystemWatcher1.Path + $"\\{dstName} {DateTime.Now.ToString("MMMM yyyy")}";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var filename = Path.GetFileName(listView1.SelectedItems[0].SubItems[1].Text);
            File.Move(listView1.SelectedItems[0].SubItems[1].Text, Path.Combine(directory, filename));
            listView1.SelectedItems[0].Remove();
        }
        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            //ejoda
            MoveFile("ΕΞΟΔΑ");
        }
        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            //Ticket Compliments
            MoveFile("ΔΙΑΤΑΚΤΙΚΕΣ");
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            //PERSONELL
            MoveFile("ΠΡΟΣΩΠΙΚΟ");
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            //ΤΑΥΤΟΤΗΤΕΣ
            MoveFile("ΤΑΥΤΟΤΗΤΕΣ");
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            //ΑΠΟΔ ΠΡΟΕΙΣΠΡΑΞΗΣ
            MoveFile("ΑΠΟΔ ΠΡΟΕΙΣΠΡΑΞΗΣ");
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            Process.Start(fileSystemWatcher1.Path);
        }
    }
}
