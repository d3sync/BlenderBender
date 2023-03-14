using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Windows.Forms;
using BlenderBender.Properties;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;

namespace BlenderBender.Class
{
    public class UserClass
    {
        public string CurrentUser()
        {
            var _user = "";
            if (Settings.Default.User != "") _user = Settings.Default.User;
            return _user;
        }

        public string DateTimeNUser()
        {
            var _userc = CurrentUser();
            if (_userc == "") _userc = "Άγνωστος Χειριστής";
            return $"{DateTime.Now.ToString("dd/MM HH:mm")}/({_userc})";
        }

        public string DateNUser()
        {
            var _userc = CurrentUser();
            if (_userc == "") _userc = "Άγνωστος Χειριστής";
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
            if (key != null && !string.IsNullOrEmpty(regKey))
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
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public void checkAdmin()
        {
            if (IsAdministrator() == false)
            {
                // Restart program and run as admin
                var exeName = Process.GetCurrentProcess().MainModule.FileName;
                var startInfo = new ProcessStartInfo(exeName);
                startInfo.Verb = "runas";
                Process.Start(startInfo);
                Application.Exit();
            }
        }

        public string GetDefault(string query)
        {
            //Reading from appsettings file
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();
            //return config.GetValue<string>("Logging:FilePath");
            return config[query];
        }

        public IConfigurationSection GetSection(string query)
        {
            //Reading from appsettings file
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();
            //return config.GetValue<string>("Logging:FilePath");
            return config.GetRequiredSection(query);
        }
    }
}