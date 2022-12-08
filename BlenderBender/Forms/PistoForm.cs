using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlenderBender.Forms
{
    public partial class PistoForm : Form
    {
        public CultureInfo cCulture = CultureInfo.CurrentCulture;
        public NumberStyles nStyles = NumberStyles.AllowDecimalPoint;
        private Form mf;
        public PistoForm(Form mf)
        {
            InitializeComponent();
            this.mf = mf;
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

        private void button25_Click(object sender, EventArgs e) => ClearTextBoxes();

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "") Clipboard.SetText(richTextBox2.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "") Clipboard.SetText(richTextBox1.Text);
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
                            if (Properties.Settings.Default.PistoMsg)
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
                            if (Properties.Settings.Default.PistoMsg)
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
        private void textBoxRdots_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Properties.Settings.Default.windowsWeirdness)
                if (e.KeyChar == '.')
                    e.KeyChar = ',';
        }
        
        private void PistoForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }
    }
}
