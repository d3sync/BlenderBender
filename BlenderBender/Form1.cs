using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
//using ControlzEx.Standard;
using LahoreSocketAsync;

namespace BlenderBender
{
    public partial class Form1 : Form
    {
        public string emailmsg =
            "\r\n\r\nΘα θέλαμε να σας ενημερώσουμε ότι η παραγγελία σας βρίσκεται στο κατάστημά μας.\r\nΜπορείτε να περάσετε να την παραλάβετε.\r\n";

        private int hold;
        public int i = 0;
        public double sum;
        public string tod = "καλησπέρα σας.";
        public string tod2 = "καλησπέρα σας.";
        public LahoreSocketClient client = null;

        public Form1()
        {
            InitializeComponent();
            
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
                if (key.GetValue("PLUS4U") != null)
                    textBox7.Text = key.GetValue("PLUS4U").ToString();
                else
                    textBox7.Text =
                        "H ΠAPAΓΓEΛIA ΣAΣ AΠO THN PLUS4U EXEI AΦIXΘΕΙ ΣTO E-SHOP.GR ΡΟΔΟΥ, ΣΤΗ Δ/ΝΣΗ: ΚΩΝ. ΥΔΡΑΙΟΥ 71. ΤΗΛ.: 2241073390. ΕΥΧΑΡΙΣΤΟΥΜΕ!";
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

                //Console.WriteLine(key.GetValue("Setting2")); 
                key.Close();
            }

            //tabPage1.Enabled = false;
            button18_Click(null, null);
            //e-mail settings disabled
            tabPage7.Visible = false;
            tabPage9.Visible = false;
            //tabPage11.Visible = false;
            //tabControl1.TabPages.Remove(tabPage11);
            tabControl1.TabPages.Remove(tabPage7);
            tabControl1.TabPages.Remove(tabPage9);
            var asm = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            var version = string.Format("{0}", fvi.ProductVersion);
            label31.Text = version;
            //end of e-mail settings
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                toolStripStatusLabel2.Text = GetLocalIPAddress();
            }

