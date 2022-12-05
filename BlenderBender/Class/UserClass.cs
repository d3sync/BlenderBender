using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;

namespace BlenderBender.Class
{
    public class UserClass
    {
        public UserClass()
        {
                
        }
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
            return $"{DateTime.Now.ToString("dd/MM HH:mm")}/({_userc})";
        }
        public string DateNUser()
        {
            string _userc = CurrentUser();
            if (_userc == "") { _userc = "Άγνωστος Χειριστής"; }
            return $"{DateTime.Now.ToString("dd/MM")}/({_userc})";
        }
        public T GetRegKey<T>(string regKey)
        {
            var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\e-ShopAssistant");
            var result = key.GetValue(regKey);
            //Phone
            //kava
            //ESHOP_SHOP
            //ESHOP_ONE
            //MAIL_ADDRESS
            //REPLACE_ON_MAIL - true or false
            if (key != null && !String.IsNullOrEmpty(regKey))
            {
                switch (regKey)
                {
                    case "Phone":
                        if (key.GetValue(regKey) == null) result = "2115000500";
                        break;
                    case "kava":
                        if (key.GetValue(regKey) == null) result = "0";
                        break;
                    case "ESHOP_SHOP":
                        if (key.GetValue(regKey) == null) result = "ΚΑΤΑΣΤΗΜΑ ????";
                        break;
                    case "ESHOP_ONE":
                        if (key.GetValue(regKey) == null)
                            result = " - Η ΠΑΡΑΓΓΕΛΙΑ ΣΑΣ ΕΙΝΑΙ ΕΤΟΙΜΗ. ΜΠΟΡΕΙΤΕ ΝΑ ΠΕΡΑΣΕΤΕ ΝΑ ΤΗΝ ΠΑΡΑΛΑΒΕΤΕ.";
                        break;
                    case "MAIL_ADDRESS":
                        if (key.GetValue(regKey) == null) result = string.Empty;
                        break;
                    case "REPLACE_ON_MAIL":
                        if (key.GetValue(regKey) == null) result = true;
                        break;
                    default:
                        break;
                }
            }
            key.Close();
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public T SetRegKey<T>(string regKey, string data)
        {
            var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\e-ShopAssistant");
            key.SetValue(regKey, data);
            key.Flush();
            key.Close();
            return (T)Convert.ChangeType(data, typeof(T));
        }
        public string GetLocalIPAddress()
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
        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        public void checkAdmin()
        {
            if (IsAdministrator() == false)
            {
                // Restart program and run as admin
                var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                startInfo.Verb = "runas";
                System.Diagnostics.Process.Start(startInfo);
                Application.Exit();
                return;
            }
        }

        public string GetDefault(string query)
        {
            //Reading from appsettings file
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            //return config.GetValue<string>("Logging:FilePath");
            return config[query];
        }
        public IConfigurationSection GetSection(string query)
        {
            //Reading from appsettings file
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            //return config.GetValue<string>("Logging:FilePath");
            return config.GetRequiredSection(query);
        }
    }
}
