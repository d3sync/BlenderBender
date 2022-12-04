using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlenderBender.Class;
using BlenderBender.Forms;
using System.Text.RegularExpressions;
using BlenderBender.Properties;

namespace BlenderBender
{
    public partial class MainWindow : Form
    {
        private int childFormNumber = 0;
        public int hold;
        public MessagesForm mes;
        public MainWindow()
        {
            InitializeComponent();
        }
        public void notifier(string message, ToolTipIcon d = ToolTipIcon.Info, int option = 0)
        {
            var data = new Dictionary<int, string>()
            {
                {0,"Αντιγράφθηκε στο Πρόχειρο: "},
                {1,"Ενημέρωση: "},
                {2,"Πρόβλημα: "}
            };
            notifyIcon1.BalloonTipIcon = d;
            notifyIcon1.BalloonTipTitle = "e-Shop Assistant";
            notifyIcon1.BalloonTipText = String.Format("{0} {1}",data[option],message);
            notifyIcon1.ShowBalloonTip(2000);
        }
        public int countdown { get; set; }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void messagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.MdiParent = this;
            f1.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            calculateForm calc = new calculateForm(this);
            calc.MdiParent = this;
            calc.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.MdiParent = this;
            about.Show();
        }

        private void notesStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            mes = new MessagesForm(this);
            mes.MdiParent = this;
            mes.Show();
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
                    if (this.MdiChildren.Length > 0)
                    {
                        var children = this.MdiChildren;
                        foreach (var child in children)
                        {
                            if (child.GetType() == typeof(MessagesForm))
                                mes.dateTimePicker3.Value = DateTime.Now;
                        }
                    }
                    Clipboard.Clear();
                }
            }
            hold = 0;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm sf = new SettingsForm();
            sf.MdiParent = this;
            sf.Show();
        }

        private void emailStripButton3_Click(object sender, EventArgs e)
        {
            EmailForm ef = new EmailForm(this);
            ef.MdiParent = this;
            ef.Show();
        }

        private void pistoStripButton4_Click(object sender, EventArgs e)
        {
            PistoForm pf = new PistoForm(this);
            pf.MdiParent = this;
            pf.Show();
        }
    }
}
