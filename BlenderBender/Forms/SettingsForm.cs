using System;
using System.Windows.Forms;
using BlenderBender.Class;
using BlenderBender.Properties;
using Microsoft.Win32;

namespace BlenderBender.Forms
{
    public partial class SettingsForm : Form    
    {
        public UserClass user;

        public SettingsForm()
        {
            InitializeComponent();
            this.KeyDown += Close_KeyDown;
            cmbKnown();
            cmbKnownUsers.Text = Settings.Default.User ?? "Αγνωστός Χειριστής";
            chkBreakFree.Checked = Settings.Default.BreakFree;
            var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\e-ShopAssistant");
            if (key != null)
            {
                if (key.GetValue("Phone") != null)
                    textBox43.Text = key.GetValue("Phone").ToString();
                else
                    textBox43.Text = "2115000500";
                if (key.GetValue("kava") != null)
                    textBox46.Text = key.GetValue("kava").ToString();
                else
                    textBox46.Text = "0";
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

            chkBreakFree.Checked = Settings.Default.BreakFree;
            checkBox2.Checked = Settings.Default.windowsWeirdness;
            richTextBox3.Text = Settings.Default.Signature;
        }
        private void Close_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\e-ShopAssistant");
            key.SetValue("Phone", textBox43.Text);
            key.SetValue("kava", textBox46.Text);
            key.SetValue("ESHOP_ONE", textBox6.Text);
            key.SetValue("ESHOP_SHOP", textBox48.Text);
            key.SetValue("MAIL_ADDRESS", textBox47.Text);
            key.SetValue("REPLACE_ON_MAIL", checkBox8.Checked.ToString());
            key.Close();
            Settings.Default._storeAddress = _storeAddress.Text;
            Settings.Default.Signature = richTextBox3.Text;
            Settings.Default.windowsWeirdness = checkBox2.Checked;
            //Properties.Settings.Default._storeArea = _txtFrom.Text;
            Settings.Default.Save();
            //_txtFrom.Text = _storeAddress.Text;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            var users = Settings.Default.KnownUsers;
            if (txtName.Text != null)
            {
                users.Add(txtName.Text.Trim());
                Settings.Default.KnownUsers = users;
                Settings.Default.Save();
            }

            cmbKnown();
        }

        private void cmbKnown()
        {
            cmbKnownUsers.Items.Clear();
            foreach (var item in Settings.Default.KnownUsers)
                if (!string.IsNullOrEmpty(item))
                    cmbKnownUsers.Items.Add(item);
        }

        private void btnDelUser_Click(object sender, EventArgs e)
        {
            var res = Settings.Default.KnownUsers.Remove(cmbKnownUsers.SelectedItem.ToString());
            Settings.Default.Save();
            cmbKnown();
            if (res)
                MessageBox.Show($"Διαγραφή χρήστη {cmbKnownUsers.SelectedItem}");
        }

        private void cmbKnownUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.User = cmbKnownUsers.SelectedItem.ToString();
            Settings.Default.Save();
        }
        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void chkBreakFree_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.BreakFree = chkBreakFree.Checked;
            Settings.Default.Save();
            if (chkBreakFree.Checked)
            {
                this.Hide();
                this.MdiParent = null;
                this.Show();
            }
        }
    }
}