using LeagueBot.Constants;
using LeagueBot.Patterns;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueBot
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Read();
            Console.Title = "LeagueBot";
            Bot bot = new Bot();
            bot.Start(AvailableGameType.ARAM);

        }
    }
}