            _txtFrom.Text = Properties.Settings.Default._storeArea;
            _storeAddress.Text = Properties.Settings.Default._storeAddress;
        }
        private static void HandleTextReceived(object sender, TextReceivedEventArgs trea)
        {
            Console.WriteLine(
                string.Format(
                    "{0} - Received: {1}{2}",
                    DateTime.Now,
                    trea.TextReceived,
                    Environment.NewLine));

        }

        private static void HandleServerDisconnected(object sender, ConnectionDisconnectedEventArgs cdea)
        {
            Console.WriteLine(
                string.Format(
                    "{0} - Disconnected from server: {1}{2}",
                    DateTime.Now,
                    cdea.DisconnectedPeer,
                    Environment.NewLine));
            //System.Console.ReadLine();
            //Environment.Exit(1);
        }

        private static void HandleServerConnected(object sender, ConnectionDisconnectedEventArgs cdea)
        {
            Console.WriteLine(
                string.Format(
                    "{0} - Connected to server: {1}{2}",
                    DateTime.Now,
                    cdea.DisconnectedPeer,
                    Environment.NewLine));
        }

        public int countdown { get; private set; }

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
            richTextBox1.Text = "";
            richTextBox2.Text = "";
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
                    var diff = double.Parse(textBox4.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture) -
                               double.Parse(textBox2.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture);

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
            if (_monitor.Checked == true)
            {
                var iData = Clipboard.GetDataObject();
                if ((Clipboard.GetDataObject() != null) && ((string) iData.GetData(DataFormats.Text) != lastclip))
                {
                    // Is Data Text?
                    if (iData.GetDataPresent(DataFormats.Text))
                    {
                        richTextBox6.Text += (string) iData.GetData(DataFormats.Text) + "\r\n";
                        lastclip = (string) iData.GetData(DataFormats.Text);
                    }
                }
            }
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
                            //Console.WriteLine("it works");
                            this.TopMost = false;
                        }
                    }
                }
            }
            if (_announceAid.Checked == true)
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
                                _orderCode.Text = data;
                                lastorder = data;
                                _ccounter += 1;
                            }
                        }

                        if (data.Contains("."))
                        {
                            if (Regex.IsMatch(data, @"^\d+\.\d{2}$"))
                            {
                                if (data != lastprice)
                                {
                                    _numPrice.Text = data;
                                    lastprice = data;
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
                                    _orderName.Text = gtext;
                                    lastname = gtext;
                                    _ccounter += 1;
                                }
                            }
                        }
                        lastclip = (string)iData.GetData(DataFormats.Text);
                        if (_ccounter == 3)
                        {
                            //this.WindowState = FormWindowState.Normal;
                            //this.BringToFront();
                            //this.TopMost = true;
                            //this.Focus();
                            _ccounter = 0;
                            //Console.WriteLine("it works");
                            //this.TopMost = false;
                            button9.PerformClick();
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
                    //notifyD("Clipboard:", "Erased");
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
            if (textBox18.Text != "")
                textBox33.Text = "" + 0.50 * double.Parse(textBox18.Text, NumberStyles.AllowDecimalPoint,
                                     CultureInfo.CurrentCulture);
            if (textBox19.Text != "")
                textBox34.Text = "" + 0.20 * double.Parse(textBox19.Text, NumberStyles.AllowDecimalPoint,
                                     CultureInfo.CurrentCulture);
            if (textBox20.Text != "")
                textBox35.Text = "" + 0.10 * double.Parse(textBox20.Text, NumberStyles.AllowDecimalPoint,
                                     CultureInfo.CurrentCulture);
            if (textBox21.Text != "")
                textBox36.Text = "" + 0.05 * double.Parse(textBox21.Text, NumberStyles.AllowDecimalPoint,
                                     CultureInfo.CurrentCulture);
            if (textBox22.Text != "")
                textBox37.Text = "" + 0.02 * double.Parse(textBox22.Text, NumberStyles.AllowDecimalPoint,
                                     CultureInfo.CurrentCulture);
            if (textBox23.Text != "")
                textBox38.Text = "" + 0.01 * double.Parse(textBox23.Text, NumberStyles.AllowDecimalPoint,
                                     CultureInfo.CurrentCulture);
            sum = double.Parse(textBox24.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture)
                  + double.Parse(textBox25.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture)
                  + double.Parse(textBox26.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture)
                  + double.Parse(textBox27.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture)
                  + double.Parse(textBox28.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture)
                  + double.Parse(textBox29.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture)
                  + double.Parse(textBox30.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture)
                  + double.Parse(textBox31.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture)
                  + double.Parse(textBox32.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture)
                  + double.Parse(textBox33.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture)
                  + double.Parse(textBox34.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture)
                  + double.Parse(textBox35.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture)
                  + double.Parse(textBox36.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture)
                  + double.Parse(textBox37.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture)
                  + double.Parse(textBox38.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture);
            if (sum != 0) textBox39.Text = "" + sum;
            var countit = double.Parse(textBox41.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture) +
                          double.Parse(textBox44.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture) +
                          double.Parse(textBox45.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture);
            var lol = countit -
                      double.Parse(textBox40.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture);
            var lol1 = double.Parse(textBox39.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture) - lol;
            lol1 = Math.Round(lol1, 2, MidpointRounding.ToEven);
            textBox42.Text = "" + lol1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox3.Enabled = false;
                textBox4.Enabled = false;
            }
            else
            {
                textBox3.Enabled = true;
                textBox4.Enabled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBox2.Checked)
                if (e.KeyChar == '.')
                    e.KeyChar = ',';
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBox2.Checked)
                if (e.KeyChar == '.')
                    e.KeyChar = ',';
        }


        private void textBox41_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBox2.Checked)
                if (e.KeyChar == '.')
                    e.KeyChar = ',';
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox9.Text = "0";
            textBox10.Text = "0";
            textBox11.Text = "0";
            textBox12.Text = "0";
            textBox13.Text = "0";
            textBox14.Text = "0";
            textBox15.Text = "0";
            textBox16.Text = "0";
            textBox17.Text = "0";
            textBox18.Text = "0";
            textBox19.Text = "0";
            textBox20.Text = "0";
            textBox21.Text = "0";
            textBox22.Text = "0";
            textBox23.Text = "0";
            textBox24.Text = "0";
            textBox25.Text = "0";
            textBox26.Text = "0";
            textBox27.Text = "0";
            textBox28.Text = "0";
            textBox29.Text = "0";
            textBox30.Text = "0";
            textBox31.Text = "0";
            textBox32.Text = "0";
            textBox33.Text = "0";
            textBox34.Text = "0";
            textBox35.Text = "0";
            textBox36.Text = "0";
            textBox37.Text = "0";
            textBox38.Text = "0";
            textBox39.Text = "0";
            textBox40.Text = "0";
            textBox41.Text = "0";
            textBox42.Text = "0";
            textBox44.Text = "0";
            textBox45.Text = "0";
        }
        
            private void button10_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("**Αποτυχία 1ου SMS - Ενημερώθηκε μέσω τηλεφώνου " +
                              DateTime.Now.ToString("dd/MM HH:mm"));
            //notifyD("Copied to clipboard:", Clipboard.GetText());
            notifier("Αποτυχία 1ου [Κλήση]");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var meh = DateTime.Now;
            if (meh.ToString("dddd") == "Friday" || meh.ToString("dddd") == "Saturday" ||
                meh.ToString("dddd") == "Παρασκευή" || meh.ToString("dddd") == "Σάββατο")
                meh = meh.AddDays(3);
            else
                meh = meh.AddDays(2);
            dateTimePicker2.Value = meh;
            var dtp = meh.ToString("dddd dd/MM");
            var ntay = meh.ToString("dddd");
            var doh = "";
            if (ntay == "Monday" || ntay == "Δευτέρα")
                doh = dtp.Replace(ntay, "την Δευτέρα");
            else if (ntay == "Tuesday" || ntay == "Τρίτη")
                doh = dtp.Replace(ntay, "την Τρίτη");
            else if (ntay == "Wednesday" || ntay == "Τετάρτη")
                doh = dtp.Replace(ntay, "την Τετάρτη");
            else if (ntay == "Thursday" || ntay == "Πέμπτη")
                doh = dtp.Replace(ntay, "την Πέμπτη");
            else if (ntay == "Friday" || ntay == "Παρασκευή")
                doh = dtp.Replace(ntay, "την Παρασκευή");
            else if (ntay == "Saturday" || ntay == "Σάββατο")
                doh = dtp.Replace(ntay, "το Σάββατο");
            else
                doh = dtp;
            Clipboard.SetText(
                $"**2η Ενημέρωση μέσω τηλεφώνου {DateTime.Now.ToString("dd/MM HH:mm")} ότι θα παραμείνει μέχρι και {doh}");
            //notifyD("Copied to clipboard:", Clipboard.GetText());
            notifier("Τηλεφωνική Υπενθύμιση");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var meh = DateTime.Now;
            meh = meh.AddDays(2);
            dateTimePicker1.Value = meh;
        }

        private void button14_Click(object sender, EventArgs e) // Button 2o EPITOPOU
        {
            var nn = 2;
            var meh = DateTime.Now;
            //toolStripStatusLabel2 = DayOfWeek.Saturday;
            if (meh.DayOfWeek == DayOfWeek.Saturday) meh = meh.AddDays(1);
            if (checkBox11.Checked) nn = nn + 5;
            meh = meh.AddDays(nn);
            if (meh.ToString("dddd") == "Sunday" || meh.ToString("dddd") == "Κυριακή")
                meh = meh.AddDays(1);
            if (checkBox9.Checked) meh = meh.AddDays(1);
            if (meh.ToString("dddd") == "Sunday" || meh.ToString("dddd") == "Κυριακή")
                meh = meh.AddDays(1);
            if (checkBox10.Checked)
            {
                meh = meh.AddDays(1);
                if (meh.ToString("dddd") == "Sunday" || meh.ToString("dddd") == "Κυριακή") meh = meh.AddDays(1);
                meh = meh.AddDays(1);
            }

            if (meh.ToString("dddd") == "Sunday" || meh.ToString("dddd") == "Κυριακή")
                meh = meh.AddDays(1);

            dateTimePicker2.Value = meh;
            var dtp = meh.ToString("dddd dd/MM");
            var ntay = meh.ToString("dddd");
            if (ntay == "Monday" || ntay == "Δευτέρα")
                label32.Text = dtp.Replace(ntay, "ΤΗΝ ΔΕΥΤΕΡΑ");
            else if (ntay == "Tuesday" || ntay == "Τρίτη")
                label32.Text = dtp.Replace(ntay, "ΤΗΝ ΤΡΙΤΗ");
            else if (ntay == "Wednesday" || ntay == "Τετάρτη")
                label32.Text = dtp.Replace(ntay, "ΤΗΝ ΤΕΤΑΡΤΗ");
            else if (ntay == "Thursday" || ntay == "Πέμπτη")
                label32.Text = dtp.Replace(ntay, "ΤΗΝ ΠΕΜΠΤΗ");
            else if (ntay == "Friday" || ntay == "Παρασκευή")
                label32.Text = dtp.Replace(ntay, "ΤΗΝ ΠΑΡΑΣΚΕΥΗ");
            else if (ntay == "Saturday" || ntay == "Σάββατο")
                label32.Text = dtp.Replace(ntay, "ΤΟ ΣΑΒΒΑΤΟ");
            else
                label32.Text = dtp;
            Clipboard.SetText(
                $"ΣΑΣ ΕΝΗΜΕΡΩΝΟΥΜΕ ΟΤΙ Η ΠΑΡΑΓΓΕΛΙΑ ΣΑΣ ΘΑ ΠΑΡΑΜΕΙΝΕΙ ΣΤΟ ΚΑΤΑΣΤΗΜΑ ΜΑΣ ΕΩΣ {label32.Text.ToUpper()}. ΤΗΛ.: {textBox43.Text}. ΕΥΧΑΡΙΣΤΟΥΜΕ");
            //notifyD("Copied to clipboard:", Clipboard.GetText());
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "e-Shop Assistant";
            notifyIcon1.BalloonTipText = "Αντιγράφθηκε στο Πρόχειρο: 2ο ΕΠΙΤΟΠΟΥ";
            notifyIcon1.ShowBalloonTip(2000);
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            var key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\e-ShopAssistant");
            key.SetValue("Phone", textBox43.Text);
            key.SetValue("kava", textBox46.Text);
            key.SetValue("PLUS4U", textBox7.Text);
            key.SetValue("ESHOP_ONE", textBox6.Text);
            key.SetValue("ESHOP_SHOP", textBox48.Text);
            key.SetValue("MAIL_ADDRESS", textBox47.Text);
            key.SetValue("REPLACE_ON_MAIL", checkBox8.Checked.ToString());
            key.Close();
            Properties.Settings.Default._storeAddress = _storeAddress.Text;
            Properties.Settings.Default._storeArea = _txtFrom.Text;
            Properties.Settings.Default.Save();
            //_txtFrom.Text = _storeAddress.Text;
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            Clipboard.SetText(
                $"**Ζήτησε να παραμείνει στο κατάστημα μέχρι και {dateTimePicker3.Value.ToString("dddd dd/MM")}**");
            //notifyD("Copied to clipboard:", Clipboard.GetText());
            if (hold != 1)
                if (dateTimePicker3.Value != DateTime.Now)
                    countdown = 100;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("**Δεν απαντούσε " + DateTime.Now.ToString("dd/MM HH:mm"));
            //notifyD("Copied to clipboard:", Clipboard.GetText());
            notifier("Δεν απαντούσε");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("**Αδυναμία Επικοινωνίας " + DateTime.Now.ToString("dd/MM HH:mm"));
            //notifyD("Copied to clipboard:", Clipboard.GetText());
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
            //notifyD("Copied to clipboard:", Clipboard.GetText());
            notifier("Αρθρο 39Α");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            var nn = 2;
            var meh = DateTime.Now;
            if (meh.DayOfWeek == DayOfWeek.Saturday) meh = meh.AddDays(1);
            if (checkBox11.Checked) nn = nn + 12;
            meh = meh.AddDays(nn);
            if (meh.ToString("dddd") == "Sunday" || meh.ToString("dddd") == "Κυριακή")
                meh = meh.AddDays(1);
            if (checkBox9.Checked) meh = meh.AddDays(1);
            if (meh.ToString("dddd") == "Sunday" || meh.ToString("dddd") == "Κυριακή")
                meh = meh.AddDays(1);
            if (checkBox10.Checked)
            {
                meh = meh.AddDays(1);
                if (meh.ToString("dddd") == "Sunday" || meh.ToString("dddd") == "Κυριακή") meh = meh.AddDays(1);
                meh = meh.AddDays(1);
            }

            if (meh.ToString("dddd") == "Sunday" || meh.ToString("dddd") == "Κυριακή")
                meh = meh.AddDays(1);

            dateTimePicker2.Value = meh;
            var dtp = meh.ToString("dddd dd/MM");
            var ntay = meh.ToString("dddd");
            if (ntay == "Monday" || ntay == "Δευτέρα")
                label32.Text = dtp.Replace(ntay, "ΤΗΝ ΔΕΥΤΕΡΑ");
            else if (ntay == "Tuesday" || ntay == "Τρίτη")
                label32.Text = dtp.Replace(ntay, "ΤΗΝ ΤΡΙΤΗ");
            else if (ntay == "Wednesday" || ntay == "Τετάρτη")
                label32.Text = dtp.Replace(ntay, "ΤΗΝ ΤΕΤΑΡΤΗ");
            else if (ntay == "Thursday" || ntay == "Πέμπτη")
                label32.Text = dtp.Replace(ntay, "ΤΗΝ ΠΕΜΠΤΗ");
            else if (ntay == "Friday" || ntay == "Παρασκευή")
                label32.Text = dtp.Replace(ntay, "ΤΗΝ ΠΑΡΑΣΚΕΥΗ");
            else if (ntay == "Saturday" || ntay == "Σάββατο")
                label32.Text = dtp.Replace(ntay, "ΤΟ ΣΑΒΒΑΤΟ");
            else
                label32.Text = dtp;
            Clipboard.SetText(
                $"ΣΑΣ ΥΠΕΝΘΥΜΙΖΟΥΜΕ ΟΤΙ Η ΠΑΡΑΓΓΕΛΙΑ ΣΑΣ ΕΙΝΑΙ ΕΤΟΙΜΗ ΚΑΙ ΠΡΕΠΕΙ ΝΑ ΠΑΡΑΔΟΘΕΙ ΜΕΧΡΙ {label32.Text.ToUpper()}. ΤΗΛ.: {textBox43.Text}.");
            //notifyD("Copied to clipboard:", Clipboard.GetText());
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "e-Shop Assistant";
            notifyIcon1.BalloonTipText = "Αντιγράφθηκε στο Πρόχειρο: 2ο ESHOP";
            notifyIcon1.ShowBalloonTip(2000);
        }
        

        private void button24_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox48.Text.ToUpper() + " " + textBox6.Text);
            //notifyD("Copied to clipboard:", Clipboard.GetText());
            notifier("1o SMS");
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
                groupBox2.Enabled = true;
            else
                groupBox2.Enabled = false;
        }


        private void textBox44_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBox2.Checked)
                if (e.KeyChar == '.')
                    e.KeyChar = ',';
        }

        private void textBox40_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBox2.Checked)
                if (e.KeyChar == '.')
                    e.KeyChar = ',';
        }


        private void button25_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            richTextBox1.Text = "";
            richTextBox2.Text = "";
        }

        private void button26_Click(object sender, EventArgs e)
        {
            var iData = Clipboard.GetDataObject();

            // Is Data Text?

            if (iData.GetDataPresent(DataFormats.Text))

                richTextBox6.Text += (string) iData.GetData(DataFormats.Text) + "\r\n";

            else

                richTextBox6.Text += "Data not found.\r\n";
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            label8.Text = textBox6.Text.Length.ToString();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            label42.Text = textBox7.Text.Length.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox8.Text != "")
            {
                Clipboard.SetText("{Τιμολογήθηκε από " + textBox8.Text + "}");
                notifier("Τιμολ. Από..");
            }
            else
            {
                MessageBox.Show("Όνομα κενό!");
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            button28.Enabled = true;
            if (radioButton6.Checked)
            {
                var nn = 2;
                var meh = DateTime.Now;
                //toolStripStatusLabel2 = DayOfWeek.Saturday;
                if (meh.DayOfWeek == DayOfWeek.Saturday) meh = meh.AddDays(1);
                if (checkBox11.Checked) nn = nn + 12;
                meh = meh.AddDays(nn);
                if (meh.ToString("dddd") == "Sunday" || meh.ToString("dddd") == "Κυριακή")
                    meh = meh.AddDays(1);
                if (checkBox9.Checked) meh = meh.AddDays(1);
                if (meh.ToString("dddd") == "Sunday" || meh.ToString("dddd") == "Κυριακή")
                    meh = meh.AddDays(1);
                if (checkBox10.Checked)
                {
                    meh = meh.AddDays(1);
                    if (meh.ToString("dddd") == "Sunday" || meh.ToString("dddd") == "Κυριακή") meh = meh.AddDays(1);
                    meh = meh.AddDays(1);
                }

                if (meh.ToString("dddd") == "Sunday" || meh.ToString("dddd") == "Κυριακή")
                    meh = meh.AddDays(1);

                dateTimePicker2.Value = meh;
                var dtp = meh.ToString("dddd dd/MM");
                var ntay = meh.ToString("dddd");

                var doh = "";
                //Creating the Message 
                if (ntay == "Monday" || ntay == "Δευτέρα")
                    doh = dtp.Replace(ntay, "την Δευτέρα");
                else if (ntay == "Tuesday" || ntay == "Τρίτη")
                    doh = dtp.Replace(ntay, "την Τρίτη");
                else if (ntay == "Wednesday" || ntay == "Τετάρτη")
                    doh = dtp.Replace(ntay, "την Τετάρτη");
                else if (ntay == "Thursday" || ntay == "Πέμπτη")
                    doh = dtp.Replace(ntay, "την Πέμπτη");
                else if (ntay == "Friday" || ntay == "Παρασκευή")
                    doh = dtp.Replace(ntay, "την Παρασκευή");
                else if (ntay == "Saturday" || ntay == "Σάββατο")
                    doh = dtp.Replace(ntay, "το Σάββατο");
                else
                    doh = dtp;
                dateTimePicker1.Value = meh;
                emailmsg =
                    $"\r\n\r\nΘα θέλαμε να σας υπενθυμίσουμε ότι η παραγγελία σας είναι έτοιμη και θα παραμείνει στο κατάστημά μας μέχρι και {doh}. \r\nΜπορείτε να περάσετε να την παραλάβετε. \r\n";
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
            _emailaid.Checked = true;
            lastname = null;
            lastemail = null;
            lastorder = null;
            lastclip = null;
            textBox53.Text = "";
            textBox54.Text = "";
            textBox55.Text = "";
            _ccounter = 0;
        }

    
        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox47.Text != "")
            {
                var message = comboBox3.Text + " " + textBox55.Text + " " + tod2 + " " + emailmsg;
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
                emailmsg =
                    "\r\n\r\nΘα θέλαμε να σας ενημερώσουμε ότι το Service σας είναι έτοιμo. \r\nΜπορείτε να περάσετε να τo παραλάβετε.\r\n";

            button28.Enabled = true;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
                emailmsg =
                    "\r\nΣας υπενθυμίζουμε ότι το Service σας είναι έτοιμo.\r\nΜπορείτε να περάσετε να τo παραλάβετε.\r\n";
            button28.Enabled = true;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
                emailmsg =
                    $"\r\nΠαρακαλούμε όπως επικοινωνήσετε μαζί μας στα τηλέφωνα {textBox43.Text}\r\n ή στο 211 5000 500.\r\n";
            button28.Enabled = true;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
                tod2 = "καλημέρα σας.";
            else
                tod2 = "καλησπέρα σας.";
        }


        private void button27_Click(object sender, EventArgs e)
        {
            richTextBox9.Text = "";
            /*
            richTextBox10.Text = "";
            foreach (string ln in richTextBox7.Lines)
            {
                string data = Regex.Replace(ln, " ", "|");
                richTextBox10.AppendText(data + "\n");
            }
            richTextBox7.Text = richTextBox10.Text;
            richTextBox10.Text = "";
            foreach (string ln in richTextBox8.Lines)
            {
                string data = Regex.Replace(ln, " ", "|");
                richTextBox10.AppendText(data + "\n");
            }

            richTextBox8.Text = richTextBox10.Text;
            */
            //Check for matches
            foreach (var ln in richTextBox7.Lines)
            foreach (var ln2 in richTextBox8.Lines)
                if (string.Equals(ln, ln2))
                    richTextBox9.AppendText(ln2 + " [Found match]\n");
        }

        private void richTextBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                ((RichTextBox) sender).Paste(DataFormats.GetFormat("Text"));
                e.Handled = true;
            }
        }

        private void richTextBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                ((RichTextBox) sender).Paste(DataFormats.GetFormat("Text"));
                e.Handled = true;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Η βασική του χρήση είναι για να ελέγχουμε σε περίπτωση που κάποιο δέμα που είναι απο προηγούμενη αποστολή έχει μείνει αχτύπητο. " +
                "Για να το κάνουμε αυτό χρείαζεται να κρατάμε σε ένα αρχείο excel ένα αντίγραφο από την ημέρα που φορτώθηκαν τα δέματα που θέλουμε" +
                "να ελένξουμε. Όταν θέλουμε να κάνουμε την σύγκριση αντιγράφουμε τα δεδομένα από το excel στο πρώτο πεδίο. Έπειτα βάζουμε τα τρέχοντα φορτωμένα δέματα" +
                "στο 2ο πεδίο. Τέλος πατάμε το κουμπί ελέγχου. Για να αντιγράψουμε τα δεδομένα απ το πινακάκι απλά πατάμε το κουμπί Αντιγραφή!\n" +
                "Όταν έχουμε κρατήσει της προηγούμενης μέρας αλλά έχουν φορτοθεί και της επόμενης μπορούμε να βάλουμε την λίστα με όλα τα φορτομένα " +
                "δέματα στο πρώτο και αύτη που είχαμε κρατήσει στην δεύτερη και πατάμε την \"Δημιουργία λίστας μοναδικών τιμών\" " +
                "\n **ΠΡΟΣΟΧΗ Όταν κρατάμε αντίγραφο απο μία λίστα δεν κρατάμε την πρώτη στήλη με τον αύξοντα αριθμό.**");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
                Clipboard.SetText("**Αποτυχία 1ου SMS - Αποστάλθηκε E-mail " + DateTime.Now.ToString("dd/MM HH:mm"));
            if (radioButton6.Checked)
            {
                var meh = DateTime.Now;
                if (meh.ToString("dddd") == "Friday" || meh.ToString("dddd") == "Saturday" ||
                    meh.ToString("dddd") == "Παρασκευή" || meh.ToString("dddd") == "Σάββατο")
                    meh = meh.AddDays(3);
                else
                    meh = meh.AddDays(2);
                dateTimePicker2.Value = meh;
                var dtp = meh.ToString("dddd dd/MM");
                var ntay = meh.ToString("dddd");
                var doh = "";
                if (ntay == "Monday" || ntay == "Δευτέρα")
                    doh = dtp.Replace(ntay, "την Δευτέρα");
                else if (ntay == "Tuesday" || ntay == "Τρίτη")
                    doh = dtp.Replace(ntay, "την Τρίτη");
                else if (ntay == "Wednesday" || ntay == "Τετάρτη")
                    doh = dtp.Replace(ntay, "την Τετάρτη");
                else if (ntay == "Thursday" || ntay == "Πέμπτη")
                    doh = dtp.Replace(ntay, "την Πέμπτη");
                else if (ntay == "Friday" || ntay == "Παρασκευή")
                    doh = dtp.Replace(ntay, "την Παρασκευή");
                else if (ntay == "Saturday" || ntay == "Σάββατο")
                    doh = dtp.Replace(ntay, "το Σάββατο");
                else
                    doh = dtp;
                Clipboard.SetText(
                    $"**Αποστάλθηκε 2o E-mail {DateTime.Now.ToString("dd/MM HH:mm")} ότι θα παραμείνει μέχρι και {doh}");
            }

            if (radioButton7.Checked)
                Clipboard.SetText("**Αποτυχία 1ου SMS - Αποστάλθηκε E-mail " + DateTime.Now.ToString("dd/MM HH:mm"));
            if (radioButton8.Checked)
                Clipboard.SetText("**Αποστάλθηκε E-mail υπενθύμισης " + DateTime.Now.ToString("dd/MM HH:mm"));
            if (radioButton9.Checked)
                Clipboard.SetText("**Αποστάλθηκε E-mail ώστε να επικοινωνήσει ο πελάτης μαζί μας. " +
                                  DateTime.Now.ToString("dd/MM HH:mm"));
            if (radioButton12.Checked)
                Clipboard.SetText("**Αποστάλθηκε E-mail ώστε να επικοινωνήσει ο πελάτης μαζί μας. " +
                                  DateTime.Now.ToString("dd/MM HH:mm"));
        }

        private void button30_Click(object sender, EventArgs e)
        {
            var listMain = new List<string>();
            var listSecondary = new List<string>();
            var listResult = new List<string>();
            listMain.Clear();
            listMain.AddRange(richTextBox7.Lines);
            listSecondary.Clear();
            listSecondary.AddRange(richTextBox8.Lines);
            listResult.Clear();
            listResult = listMain.Except(listSecondary).ToList();
            richTextBox9.Text = "";
            foreach (var item in listResult) richTextBox9.Text += item + " \n";
        }

        private void richTextBox7_TextChanged(object sender, EventArgs e)
        {
            label52.Text = richTextBox7.Lines.Length.ToString();
        }

        private void richTextBox8_TextChanged(object sender, EventArgs e)
        {
            label53.Text = richTextBox8.Lines.Length.ToString();
        }

        private void richTextBox9_TextChanged(object sender, EventArgs e)
        {
            label54.Text = richTextBox9.Lines.Length.ToString();
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
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "e-Shop Assistant";
            notifyIcon1.BalloonTipText = "Ο πιστός βοηθός είναι ακόμα εδώ!!!";
            notifyIcon1.ShowBalloonTip(2000);
        }

        private void TextBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox9, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox9, false, true, true, true);
            else
                return;
        }

        private void TextBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox10, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox10, false, true, true, true);
            else
                return;
        }

        private void TextBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox11, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox11, false, true, true, true);
            else
                return;
        }

        private void TextBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox12, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox12, false, true, true, true);
            else
                return;
        }

        private void TextBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox13, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox13, false, true, true, true);
            else
                return;
        }

        private void TextBox14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox14, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox14, false, true, true, true);
            else
                return;
        }

        private void TextBox15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox15, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox15, false, true, true, true);
            else
                return;
        }

        private void TextBox16_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox16, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox16, false, true, true, true);
            else
                return;
        }

        private void TextBox17_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox17, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox17, false, true, true, true);
            else
                return;
        }

        private void TextBox18_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox18, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox18, false, true, true, true);
            else
                return;
        }

        private void TextBox19_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox19, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox19, false, true, true, true);
            else
                return;
        }

        private void TextBox20_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox20, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox20, false, true, true, true);
            else
                return;
        }

        private void TextBox21_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox21, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox21, false, true, true, true);
            else
                return;
        }

        private static int Lol(int a)
        {
            int oo = 0;
            string r="";
            char[] dd = a.ToString().ToCharArray();
            foreach (int _d in dd)
            {
                int _ll = _d * _d;
                r += _ll;
            }

            oo = Convert.ToInt32(r);
            return oo;
        }

        private void TextBox22_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox22, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox22, false, true, true, true);
            else
                return;
        }

        private void TextBox23_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox23, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox23, false, true, true, true);
            else
                return;
        }

        private void RadioButton12_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked)
                emailmsg =
                    $"\r\nΣας ενημερώνουμε ότι η παραγγελία σας βρίσκεται στο κατάστημα μας.\r\n Παρακαλώ όπως επικοινωνήσετε μαζί μας στο τηλέφωνο {textBox43.Text}\r\n σχετικά με την παράδοση της. \r\n";
            button28.Enabled = true;
        }

        private void TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox3, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox3, false, true, true, true);
            else
                return;
        }

        private void TextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox4, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox4, false, true, true, true);
            else
                return;
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox1, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox1, false, true, true, true);
            else
                return;
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(textBox2, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(textBox2, false, true, true, true);
            else
                return;
        }

        private void CheckBox11_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked)
            {
                checkBox9.Enabled = false;
                checkBox10.Enabled = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
            }
            else
            {
                checkBox9.Enabled = true;
                checkBox10.Enabled = true;
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            string strInputUser = "ANNOUNCE" + "|" + _orderCode.Text + "|" + _orderName.Text + "|" + _numPrice.Text + "|" + _boxSize.SelectedItem;
            client.SendToServer(strInputUser);
            _orderName.Clear();
            _orderCode.Clear();
            _numPrice.Text = null;
            lastclip = null;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            client = new LahoreSocketClient();
            client.RaiseTextReceivedEvent += HandleTextReceived;
            client.RaiseServerDisconnected += HandleServerDisconnected;
            client.RaiseServerConnected += HandleServerConnected;
            string strIPAddress = textBox5.Text;
            string strPortInput = "23000";

            if (!client.SetServerIPAddress(strIPAddress) ||
                !client.SetPortNumber(strPortInput))
            {
                MessageBox.Show(
                    string.Format(
                        "Wrong IP Address or port number supplied - {0} - {1} - Press a key to exit",
                        strIPAddress,
                        strPortInput));
                
                return;
            }

            client.ConnectToServer();
            button5.Text = "Connected!";
            button5.Enabled = false;
            lastclip = null;
            _announceAid.Checked = true;
            lastname = null;
            lastprice = null;
            lastorder = null;
            _orderName.Text = "";
            _orderCode.Text = "";
            _numPrice.Text = "";
            _boxSize.SelectedIndex = 0;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            client.CloseAndDisconnect();
            button5.Text = "Connect";
            button5.Enabled = true;
            lastclip = null;
            _announceAid.Checked = false;
            lastname = null;
            lastprice = null;
            lastorder = null;
        }

        private void textBox49_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button9.PerformClick();
            else
                return;
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            richTextBox6.Text = "";
        }

        private void button32_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("**Ζήτησε να παραλάβει απ το κατάστημα. " + DateTime.Now.ToString("dd/MM HH:mm"));
            //notifyD("Copied to clipboard:", Clipboard.GetText());
            notifier("Παραλαβή Επιτόπου");
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string dt = null;
            if (radioButton1.Checked)
            {
                dt = "Ο πελάτης ενημερώθηκε";
            }
            if (radioButton2.Checked)
            {
                dt = "Αδυναμία ενημέρωσης";
            }
            Clipboard.SetText("~~" + dt +" " + DateTime.Now.ToString("dd/MM HH:mm") + " " + listBox1.SelectedItem.ToString() +
                              "~~");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(DateTime.Now.ToString("dd/MM HH:mm"));
        }

        private void button34_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                filetoconvert.Text = file;
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string file = folderBrowserDialog1.SelectedPath;
                foldertooutput.Text = file;
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            //Process.Start(@".");
            //Console.WriteLine("{0}\\poppler\\poppler-0.68\\bin\\pdftocairo.exe", Environment.CurrentDirectory);
            string path = String.Format("{0}\\poppler\\poppler-0.68\\bin\\pdftocairo.exe", Environment.CurrentDirectory);
            //Process.Start(@path);
            string tnow = DateTime.Now.ToString("ddMMyy.HH.mm");
            Process.Start(@path, "-jpeg \"" + filetoconvert.Text +"\" \"" + foldertooutput.Text + "/outputimage"+tnow+ "\"");
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                tabControl1.TabPages.Remove(tabPage9);
                tabPage9.Visible = false;
            }
            else 
            {
                tabControl1.TabPages.Add(tabPage9);
                tabPage9.Visible = true;
            }
        }

        private void _emailaid_CheckedChanged(object sender, EventArgs e)
        {
            Clipboard.Clear();
            lastname = null;
            lastemail = null;
            lastorder = null;
            lastclip = null;
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
                    button4.PerformClick();
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

        private void _ppview_Click(object sender, EventArgs e)
        {
            var data = new Dictionary<string, string>()
            {
                {"addressFrom", _storeAddress.Text },
                {"addressTo", "ΑΝΕΜΩΝΗΣ 6. ΤΚ 13671, ΑΧΑΡΝΑΙ" },
                {"storeArea", Properties.Settings.Default._storeArea },
                {"date", _datePicker.Value.ToString("dd/MM/yyyy") },
                {"storeTo","Μενίδι" },
                {"phone",textBox43.Text },
                {"AA", _AAp.Text },
            };
            Form printableForm = new PrintableForm(data);
            printableForm.Show();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cleanServed_Click(object sender, EventArgs e)
        {
            string strInputUser = "CLEANSERVED" + "|DONE";
            client.SendToServer(strInputUser);
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var website = "http://tools.idle.gr/PrintableApp/publish.htm";
            System.Diagnostics.Process.Start(website);
        }
    }

    public class hwid
    {
        public hwid()

        {
            Console.Write("Your HWID: ");
            Console.WriteLine(Value());
            Console.ReadLine();
        }

        public string GetHash(string s)
        {
            //Initialize a new MD5 Crypto Service Provider in order to generate a hash
            MD5 sec = new MD5CryptoServiceProvider();
            //Grab the bytes of the variable 's'
            byte[] bt = Encoding.ASCII.GetBytes(s);
            //Grab the Hexadecimal value of the MD5 hash
            return GetHexString(sec.ComputeHash(bt));
        }

        private static string GetHexString(IList<byte> bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Count; i++)
            {
                byte b = bt[i];
                int n = b;
                int n1 = n & 15;
                int n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + 'A')).ToString(CultureInfo.InvariantCulture);
                else
                    s += n2.ToString(CultureInfo.InvariantCulture);
                if (n1 > 9)
                    s += ((char)(n1 - 10 + 'A')).ToString(CultureInfo.InvariantCulture);
                else
                    s += n1.ToString(CultureInfo.InvariantCulture);
                if ((i + 1) != bt.Count && (i + 1) % 2 == 0) s += "-";
            }

            return s;
        }

        private static string _fingerPrint = string.Empty;

        public string Value()
        {
            //You don't need to generate the HWID again if it has already been generated. This is better for performance
            //Also, your HWID generally doesn't change when your computer is turned on but it can happen.
            //It's up to you if you want to keep generating a HWID or not if the function is called.
            if (string.IsNullOrEmpty(_fingerPrint))
            {
                _fingerPrint = GetHash("CPU >> " + CpuId() + "\nBIOS >> " + BiosId() + "\nBASE >> " + BaseId() +
                                       "\nDISK >> " + DiskId() + "\nVIDEO >> " + VideoId() + "\nMAC >> " + MacId());
            }

            return _fingerPrint;
        }

        //Return a hardware identifier
        private static string Identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementBaseObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() != "True") continue;
                //Only get the first one
                if (result != "") continue;
                try
                {
                    result = mo[wmiProperty].ToString();
                    break;
                }
                catch
                {
                }
            }

            return result;
        }

        //Return a hardware identifier
        private static string Identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementBaseObject mo in moc)
            {
                //Only get the first one
                if (result != "") continue;
                try
                {
                    result = mo[wmiProperty].ToString();
                    break;
                }
                catch
                {
                }
            }

            return result;
        }

        private static string CpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = Identifier("Win32_Processor", "UniqueId");
            if (retVal != "") return retVal;
            retVal = Identifier("Win32_Processor", "ProcessorId");
            if (retVal != "") return retVal;
            retVal = Identifier("Win32_Processor", "Name");
            if (retVal == "") //If no Name, use Manufacturer
            {
                retVal = Identifier("Win32_Processor", "Manufacturer");
            }

            //Add clock speed for extra security
            retVal += Identifier("Win32_Processor", "MaxClockSpeed");
            return retVal;
        }

        //BIOS Identifier
        private static string BiosId()
        {
            return Identifier("Win32_BIOS", "Manufacturer") + Identifier("Win32_BIOS", "SMBIOSBIOSVersion") +
                   Identifier("Win32_BIOS", "IdentificationCode") + Identifier("Win32_BIOS", "SerialNumber") +
                   Identifier("Win32_BIOS", "ReleaseDate") + Identifier("Win32_BIOS", "Version");
        }

        //Main physical hard drive ID
        private static string DiskId()
        {
            return Identifier("Win32_DiskDrive", "Model") + Identifier("Win32_DiskDrive", "Manufacturer") +
                   Identifier("Win32_DiskDrive", "Signature") + Identifier("Win32_DiskDrive", "TotalHeads");
        }

        //Motherboard ID
        private static string BaseId()
        {
            return Identifier("Win32_BaseBoard", "Model") + Identifier("Win32_BaseBoard", "Manufacturer") +
                   Identifier("Win32_BaseBoard", "Name") + Identifier("Win32_BaseBoard", "SerialNumber");
        }

        //Primary video controller ID
        private static string VideoId()
        {
            return Identifier("Win32_VideoController", "DriverVersion") + Identifier("Win32_VideoController", "Name");
        }

        //First enabled network card ID
        private static string MacId()
        {
            return Identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }
        
    }
    
}