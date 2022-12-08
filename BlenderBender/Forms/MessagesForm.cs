﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlenderBender.Class;

namespace BlenderBender.Forms
{
    public partial class MessagesForm : Form
    {
        public MainWindow mf;
        DateClass dtto = new DateClass();
        UserClass user = new UserClass();
        public MessagesForm(MainWindow mf)
        {
            InitializeComponent();
            this.mf = mf;
            Known();
            currentUser.Text = Properties.Settings.Default.User ?? "Αγνωστός Χειριστής";
            cmbExtraDays.SelectedIndex = 0;
        }

        private void Known()
        {
            currentUser.Items.Clear();
            foreach (var item in Properties.Settings.Default.KnownUsers)
            {
                if (!String.IsNullOrEmpty(item))
                    currentUser.Items.Add(item);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"**Αποτυχία 1ου SMS - Ενημερώθηκε μέσω τηλεφώνου {user.DateTimeNUser()}");
            mf.notifier("Αποτυχία 1ου [Κλήση]");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int extra = 0;
            extra += Int32.Parse(cmbExtraDays.SelectedItem.ToString());

            string doh = dtto.DateTo("excludeSunday", extra);

            Clipboard.SetText(
                $"**2η Ενημέρωση μέσω τηλεφώνου {user.DateTimeNUser()} ότι θα παραμείνει μέχρι και {doh}");
            mf.notifier("Τηλεφωνική Υπενθύμιση");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            {
                int extra = 0;
                extra += Int32.Parse(cmbExtraDays.SelectedItem.ToString());
                label32.Text = dtto.DateTo("excludeSunday", extra);
                Clipboard.SetText(
                    $"{user.GetRegKey<string>("ESHOP_SHOP")} - ΣΑΣ ΕΝΗΜΕΡΩΝΟΥΜΕ ΟΤΙ Η ΠΑΡΑΓΓΕΛΙΑ ΣΑΣ ΘΑ ΠΑΡΑΜΕΙΝΕΙ ΣΤΟ ΚΑΤΑΣΤΗΜΑ ΜΑΣ ΕΩΣ {label32.Text.ToUpper()}. ΕΥΧΑΡΙΣΤΟΥΜΕ");
                mf.notifier("2ο ΕΠΙΤΟΠΟΥ");
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("Η ΠΑΡΑΓΓΕΛΙΑ ΣΑΣ ΒΡΙΣΚΕΤΑΙ ΣΕ ΑΝΑΜΟΝΗ ΔΙΕΥΚΡΙΝΙΣΕΩΝ. ΠΑΡΑΚΑΛΩ ΕΠΙΚΟΙΝΩΝΗΣΤΕ ΜΑΖΙ ΜΑΣ ΣΤΟ 2115000500 . ΕΥΧΑΡΙΣΤΟΥΜΕ");
            mf.notifier("Αναμονή διευκρινίσεων");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            Clipboard.SetText(
                $"**Ζήτησε να παραμείνει στο κατάστημα μέχρι και {dateTimePicker3.Value.ToString("dddd dd/MM")}({user.CurrentUser()})**");
            if (mf.hold != 1)
                if (dateTimePicker3.Value != DateTime.Now)
                    mf.countdown = 100;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"**ΔΑ {user.DateNUser()}");
            mf.notifier("Δεν απαντούσε");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"**Δεν απαντούσε {user.DateTimeNUser()}");
            mf.notifier("Δεν απαντούσε");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(DateTime.Now.ToString("dd/MM HH:mm"));
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"**Αδυναμία Επικοινωνίας {user.DateTimeNUser()}");
            mf.notifier("Αδυναμία Επικοινωνίας");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (currentUser.Text != "")
            {
                Clipboard.SetText("{Τιμολογήθηκε από " + currentUser.Text + "}");
                mf.notifier("Τιμολ. Από..");
            }
            else
            {
                MessageBox.Show("Όνομα κενό!");
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("ΑΡΘΡΟ 39Α, ΥΠΟΧΡΕΟΣ ΓΙΑ ΤΗΝ ΚΑΤΑΒΟΛΗ ΤΟΥ ΦΟΡΟΥ ΕΙΝΑΙ Ο ΑΓΟΡΑΣΤΗΣ");
            mf.notifier("Αρθρο 39Α");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"**Ζήτησε να παραλάβει απ το κατάστημα. {user.DateTimeNUser()}");
            mf.notifier("Παραλαβή Επιτόπου");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"~~ΔΡΟΜΟΛΟΓΙΟ: Ζήτησε στο επόμενο~~");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            int extra = 0;
            extra += Int32.Parse(cmbExtraDays.SelectedItem.ToString());
            label32.Text = dtto.DateTo("excludeSunday", extra);
            Clipboard.SetText(
                $"{user.GetRegKey<string>("ESHOP_SHOP")} - ΣΑΣ ΥΠΕΝΘΥΜΙΖΟΥΜΕ ΟΤΙ Η ΠΑΡΑΓΓΕΛΙΑ ΣΑΣ ΕΙΝΑΙ ΕΤΟΙΜΗ ΚΑΙ ΠΡΕΠΕΙ ΝΑ ΠΑΡΑΔΟΘΕΙ ΜΕΧΡΙ {label32.Text.ToUpper()}.");
            mf.notifier("2ο ESHOP");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Clipboard.SetText(
                $"**Επιθυμεί παράδοση {dateTimePicker1.Value.ToString("dddd dd/MM")}({user.CurrentUser()})**");
            if (mf.hold != 1)
                if (dateTimePicker1.Value != DateTime.Now)
                    mf.countdown = 100;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clipboard.SetText($"~~ΔΡΟΜΟΛΟΓΙΟ: {comboBox1.SelectedItem.ToString()}~~");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(user.GetRegKey<string>("ESHOP_ONE").ToUpper() + " " + user.GetRegKey<string>("Phone"));
            mf.notifier("1o SMS");
        }

        private void currentUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.User = currentUser.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        private void MessagesForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }
    }
}
