using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Windows.Forms;
using BlenderBender.Properties;
using BlenderBender.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;

namespace BlenderBender.Class
{
    /// <summary>
    /// Service for user-related operations including registry management, network info, and configuration.
    /// </summary>
    public class UserClass : IUserService
    {
        /// <summary>
        /// Gets the current user name from settings.
        /// </summary>
        /// <returns>Current user name or empty string if not set</returns>
        public string CurrentUser()
        {
            return !string.IsNullOrEmpty(Settings.Default.User) ? Settings.Default.User : string.Empty;
        }

        /// <summary>
        /// Gets formatted date/time with current user information.
        /// </summary>
        /// <returns>Date/time string with user info in parentheses</returns>
        public string DateTimeNUser()
        {
            var user = CurrentUser();
            var userDisplay = string.IsNullOrEmpty(user) ? "Άγνωστος Χειριστής" : user;
            return $"{DateTime.Now:dd/MM HH:mm}/({userDisplay})";
        }

        /// <summary>
        /// Gets formatted date with current user information.
        /// </summary>
        /// <returns>Date string with user info in parentheses</returns>
        public string DateNUser()
        {
            var user = CurrentUser();
            var userDisplay = string.IsNullOrEmpty(user) ? "Άγνωστος Χειριστής" : user;
            return $"{DateTime.Now:dd/MM}/({userDisplay})";
        }

        /// <summary>
        /// Gets a registry key value with type conversion and default fallback.
        /// </summary>
        /// <typeparam name="T">Target type for conversion</typeparam>
        /// <param name="regKey">Registry key name</param>
        /// <returns>Registry value converted to specified type, or default value</returns>
        public T GetRegKey<T>(string regKey)
        {
            if (string.IsNullOrEmpty(regKey))
                throw new ArgumentException("Registry key cannot be null or empty", nameof(regKey));

            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\e-ShopAssistant"))
                {
                    if (key == null)
                        return GetDefaultValue<T>(regKey);

                    var result = key.GetValue(regKey);
                    
                    // Return default if key doesn't exist
                    if (result == null)
                        return GetDefaultValue<T>(regKey);

                    return (T)Convert.ChangeType(result, typeof(T));
                }
            }
            catch (Exception ex)
            {
                // Log error and return default value
                System.Diagnostics.Debug.WriteLine($"Error reading registry key '{regKey}': {ex.Message}");
                return GetDefaultValue<T>(regKey);
            }
        }

        /// <summary>
        /// Gets default values for specific registry keys.
        /// </summary>
        private T GetDefaultValue<T>(string regKey)
        {
            var defaults = new Dictionary<string, object>
            {
                { "Phone", "2115000500" },
                { "kava", "0" },
                { "ESHOP_SHOP", "ΚΑΤΑΣΤΗΜΑ ????" },
                { "ESHOP_ONE", " - Η ΠΑΡΑΓΓΕΛΙΑ ΣΑΣ ΕΙΝΑΙ ΕΤΟΙΜΗ. ΜΠΟΡΕΙΤΕ ΝΑ ΠΕΡΑΣΕΤΕ ΝΑ ΤΗΝ ΠΑΡΑΛΑΒΕΤΕ." },
                { "MAIL_ADDRESS", string.Empty },
                { "REPLACE_ON_MAIL", true }
            };

            if (defaults.ContainsKey(regKey))
                return (T)Convert.ChangeType(defaults[regKey], typeof(T));

            return default(T);
        }

        /// <summary>
        /// Sets a registry key value.
        /// </summary>
        /// <typeparam name="T">Type of the data</typeparam>
        /// <param name="regKey">Registry key name</param>
        /// <param name="data">Data to store</param>
        /// <returns>The data that was stored</returns>
        public T SetRegKey<T>(string regKey, string data)
        {
            if (string.IsNullOrEmpty(regKey))
                throw new ArgumentException("Registry key cannot be null or empty", nameof(regKey));

            try
            {
                using (var key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\e-ShopAssistant"))
                {
                    key.SetValue(regKey, data);
                    key.Flush();
                    return (T)Convert.ChangeType(data, typeof(T));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error setting registry key '{regKey}': {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Gets the local machine's IP address.
        /// </summary>
        /// <returns>IP address string</returns>
        /// <exception cref="Exception">Thrown when no IPv4 network adapter is found</exception>
        public string GetLocalIPAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                        return ip.ToString();
                }
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting local IP address: {ex.Message}");
                return "127.0.0.1"; // Fallback to localhost
            }
        }

        /// <summary>
        /// Checks if the current user has administrator privileges.
        /// </summary>
        /// <returns>True if user is administrator, false otherwise</returns>
        public static bool IsAdministrator()
        {
            try
            {
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking administrator status: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Forces the application to run as administrator.
        /// </summary>
        [Obsolete("Consider using more user-friendly permission handling instead of forcing admin mode")]
        public void checkAdmin()
        {
            if (!IsAdministrator())
            {
                try
                {
                    // Restart program and run as admin
                    var exeName = Process.GetCurrentProcess().MainModule.FileName;
                    var startInfo = new ProcessStartInfo(exeName)
                    {
                        Verb = "runas"
                    };
                    Process.Start(startInfo);
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error starting as administrator: {ex.Message}");
                    MessageBox.Show("Administrator privileges required but could not be obtained.", 
                        "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Gets a configuration value from appsettings.json.
        /// </summary>
        /// <param name="query">Configuration key path</param>
        /// <returns>Configuration value as string</returns>
        [Obsolete("Consider using dependency injection for IConfiguration instead of static access")]
        public string GetDefault(string query)
        {
            try
            {
                //Reading from appsettings file
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();
                return config[query];
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading configuration '{query}': {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets a configuration section from appsettings.json.
        /// </summary>
        /// <param name="query">Section path</param>
        /// <returns>Configuration section</returns>
        [Obsolete("Consider using dependency injection for IConfiguration instead of static access")]
        public IConfigurationSection GetSection(string query)
        {
            try
            {
                //Reading from appsettings file
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();
                return config.GetRequiredSection(query);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading configuration section '{query}': {ex.Message}");
                return null;
            }
        }
    }
}