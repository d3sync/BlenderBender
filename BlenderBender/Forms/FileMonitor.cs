using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlenderBender;

namespace BlenderBender.Forms
{
    public partial class FileMonitor : Form
    {
        public MainWindow mf;
        public FileMonitor(MainWindow mf)
        {
            InitializeComponent();
            this.mf = mf;
            checkBox1.Checked = Properties.Settings.Default.filemonitor;
            fileSystemWatcher1.Path = Path.GetFullPath(Properties.Settings.Default.monitorfolder);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.filemonitor = fileSystemWatcher1.EnableRaisingEvents = checkBox1.Checked;
            Properties.Settings.Default.Save();
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.WindowsShutDown)
            {
                this.Hide();
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }
    }
}
