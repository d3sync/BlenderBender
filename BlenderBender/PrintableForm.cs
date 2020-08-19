using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetBarcode;
//using Type = System.Type;

namespace BlenderBender
{
    public partial class PrintableForm : Form
    {
        public PrintableForm(Dictionary<string,string> data)
        {
            InitializeComponent();
            /*                {"addressFrom", _storeAddress.Text },
                {"addressTo", "unknown 69" },
                {"storeArea", Properties.Settings.Default._storeArea },
                {"date", _datePicker.Value.ToString("dd/MM/YYYY") },
                {"storeTo","Μενίδι" },
                {"AA", _AAp.Text },
                */
            txtAddressFrom.Text = data["addressFrom"];
            txtAddressTo.Text = data["addressTo"];
            txtFrom.Text = data["storeArea"];
            _txtDate.Text = data["date"];
            _txtTo.Text = data["storeTo"];
            _txtAA.Text = data["AA"];
            _txtPhone.Text = "ΤΗΛ. " + data["phone"];
        }

        private void PrintableForm_Load(object sender, EventArgs e)
        {

        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        private Bitmap memoryImage;
        private void CaptureScreen()
        {
            Graphics mygraphics = panel1.CreateGraphics();
            Size s = panel1.Size;
            memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            IntPtr dc1 = mygraphics.GetHdc();
            IntPtr dc2 = memoryGraphics.GetHdc();
            BitBlt(dc2, 0, 0, panel1.ClientRectangle.Width, panel1.ClientRectangle.Height, dc1, 0, 0, 13369376);
            mygraphics.ReleaseHdc(dc1);
            memoryGraphics.ReleaseHdc(dc2);
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var barcode = new Barcode(_txtAA.Text, NetBarcode.Type.Code93, true);
                //barcode.SaveImageFile("./lol.png", ImageFormat.Png);
                byte[] bytes = Convert.FromBase64String(barcode.GetBase64Image());

                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            catch
            {
                _txtAA.Text = "";
            }
            //pictureBox1.Image = Image.FromFile("./lol.png");

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (!_pall.Checked)
            {
                if ((_txtTotal.Value > 0) && (_txtCurrent.Value > 0) && (_txtCurrent.Value < _txtCurrent.Maximum))
                {
                    if (_txtCurrent.Value <= _txtTotal.Value)
                    {
                        _labelNprint.Text = _txtCurrent.Value + " / " + _txtTotal.Value;
                        CaptureScreen();
                        printDocument1.DefaultPageSettings.Landscape = true;
                        printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                        printDocument1.Print();
                        if (_txtCurrent.Value <= _txtTotal.Value)
                        {
                            _txtCurrent.Value += 1;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Νομίζω πως τα εκτύπωσες όλα!");
                }
            }
            else
            {
                if ((_txtTotal.Value > 0) && (_txtCurrent.Value > 0))
                {
                    while (_txtCurrent.Value <= _txtTotal.Value)
                    {
                        _labelNprint.Text = _txtCurrent.Value + " / " + _txtTotal.Value;
                        CaptureScreen();
                        printDocument1.DefaultPageSettings.Landscape = true;
                        printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                        try
                        {
                            _txtCurrent.Value += 1;
                        }
                        catch
                        {
                            continue;

                        }
                    }
                    printDocument1.Print();
                }
            }
            printDocument1.Dispose();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAddressTo_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _txtTotal_ValueChanged(object sender, EventArgs e)
        {
            _labelNprint.Text = _txtCurrent.Value + " / " + _txtTotal.Value;
            _txtCurrent.Maximum = _txtTotal.Value +1;
        }

        private void _txtCurrent_ValueChanged(object sender, EventArgs e)
        {
            _labelNprint.Text = _txtCurrent.Value + " / " + _txtTotal.Value;
        }
    }
}
