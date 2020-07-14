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

            checkAccountsList();

            if (args.Length == 0)
                HandleCommand(string.Empty, -1);
            else if (args.Length == 1)
                HandleCommand(args[0], -1);
            else if (args.Length == 2)
            {   
                int gamesInt = int.Parse(args[1]);

                if(gamesInt>=0){
                    Logger.Write("Bot has " + args[1] + " game(s) left to play.", MessageState.INFO);
                }else{
                    Logger.Write("Bot has played " + (Math.Abs(gamesInt)-1).ToString() + " game(s)", MessageState.INFO);
                }                
                
                HandleCommand(args[0], gamesInt);
            }
            Console.Read();
        }

        static void HandleCommand(string restart, int games)
        {
            string line;
            Globals.numberOfGames = games;

            if (restart == string.Empty)
            {
                Logger.Write("Enter a pattern filename, type 'help' for help.", MessageState.INFO);
                line = Console.ReadLine();
            }
            else { line = restart; }

            PatternsManager.Execute(line);

            HandleCommand(string.Empty, games);
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

        private static void checkAccountsList()
        {
            string path = Directory.GetCurrentDirectory() + "\\accounts.txt";
            if (!File.Exists(path))
            {
                Logger.WriteColor2("accounts.txt not found. Creating file...\nPlease fill the list that has been generated with accounts data.\nNOTE: One login:password per line.");
                File.Create(path);
            }
        }

    }

    public class Globals
    {
        public static int numberOfGames; // Modifiable
    }
}
