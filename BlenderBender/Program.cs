﻿using System;
using System.Threading;
using System.Windows.Forms;

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
            if (Properties.Settings.Default.UpdateSettings)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpdateSettings = false;
                Properties.Settings.Default.Save();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}