
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
            bot.initialize(); // scripts are isolated.

            bot.log("Match ended.");

            client.onGameEnd();

           
            bot.wait(5 * 1000);

            bot.waitProcessOpen(ClientProcessName);

            bot.bringProcessToFront(ClientProcessName);
            bot.centerProcess(ClientProcessName);

            bot.log("Closing client...");

            client.closeClient();
           
            bot.wait(5 * 1000);

            bot.log("Opening client..");

            client.openClient();

            bot.wait(25 * 1000);

            bot.executePattern("StartCoop");
        }
    }
}
