using LeagueBot.DesignPattern;
using LeagueBot.Game;
using LeagueBot.Game.Enums;
using LeagueBot.Image;
using LeagueBot.IO;
using LeagueBot.Patterns;
using LeagueBot.Texts;
using LeagueBot.Windows;
using NLua;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static LeagueBot.Windows.Interop;

namespace LeagueBot
{
    /* 
     * The user input settings (game mode, champion , champion behaviour)
     * -> We create SessionParameters.cs that we pass to each scripts.
     * Or
     * 
     * The user input settings (game mode, champion , champion behaviour)
     * -> We create Session.cs that call scripts.
     */
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Logger.OnStartup();

            StartupManager.Initialize(Assembly.GetExecutingAssembly());

            HandleCommand();

            Console.Read();
        }
        static void HandleCommand()
        {
            Logger.Write("Enter a pattern filename, type 'help' for help.", MessageState.INFO);

            string line = Console.ReadLine();

            if (line == "help" || !PatternsManager.Contains(line))
            {
                Logger.Write(PatternsManager.ToString());
                HandleCommand();
                return;
            }


            PatternsManager.Execute(line);

            HandleCommand();
        }
    }
}
