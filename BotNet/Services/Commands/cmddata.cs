using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNet.Services.Commands
{
    public class cmddata
    {
        public static string cmd { get; set; }
        public static int cid { get; set; }
        public static string[] args { get; set; } = new string[4];
    }
}
