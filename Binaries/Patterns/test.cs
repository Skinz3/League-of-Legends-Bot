
using System;
using System.Collections.Generic;
using System.Drawing;
using LeagueBot.Patterns;
using LeagueBot.ApiHelpers;

namespace LeagueBot
{
    public class test : PatternScript
    {
        public override void Execute()
        {
            bot.initialize();
            
            bot.log("Waiting for league client process... Ensure League client window size is 1600x900");
        
            bot.waitProcessOpen(CLIENT_PROCESS_NAME);
            
            bot.bringProcessToFront(CLIENT_PROCESS_NAME);
            bot.waitUntilProcessBounds(CLIENT_PROCESS_NAME, 1600, 900);
            bot.centerProcess(CLIENT_PROCESS_NAME);
            
            client.loadSummoner();

            client.pickChampion("Annie");

        }
    }
}
