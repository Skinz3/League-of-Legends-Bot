
using System;
using System.Collections.Generic;
using System.Drawing;
using LeagueBot.Patterns;

namespace LeagueBot
{
    public class StartCoop : PatternScript
    {
        private const string MODE = "intermediate";

        private const string SELECTED_CHAMPION = "garen";

        public override void Execute()
        {
            bot.log("Waiting for league client process... Ensure League client window size is 1600x900");
            bot.waitProcessOpen(CLIENT_PROCESS_NAME);
            bot.bringProcessToFront(CLIENT_PROCESS_NAME);
            bot.waitUntilProcessBounds(CLIENT_PROCESS_NAME, 1600, 900);
            bot.centerProcess(CLIENT_PROCESS_NAME);

            bot.log("Client ready.");

            client.clickPlayButton();
            bot.wait(2000);
            client.clickCoopvsIAText();
            bot.wait(2000);

            if (MODE == "intermediate")
            {
                client.clickIntermediateText();
            }
            else if (MODE == "intro")
            {
                client.clickIntroText();
            }


            bot.wait(2000);
            client.clickConfirmButton();

            bot.wait(5000);

            client.clickFindMatchButton();

            bot.log("Finding match...");

            while (client.mustSelectChamp() == false)
            {
                client.acceptMatch();
                bot.wait(3000);
            }

            bot.log("Match founded");

            client.clickChampSearch();

            bot.inputWords(SELECTED_CHAMPION);

            client.selectFirstChampion();

            bot.wait(2000);

            client.lockChampion();

            bot.executePattern("Coop");
        }
    }
}
