using System;
using System.Collections.Generic;
using System.Linq;
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
            cmbKnown2();
            cmbKnownUsers.Text = Settings.Default.User ?? "Αγνωστός Χειριστής";
            chkBreakFree.Checked = Settings.Default.BreakFree;
            var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\e-ShopAssistant");
            if (key != null)
            {
                textBox43.Text = key.GetValue("Phone") != null ? key.GetValue("Phone").ToString() : "2115000500";
                textBox46.Text = key.GetValue("kava") != null ? key.GetValue("kava").ToString() : "0";
                textBox48.Text = key.GetValue("ESHOP_SHOP") != null ? key.GetValue("ESHOP_SHOP").ToString() : "ΚΑΤΑΣΤΗΜΑ ΡΟΔΟΥ";
                textBox6.Text = key.GetValue("ESHOP_ONE") != null ? key.GetValue("ESHOP_ONE").ToString() : " - Η ΠΑΡΑΓΓΕΛΙΑ ΣΑΣ ΕΙΝΑΙ ΕΤΟΙΜΗ. ΜΠΟΡΕΙΤΕ ΝΑ ΠΕΡΑΣΕΤΕ ΝΑ ΤΗΝ ΠΑΡΑΛΑΒΕΤΕ.";
                textBox43.Text = key.GetValue("Phone") != null ? key.GetValue("Phone").ToString() : "2115000500";
                if (key.GetValue("MAIL_ADDRESS") != null) textBox47.Text = key.GetValue("MAIL_ADDRESS").ToString();
                checkBox8.Checked = key.GetValue("REPLACE_ON_MAIL") == null || Convert.ToBoolean(key.GetValue("REPLACE_ON_MAIL"));

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
            try
            {
                var usr = Settings.Default.KnownUsers2.Split('|').ToList();
                if (usr.Count > 0)
                {
                    usr.Add(txtName.Text.Trim());
                }
                else
                {
                    usr.Add(txtName.Text.Trim());
                }

                Settings.Default.KnownUsers2 = string.Join("|", usr);
                Settings.Default.Save();
                cmbKnown2();
            }
            catch
            {
            }
        }

        private void cmbKnown()
        {
            try
            {
                cmbKnownUsers.Items.Clear();
                foreach (var item in Settings.Default.KnownUsers)
                    if (!string.IsNullOrEmpty(item))
                        cmbKnownUsers.Items.Add(item);
            }
            catch { }
        }
        private void cmbKnown2()
        {
            try
            {
                cmbKnownUsers.Items.Clear();
                foreach (var item in Settings.Default.KnownUsers2.Split('|').ToList())
                    if (!string.IsNullOrEmpty(item))
                        cmbKnownUsers.Items.Add(item);
            }
            catch { }
        }

        private void btnDelUser_Click(object sender, EventArgs e)
        {
            var usr = Settings.Default.KnownUsers2.Split('|').ToList();
            var res = usr.Remove(cmbKnownUsers.SelectedItem.ToString());
            Settings.Default.KnownUsers2 = string.Join("|", usr);
            Settings.Default.Save();
            cmbKnown2();
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