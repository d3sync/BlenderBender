using System;
using System.Windows.Forms;
using BlenderBender.Properties;

namespace BlenderBender
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //const string appName = "e-Shop Assistant";
            //bool createdNew;
            //var mutex = new Mutex(true, appName, out createdNew);
            //if (!createdNew)
            //{
            //    //app is already running! Exiting the application  
            //    MessageBox.Show("The application is already running.");
            //    return;
            //}
            if (Settings.Default.UpdateSettings)
            {
                Settings.Default.Upgrade();
                Settings.Default.Reload();
                Settings.Default.UpdateSettings = false;
                Settings.Default.Save();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}