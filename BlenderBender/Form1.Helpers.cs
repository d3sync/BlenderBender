using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace BlenderBender
{
    public partial class Form1 : Form
    {
        public string CurrentUser()
        {
            string _user = "";
            if (Properties.Settings.Default.User != "")
            {
                _user = Properties.Settings.Default.User;
            }
            return _user;
        }

        public string DateTimeNUser()
        {
            string _userc = CurrentUser();
            if (_userc == "") { _userc = "Άγνωστος Χειριστής"; }
            return $"{DateTime.Now:dd/MM HH:mm}/({_userc})";
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private void notifier(string message)
        {
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "e-Shop Assistant";
            notifyIcon1.BalloonTipText = "Αντιγράφθηκε στο Πρόχειρο: " + message;
            notifyIcon1.ShowBalloonTip(2000);
        }
    }
}
