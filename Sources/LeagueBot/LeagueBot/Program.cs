using LeagueBot.DesignPattern;
using LeagueBot.Game;
using LeagueBot.Game.Enums;
using LeagueBot.Image;
using LeagueBot.IO;
using LeagueBot.Patterns;
using LeagueBot.Texts;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

            checkChampList();

            if (args.Length == 0)
                HandleCommand(string.Empty);
            else
                HandleCommand(args[0]);
            
            Console.Read();
        }

        static void HandleCommand(string restart)
        {
            string line;

            if (restart == string.Empty)
            {
                Logger.Write("Enter a pattern filename, type 'help' for help.", MessageState.INFO);
                line = Console.ReadLine();
            } else { line = restart; }
            

            PatternsManager.Execute(line);

            HandleCommand(string.Empty);
        }

        private static void checkChampList()
        {
            string path = Directory.GetCurrentDirectory() + "\\champlist.txt";
            if (!File.Exists(path))
            {
                Logger.WriteColor1("champlist.txt not found. Creating file...\nPlease fill the list that has been generated with champion names.\nNOTE: One champion per line.");
                File.Create(path);
            }
        }
    }
}
