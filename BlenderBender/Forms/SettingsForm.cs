using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlenderBender.Class;
using System.Xml.Linq;

namespace BlenderBender.Forms
{
    public partial class SettingsForm : Form
    {
        public UserClass user;
        public SettingsForm()
        {
            InitializeComponent();
            cmbKnown();
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
            checkBox2.Checked = Properties.Settings.Default.windowsWeirdness;
            richTextBox3.Text = Properties.Settings.Default.Signature;
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
            Properties.Settings.Default._storeAddress = _storeAddress.Text;
            Properties.Settings.Default.Signature = richTextBox3.Text;
            Properties.Settings.Default.windowsWeirdness = checkBox2.Checked;
            //Properties.Settings.Default._storeArea = _txtFrom.Text;
            Properties.Settings.Default.Save();
            //_txtFrom.Text = _storeAddress.Text;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            List<string> users = Properties.Settings.Default.KnownUsers;
            if (txtName.Text != null)
            {
                users.Add(txtName.Text.Trim());
                Properties.Settings.Default.KnownUsers = users;
                Properties.Settings.Default.Save();
            }

            cmbKnown();

        }

        private void cmbKnown()
        {
            cmbKnownUsers.Items.Clear();
            foreach (var item in Properties.Settings.Default.KnownUsers)
            {
                if(!String.IsNullOrEmpty(item))
                    cmbKnownUsers.Items.Add(item);
            }
        }

        private void btnDelUser_Click(object sender, EventArgs e)
        {
            var res = Properties.Settings.Default.KnownUsers.Remove(cmbKnownUsers.SelectedItem.ToString());
            Properties.Settings.Default.Save();
            cmbKnown();
            if (res)
                 MessageBox.Show($"Διαγραφή χρήστη {cmbKnownUsers.SelectedItem.ToString()}");
        }
    }
}
