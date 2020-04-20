using LeagueBot.Game;
using LeagueBot.Img;
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
        [STAThread]
        static void Main(string[] args)
        {
            Logger.OnStartup();

            Configuration.LoadConfig();

            PatternsManager.Initialize();

            PixelCache.Initialize();

            try
            {
                LeagueManager.ApplySettings();
            }
            catch
            {
                Logger.Write("Unable to set league of legends settings. (Probably due to permission restrictions.)", MessageState.WARNING);
            }

            HandleCommand();

            Console.Read();
        }

        static void HandleCommand()
        {
            Logger.Write("Enter a pattern filename, type 'help' for help.");

            string line = Console.ReadLine();

            if (line == "help" || !PatternsManager.Contains(line))
            {
                Logger.Write(PatternsManager.ToString(), MessageState.INFO2);
                HandleCommand();
                return;
            }


            PatternsManager.Execute(line);
        }
    }
}
