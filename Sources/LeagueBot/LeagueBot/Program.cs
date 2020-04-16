using LeagueBot.IO;
using LeagueBot.Patterns;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LeagueBot.Windows.Interop;

namespace LeagueBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.OnStartup();

            PatternsManager.Initialize();

            PatternsManager.Execute("startAram.lua");

            Console.Read();
        }
    }
}
