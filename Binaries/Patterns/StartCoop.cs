
using System;
using System.Collections.Generic;
using System.Drawing;
using LeagueBot.Patterns;
using LeagueBot.ApiHelpers;

namespace LeagueBot
{
    public class StartCoop : PatternScript
    {
        private const string MODE = "intro";
        
        private Random RandomTextSender;
        
        private const string SELECTED_CHAMPION = "ashe";

        public override void Execute()
        {
            bot.log("Waiting for league client process...");
            bot.waitProcessOpen(CLIENT_PROCESS_NAME);
            bot.bringProcessToFront(CLIENT_PROCESS_NAME);
            bot.centerProcess(CLIENT_PROCESS_NAME);

            bot.log("Client ready.");
            bot.wait(2000);
            client.createLobby(MODE);

            bot.log("Attempting to search for game...");

            client.startQueue();
            bool restartneeded = false;

            while (client.leaverbuster())
            {
                restartneeded = true;
                bot.log("Leaverbuster detected. Waiting 30 seconds.");
                bot.wait(30000);
            }

            if (restartneeded) { client.startQueue(); }

            while (!client.inChampSelect())
            {
                client.acceptQueue();
                bot.wait(3000);
            }

            bot.log("Match found");

            string[] champs = io.getChamps();

            if(champs.Length > 0)
            {
                foreach (string champ in champs)
                {
                    bot.log("Attempting to pick "+champ);
                    client.pickChampionByName(champ);
                }
            } else
            {
                client.pickChampionByName(SELECTED_CHAMPION);
            }

            bot.executePattern("Coop");
        }
    }
}
