using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using BlenderBender.Properties;

namespace BlenderBender.Forms
{
    public partial class FileMonitor : Form
    {
        public MainWindow mf;

        //public List<FileModel> files = new List<FileModel>();
        //public List<FileModel> classified = new List<FileModel>();
        public FileMonitor(MainWindow mf)
        {
            InitializeComponent();
            this.mf = mf;
            checkBox1.Checked = Settings.Default.filemonitor;
            label40.Text = Settings.Default.monitorfolder;
            fileSystemWatcher1.Path = Path.GetFullPath(Settings.Default.monitorfolder);
            //backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            //backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            //backgroundWorker1.WorkerReportsProgress = true;
            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowser_DocumentCompleted);
        }
        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Set the zoom level to fit the entire page
            webBrowser1.Document.Body.Style = "zoom:80%"; // Change the zoom percentage as required
        }

        //public void ClassifyFile(FileModel lvi)
        //{

        //    var ocrTesseract = new IronTesseract()
        //    {
        //        Language = OcrLanguage.GreekBest,
        //        Configuration = new TesseractConfiguration()
        //        {
        //            ReadBarCodes = true,
        //            RenderSearchablePdfsAndHocr = true,
        //            PageSegmentationMode = TesseractPageSegmentationMode.AutoOsd,
        //        }
        //    };
        //    using (var ocrInput = new OcrInput(lvi.FullPath))
        //    {
        //        try
        //        {
        //            var ocrResult = ocrTesseract.Read(ocrInput);
        //            var barcodes = ocrResult.Barcodes.ToList();


        //            bool hasbarcodes = barcodes.Count > 0;
        //            if (hasbarcodes)
        //            {
        //                lvi.HasBarcodes = "true";
        //            }

        //            if (ocrResult.Text.Contains("ΑΠΟΔΕΙΞΗ ΕΙΣΠΡΑΞΗΣ") || ocrResult.Text.Contains("ΔΕΛΤΙΟ ΑΠΟΣΤΟΛΗΣ") ||
        //                ocrResult.Text.Contains("ΑΠΟΔΕΙΞΗ ΛΙΑΝΙΚΗΣ ΠΩΛΗΣΗΣ - Δ. ΑΠΟΣΤΟΛΗ") ||
        //                ocrResult.Text.Contains("ΑΠΟΔΕΙΞΗ ΛΙΑΝΙΚΗΣ ΠΩΛΗΣΗΣ") ||
        //                ocrResult.Text.Contains("ΔΕΛΤΙΟ ΑΠΟΣΤΟΛΗΣ"))
        //                lvi.Classification = "ΠΑΡΑΣΤΑΤΙΚΟ";
        //            if (ocrResult.Text.Contains("pc1.gr"))
        //                lvi.Classification += "για pc1.gr";
        //            if (ocrResult.Text.Contains("e-shop.gr"))
        //                lvi.Classification += "για e-shop.gr";
        //            if ((ocrResult.Text.Contains("Cleaning") && ocrResult.Text.Contains("Pro")) ||
        //                (ocrResult.Text.Contains("ΤΙΜΟΛΟΓΙΟ")) && (ocrResult.Text.Contains("ΠΑΡΟΧΗΣ")) &&
        //                (ocrResult.Text.Contains("ΚΑΘ/ΤΗΤΑ")) && (ocrResult.Text.Contains("ΤΖΑΜΙΩΝ")))
        //                lvi.Classification = "Έξοδο Καθαρισμός τζαμιών";
        //            if (ocrResult.Text.Contains("ΠΙΣΤΟΠΟΙΗΜΕΝΟΣ ΕΠΑΓΓΕΛΜΑΤΙΑΣ") ||
        //                ocrResult.Text.Contains("Πιστοποιημένος Επαγγελματίας") ||
        //                ocrResult.Text.Contains("ΑΛΕΞΑΝΔΡΟΣ"))
        //                lvi.Classification = "Πιστοποιητικό.";
        //            else
        //            {
        //                lvi.Classification = "Αποτυχία ελέγχου, άγνωστο περιεχόμενο";
        //            }

        //            classified.Add(lvi);

        //            Console.WriteLine(ocrResult.Text);
        //        }
        //        catch { lvi.Classification = "Αποτυχία ελέγχου, άγνωστο περιεχόμενο"; }
        //    }
        //}

        //private void DisplayInListView()
        //{
        //    try
        //    {

        //        listView1.Items.Clear();
        //        foreach (var lvi in classified)
        //        {
        //            FormControlHelper.ControlInvoke(listView1, () => listView1.Items.Add(new ListViewItem(new string[]
        //                { lvi.Name, lvi.FullPath, lvi.ChangeType.ToString(), "", lvi.Classification, lvi.HasBarcodes})));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message, ex.Data);
        //    }
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            //DisplayInListView();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //if (files.Count > 0)
            //{
            //    var i = 0;
            //    backgroundWorker1.ReportProgress(i);
            //    foreach (var file in files)
            //    {
            //        ClassifyFile(file);
            //        i += 100 / files.Count();
            //        backgroundWorker1.ReportProgress(i);
            //    }
            //}
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Settings.Default.monitorfolder = folderBrowserDialog1.SelectedPath;
                Settings.Default.Save();
                label40.Text = Settings.Default.monitorfolder;
                fileSystemWatcher1.Path = Path.GetFullPath(Settings.Default.monitorfolder);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.filemonitor = fileSystemWatcher1.EnableRaisingEvents = checkBox1.Checked;
            Settings.Default.Save();
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            listView1.Items.Add(new ListViewItem(new[] { e.Name, e.FullPath, e.ChangeType.ToString() }));
            //files.Add(new FileModel()
            //{
            //    Name = e.Name,
            //    FullPath = e.FullPath,
            //    //FileExtension = e.FullPath.Split('.')[e.FullPath.Split('.').Length - 1],
            //    ChangeType = e.ChangeType.ToString(),
            //    Classification = "",
            //    HasBarcodes = "undetermined"
            //});
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            //listView1.Items.Add(new ListViewItem(new string[] { e.Name, e.FullPath, e.ChangeType.ToString() }));
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            listView1.Items.Add(new ListViewItem(new[] { e.Name, e.FullPath, e.ChangeType.ToString(), e.OldName }));
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            try
            {
                //Rename Tautotita
                if (listView1.SelectedItems[0].SubItems[1] != null)
                {
                    var extension = Path.GetExtension(listView1.SelectedItems[0].SubItems[1].Text);
                    File.Move(listView1.SelectedItems[0].SubItems[1].Text,
                        fileSystemWatcher1.Path + "\\IDENTITY - " + DateTime.Now.ToString("ddMMyyyyhhmmss") +
                        extension);
                    listView1.SelectedItems[0].Remove();
                }
            }
            catch
            {
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems[0].SubItems[1] != null)
                {
                    var extension = Path.GetExtension(listView1.SelectedItems[0].SubItems[1].Text);
                    File.Move(listView1.SelectedItems[0].SubItems[1].Text,
                        fileSystemWatcher1.Path + "\\ΕΞΟΔΟ - " + DateTime.Now.ToString("dd.MM.yyyy-hhmmss") +
                        extension);
                    listView1.SelectedItems[0].Remove();
                }
            }
            catch
            {
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems[0].SubItems[1] != null)
                {
                    var extension = Path.GetExtension(listView1.SelectedItems[0].SubItems[1].Text);
                    File.Move(listView1.SelectedItems[0].SubItems[1].Text,
                        fileSystemWatcher1.Path + "\\TICKET COMPLIMENTS - " + DateTime.Now.ToString("ddMMyyyyhhmmss") +
                        extension);
                    listView1.SelectedItems[0].Remove();
                }
            }
            catch
            {
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems[0].SubItems[1] != null) listView1.SelectedItems[0].Remove();
            }
            catch
            {
            }
        }

        private void CopyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView1.SelectedItems[0].SubItems[1].Text);
        }

        private void toolStripSeparator4_Click(object sender, EventArgs e)
        {
            var extension = Path.GetExtension(listView1.SelectedItems[0].SubItems[1].Text);
            File.Move(listView1.SelectedItems[0].SubItems[1].Text,
                fileSystemWatcher1.Path + "\\ΑΠΟΔΕΙΞΗ ΠΡΟΕΙΣΠΡΑΞΗΣ - " + DateTime.Now.ToString("ddMMyyyyhhmmss") +
                extension);
            listView1.SelectedItems[0].Remove();
        }

        private void MoveFile(string dstName)
        {
            try
            {
                var directory = fileSystemWatcher1.Path + $"\\{dstName} {DateTime.Now.ToString("MMMM yyyy")}";
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                var filename = Path.GetFileName(listView1.SelectedItems[0].SubItems[1].Text);
                File.Move(listView1.SelectedItems[0].SubItems[1].Text, Path.Combine(directory, filename));
                listView1.SelectedItems[0].Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "There is ur problem");
            }
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
                Hide();
                e.Cancel = true;
            }

            base.OnFormClosing(e);
        }

        private void FileMonitor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Hide();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[1].Text != "" ||
                listView1.SelectedItems[0].SubItems[1].Text != null)
            {
                saveFileDialog1.InitialDirectory = Settings.Default.monitorfolder;
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                webBrowser1.Visible = true;
                    webBrowser1.Navigate("file:///" + listView1.SelectedItems[0].SubItems[1].Text);
                    
            }
            catch
            {
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[1].Text != "" ||
                listView1.SelectedItems[0].SubItems[1].Text != null)
            {
                saveFileDialog1.InitialDirectory = Settings.Default.monitorfolder;
                var extension = Path.GetExtension(listView1.SelectedItems[0].SubItems[1].Text);
                saveFileDialog1.FileName = $"ΕΞΟΔΟ - (ΤΑΔΕ) - (ΤΟΣΑ) ΕΥΡΩ - {DateTime.Now.ToString("dd.MM.yyyy")}";
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
        //class FormControlHelper
        //{
        //    delegate void UniversalVoidDelegate();

        //    /// <summary>
        //    /// Call form control action from different thread
        //    /// </summary>
        //    public static void ControlInvoke(Control control, Action function)
        //    {
        //        if (control.IsDisposed || control.Disposing)
        //            return;

        //        if (control.InvokeRequired)
        //        {
        //            control.Invoke(new UniversalVoidDelegate(() => ControlInvoke(control, function)));
        //            return;
        //        }
        //        function();
        //    }
        //}
    }
}