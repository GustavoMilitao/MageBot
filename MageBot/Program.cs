using System;
using System.Windows.Forms;
using Microsoft.Win32;
using MageBot.Core.Engine.ExceptionHandler;
using MageBot.Interface;

namespace MageBot
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            args = new string[] { "ok" };
            args = new string[] { "ok" };
            if (args[0] == "ok")
            {
                try
                {
                    UnhandledExceptionManager.AddHandler();
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    RegistryKey reg;
                    Registry.CurrentUser.DeleteSubKeyTree(@"Software\\MageBot", false);
                    reg = Registry.CurrentUser.CreateSubKey(@"Software\\MageBot");
                    reg = Registry.CurrentUser.OpenSubKey(@"Software\\MageBot", true);
                    if (reg.ValueCount > 1)
                    {
                        reg.DeleteValue("Version");
                        reg.DeleteValue("Minor");
                        System.Threading.Thread.Sleep(1000);
                    }
                    reg.SetValue("Version", 2.0);
                    reg.SetValue("Minor", "0.0");
                    Application.Run(new MainForm("2.0"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Launch MageBot.via updater !");
                Environment.Exit(0);
            }

            /* Changelog :
             * Flood : Modos
             * Déplacements : Fix
             * Trajets : Implantation d'un watchdog
             * Trajets : Fix des variables %PODS%
             * */
        }

    }
}
