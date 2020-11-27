using System.Threading;

namespace LeagueBot.ApiHelpers
{
    internal class BotHelper
    {

        // TODO: Should have functions for emulating keypresses and so on with using delay.

        /*
         * In milliseconds
         */
        private const int IDLE_DELAY = 250;

        public static void InputIdle() => Thread.Sleep(IDLE_DELAY);

        public static void Sleep(int ms) => Thread.Sleep(ms);
    }
}