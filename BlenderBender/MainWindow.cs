using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using BlenderBender.Forms;

namespace BlenderBender
{
    public partial class MainWindow : Form
    {
        private int childFormNumber;
        public FileMonitor fmonitor;
        public int hold;
        public MessagesForm mes;

        public MainWindow()
        {
            InitializeComponent();
            CreateDefaultTxtFile();
            fmonitor = new FileMonitor(this);
            fmonitor.MdiParent = this;
        }

        public int countdown { get; set; }

        public void notifier(string message, ToolTipIcon d = ToolTipIcon.Info, int option = 0)
        {
            var data = new Dictionary<int, string>
            {
                { 0, "Αντιγράφθηκε στο Πρόχειρο: " },
                { 1, "Ενημέρωση: " },
                { 2, "Πρόβλημα: " }
            };
            notifyIcon1.BalloonTipIcon = d;
            notifyIcon1.BalloonTipTitle = "e-Shop Assistant";
            notifyIcon1.BalloonTipText = string.Format("{0} {1}", data[option], message);
            notifyIcon1.ShowBalloonTip(2000);
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            var childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private string GetSettingsFile()
        {
            var folder = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\e-ShopAssistant";
            var settings = $@"{folder}\Settings.ini";
            if (File.Exists(settings))
                return settings;
            CreateDefaultTxtFile();
            return settings;
        }

        private void OpenFile(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var childForm in MdiChildren) childForm.Close();
        }

        private void messagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f1 = new Form1();
            f1.MdiParent = this;
            f1.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            var calc = new calculateForm(this);
            calc.MdiParent = this;
            calc.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new About();
            about.MdiParent = this;
            about.Show();
        }

        private void notesStripButton2_Click(object sender, EventArgs e)
        {
            Process.Start(GetSettingsFile());
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            mes = new MessagesForm(this);
            mes.MdiParent = this;
            mes.Show();
        }

