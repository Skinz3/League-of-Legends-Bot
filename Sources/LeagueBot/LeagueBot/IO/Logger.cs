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
        private const ConsoleColor COLOR_1 = ConsoleColor.Cyan;
        private const ConsoleColor COLOR_2 = ConsoleColor.DarkCyan;
        private const ConsoleColor INFORMATION = ConsoleColor.Green;
        private const ConsoleColor SHOP = ConsoleColor.Yellow;
        private const ConsoleColor PLAYER = ConsoleColor.Blue;
        private const ConsoleColor UNSET = ConsoleColor.Gray;


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
        }
        public static void WriteColor1(object value)
        {
            WriteColored(value, COLOR_1);
        }
        public static void WriteColor2(object value)
        {
            WriteColored(value, COLOR_2);
        }
        public static void WritePixel(string message)
        {
            Console.ForegroundColor = INFORMATION;
            Console.WriteLine($"<DETECTION> - {message}");
        }
        public static void WriteInformation(string message, string type)
        {
            switch (type)
            {
                case "PLAYER":
                    Console.ForegroundColor = PLAYER;
                    break;
                case "SHOP":
                    Console.ForegroundColor = SHOP;
                    break;
                default:
                    Console.ForegroundColor = UNSET;
                    break;
            }
            Console.WriteLine($"<{type}> - {message}");
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
            WriteColor2(" __                           _____     _   ");
            WriteColor2("|  |   ___ ___ ___ _ _ ___   | __  |___| |_ ");
            WriteColor2("|  |__| -_| .'| . | | | -_|  | __ -| . |  _|");
            WriteColor2("|_____|___|__,|_  |___|___|  |_____|___|_|  ");
            WriteColor2("              |___|                         ");
            Console.WriteLine();
            Console.Title = Assembly.GetCallingAssembly().GetName().Name;
        }
    }
}
