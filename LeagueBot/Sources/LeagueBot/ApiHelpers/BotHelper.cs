using LeagueBot.IO;
using LeagueBot.Patterns;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static LeagueBot.Windows.Interop;

namespace LeagueBot.ApiHelpers
{
    class BotHelper
    {
        /*
         * In milliseconds
         */
        private const int IDLE_DELAY = 250;

        public static void Wait(int ms)
        {
            Thread.Sleep(ms);
        }
        public static void InputIdle()
        {
            Thread.Sleep(IDLE_DELAY);
        }
    }
}
