
using System;
using System.Collections.Generic;
using System.Drawing;
using LeagueBot.Patterns;
using LeagueBot.ApiHelpers;

namespace LeagueBot
{
    public class StartDev : PatternScript
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
			//bot.KillProcess(CLIENT_PROCESS_NAME);
			//bot.wait(13000);
			
			bot.waitProcessOpen(CLIENT_PROCESS_NAME);
            bot.bringProcessToFront(CLIENT_PROCESS_NAME);
            bot.centerProcess(CLIENT_PROCESS_NAME);
			bot.log("Client ready.");
			/*
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
*/
            string[] champs = io.getChamps();

            if(champs.Length > 0)
            {
                foreach (string champ in champs)
                {
                    bot.log("Attempting to pick "+champ);
                    client.pickChampionByName(champ);
					bot.wait(2000);
					if(client.IsPicked())
					{
						bot.log(champ+" is picked!");
						bot.executePattern("Coop");
						break;
					}

                }
            } else
            {
                client.pickChampionByName(SELECTED_CHAMPION);
            }

            bot.executePattern("Coop");
        }
    }
}
