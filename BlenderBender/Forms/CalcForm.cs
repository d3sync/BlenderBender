using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BlenderBender.Properties;

namespace BlenderBender
{
    public partial class calculateForm : Form
    {
        public Form _m1;
        public CultureInfo cCulture = CultureInfo.CurrentCulture;
        public NumberStyles nStyles = NumberStyles.AllowDecimalPoint;

        public calculateForm(Form m1)
        {
            InitializeComponent();
            _m1 = m1;
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

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (Regex.IsMatch(tb.Text, @"^\d+$"))
            {
                button8.PerformClick();
            }
            else
            {
                tb.Text = "0";
                MessageBox.Show("Συμπληρώνουμε μόνο ακέραιους αριθμούς!");
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
            var tb = (TextBox)sender;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                SelectNextControl(tb, true, true, true, true);
            else if (e.KeyCode == Keys.Up)
                SelectNextControl(tb, false, true, true, true);
            else
                return;
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
            var sum = double.Parse(textBox24.Text, nStyles, cCulture)
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

        private void clrBtn_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void textBoxRdots_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Settings.Default.windowsWeirdness)
                if (e.KeyChar == '.')
                    e.KeyChar = ',';
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}