
using System;
using System.Collections.Generic;
using System.Drawing;
using LeagueBot.Patterns;

namespace LeagueBot
{
    public class EndCoop : PatternScript
    {

        public override void Execute()
        {
            bot.wait(10000);
            bot.log("Match ended.");

            bot.waitProcessOpen(CLIENT_PROCESS_NAME);

            bot.bringProcessToFront(CLIENT_PROCESS_NAME);
            bot.centerProcess(CLIENT_PROCESS_NAME);

            bot.wait(4000);

            bot.executePattern("Restart");
        }
    }
}