        public void CreateDefaultTxtFile()
        {
            var template = new List<string>
            {
                "##################################################################################################",
                "##################################################################################################",
                "###                                ΑΡΧΕΙΟ ΕΤΟΙΜΩΝ ΚΕΙΜΕΝΩΝ    (ΔΟΚΙΜΑΣΤΙΚΟ)                    ###",
                "##################################################################################################",
                "### ΤΟ ΚΕΙΜΕΝΟ ΠΟΥ ΞΕΚΙΝΑΕΙ ΑΠΟ '#' ΔΕΝ ΑΝΑΓΝΩΡΙΖΕΤΑΙ ΕΙΝΑΙ ΜΟΝΟ ΓΙΑ ΣΗΜΕΙΩΣΕΙΣ                ###",
                "### ΥΠΑΡΧΟΥΝ ΣΥΓΚΕΚΡΙΜΕΝΑ TAGS ΤΑ ΟΠΟΙΑ ΓΙΝΟΝΤΑΙ REPLACE ΚΑΤΑ ΤΟ ΦΟΡΤΩΜΑ ΤΩΝ ΚΕΙΜΕΝΩΝ ΣΤΟ APP  ###",
                "### * [phone] ΓΙΑ ΝΑ ΠΡΟΣΘΕΣΕΤΕ ΤΟ ΤΗΛΕΦΩΝΟ ΤΟΥ ΚΑΤΑΣΤΗΜΑΤΟΣ                                   ###",
                "### * [mphone] ΓΙΑ ΝΑ ΠΡΟΣΘΕΣΕΤΕ ΤΟ ΚΕΝΤΡΙΚΟ ΤΗΛΕΦΩΝΟ ΤΟΥ E-SHOP.GR                            ###",
                "### * [fdate] ΗΜΕΡΟΜΗΝΙΑ ΣΤΟ ΜΕΛΛΟΝ ΠΡΟΕΠΙΛΟΓΗ +2 Ή ΠΑΡΑΠΑΝΩ ΕΠΙΛΕΓΟΝΤΑΣ ΤΟ ΑΠ ΤΟ COMBOBOX     ###",
                "### * [user]  ΒΑΖΕΙ ΤΟ ΟΝΟΜΑ ΤΟΥ ΧΕΙΡΙΣΤΗ ΠΟΥ ΕΙΝΑΙ ΔΗΛΩΜΕΝΟ ΣΤΗΝ ΕΦΑΡΜΟΓΗ                     ###",
                "### * [datetime-user] ΗΜΕΡΟΜΗΝΙΑ ΩΡΑ ΚΑΙ ΧΕΙΡΙΣΤΗΣ                                             ###",
                "### * [date-user]     ΗΜΕΡΟΜΗΝΙΑ ΚΑΙ ΧΕΙΡΙΣΤΗΣ                                                 ###",
                "### * [newline]     ΑΛΛΑΓΗ ΓΡΑΜΜΗΣ                                                             ###",
                "##################################################################################################",
                "### ΓΙΑ ΤΗΝ ΚΩΔΙΚΟΠΟΙΗΣΗ ΤΩΝ ΚΕΙΜΕΝΩΝ ΥΠΑΡΧΟΥΝ 2 ΠΕΔΙΑ ΧΩΡΙΣΜΕΝΑ ΜΕ '|'                        ###",
                "### ΠΕΔΙΟ1: ΟΝΟΜΑ ΚΩΔΙΚΟΠΟΙΗΣΗΣ | ΠΕΔΙΟ2: ΚΕΙΜΕΝΟ ΑΠΟΣΤΟΛΗΣ                                    ###",
                "### i.e. ΕΝΗΜ. ΓΙΑ ΕΠΙΚ.|Παρακαλούμε επικοινωνήστε μαζί μας τηλ [phone]                        ###",
                "### Η κωδικοποίηση αφορά μόνο τό κυρίως σώμα του μηνύματος και όχι τα βασικά  (καλησπέρα κτλ)  ###",
                "##################################################################################################",
                "##################################################################################################"
            };
            var folder = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\e-ShopAssistant";
            var settings = $@"{folder}\Settings.ini";
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            if (!File.Exists(settings))
                using (var sw = File.CreateText(settings))
                {
                    foreach (var item in template) sw.WriteLine(item);
                    sw.WriteLine("ΕΝΗΜ. ΓΙΑ ΕΠΙΚ.(ΤΕΣΤ)|Παρακαλούμε επικοινωνήστε μαζί μας στο τηλ [phone]");
                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //toolStripProgressBar1.Value = i++;
            //if (i >= 100) { i = 0; }
            toolStripStatusLabel.Text = DateTime.Now.ToString();

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
                    if (MdiChildren.Length > 0)
                    {
                        var children = MdiChildren;
                        foreach (var child in children)
                            if (child.GetType() == typeof(MessagesForm))
                                mes.dateTimePicker3.Value = DateTime.Now;
                    }

                    Clipboard.Clear();
                }
            }

            hold = 0;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sf = new SettingsForm();
            sf.MdiParent = this;
            sf.Show();
        }

        private void emailStripButton3_Click(object sender, EventArgs e)
        {
            var ef = new EmailForm(this);
            ef.MdiParent = this;
            ef.Show();
        }

        private void pistoStripButton4_Click(object sender, EventArgs e)
        {
            var pf = new PistoForm(this);
            pf.MdiParent = this;
            pf.Show();
        }

        private void toolStripBtnFmonitor_Click(object sender, EventArgs e)
        {
            fmonitor.Show();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.D1:
                        break;
                    case Keys.D2:
                        break;
                    case Keys.D3:
                        break;
                    case Keys.D4:
                        break;
                    case Keys.D5:
                        break;
                    case Keys.D6:
                        break;
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Process.Start("calc.exe");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe");
        }

        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void messagesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripButton1.PerformClick();
        }

        private void emailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            emailStripButton3.PerformClick();
        }

        private void πιστωτικάToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pistoStripButton4.PerformClick();
        }

        private void υπολογισμόςΧρημάτωνToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton5.PerformClick();
        }

        private void toolStripMenuItemFM_Click(object sender, EventArgs e)
        {
            toolStripBtnFmonitor.PerformClick();
        }
    }
}