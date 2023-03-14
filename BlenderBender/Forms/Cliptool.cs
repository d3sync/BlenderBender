using System;
using System.Windows.Forms;

namespace BlenderBender
{
    public partial class Cliptool : Form
    {
        public string lastclip;

        public Cliptool()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = DateTime.Now.ToString();
            if (_monitor.Checked)
            {
                var iData = Clipboard.GetDataObject();
                if (Clipboard.GetDataObject() != null && (string)iData.GetData(DataFormats.Text) != lastclip)
                    // Is Data Text?
                    if (iData.GetDataPresent(DataFormats.Text))
                    {
                        //richTextBox6.Text += (string)iData.GetData(DataFormats.Text) + "\r\n";
                        listBox1.Items.Insert(0, (string)iData.GetData(DataFormats.Text));
                        lastclip = (string)iData.GetData(DataFormats.Text);
                    }

                lastclip = (string)iData.GetData(DataFormats.Text);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Clipboard.SetText(listBox1.SelectedItem.ToString());
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void Cliptool_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            Hide();
        }
    }
}