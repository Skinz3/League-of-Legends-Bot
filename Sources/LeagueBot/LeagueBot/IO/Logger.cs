using System;
using System.Collections.Generic;
using System.Reflection;

namespace LeagueBot.IO
{
    public class Logger
    {
        private const ConsoleColor COLOR_1 = ConsoleColor.Magenta;
        private const ConsoleColor COLOR_2 = ConsoleColor.DarkMagenta;

        private static readonly Dictionary<LogLevel, ConsoleColor> Colors = new Dictionary<LogLevel, ConsoleColor>()
        {
            { LogLevel.INFO,            ConsoleColor.Gray },
            { LogLevel.INFO2,           ConsoleColor.DarkGray },
            { LogLevel.IMPORTANT_INFO,  ConsoleColor.White },
            { LogLevel.SUCCES,          ConsoleColor.Green },
            { LogLevel.WARNING,         ConsoleColor.Yellow },
            { LogLevel.ERROR ,          ConsoleColor.DarkRed},
            { LogLevel.ERROR_FATAL,     ConsoleColor.Red }
        };

        public static void NewLine() => Console.WriteLine(Environment.NewLine);

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
            Console.Title = Assembly.GetEntryAssembly().GetName().Name + " (" + Program.GameCount + " games)";
        }

        public static void Write(object value, LogLevel state = LogLevel.INFO)
        {
            WriteColored(value, Colors[state]);

            if (state == LogLevel.ERROR_FATAL)
            {
                Console.ReadLine();
                Environment.Exit(1);
            }
        }

        public static void WriteColor1(object value) => WriteColored(value, COLOR_1);

        public static void WriteColor2(object value) => WriteColored(value, COLOR_2);

        private static void WriteColored(object value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
        }
    }
}