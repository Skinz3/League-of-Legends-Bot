using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.IO
{
    public enum MessageState
    {
        INFO = 0,
        INFO2 = 1,
        IMPORTANT_INFO = 2,
        WARNING = 3,
        ERROR = 4,
        ERROR_FATAL = 5,
        SUCCES = 6,
    }
    public class Logger
    {
        private const ConsoleColor COLOR_1 = ConsoleColor.Magenta;
        private const ConsoleColor COLOR_2 = ConsoleColor.DarkMagenta;

        private static Dictionary<MessageState, ConsoleColor> Colors = new Dictionary<MessageState, ConsoleColor>()
        {
            { MessageState.INFO,            ConsoleColor.Gray },
            { MessageState.INFO2,           ConsoleColor.DarkGray },
            { MessageState.IMPORTANT_INFO,  ConsoleColor.White },
            { MessageState.SUCCES,          ConsoleColor.Green },
            { MessageState.WARNING,         ConsoleColor.Yellow },
            { MessageState.ERROR ,          ConsoleColor.DarkRed},
            { MessageState.ERROR_FATAL,     ConsoleColor.Red }
        };

        public static void Write(object value, MessageState state = MessageState.INFO)
        {
            WriteColored(value, Colors[state]);

            if (state == MessageState.ERROR_FATAL)
            {
                Console.ReadLine();
                Environment.Exit(1);
            }
        }
        public static void WriteColor1(object value)
        {
            WriteColored(value, COLOR_1);
        }
        public static void WriteColor2(object value)
        {
            WriteColored(value, COLOR_2);
        }
        private static void WriteColored(object value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
        }
        public static void NewLine()
        {
            Console.WriteLine(Environment.NewLine);
        }
        public static void OnStartup()
        {

            WriteColor2(" .                            .--.      .  ");
            WriteColor1(" |                            |   )    _|_ ");
            WriteColor2(" |    .-. .-.  .-...  . .-.   |--:  .-. |  ");
            WriteColor1(" |   (.-'(   )(   ||  |(.-'   |   )(   )|  ");
            WriteColor2(" '---'`--'`-'`-`-`|`--`-`--'  '--'  `-' `-'");
            WriteColor2("               ._.' ");
            WriteColor2("> https://github.com/Skinz3");
            Console.WriteLine();
            Console.Title = Assembly.GetEntryAssembly().GetName().Name + " ("+Program.GameCount + " games)";
        }
    }
}
