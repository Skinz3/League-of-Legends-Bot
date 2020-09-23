
using System;
using System.Collections.Generic;
using System.Drawing;
using LeagueBot.Patterns;
using LeagueBot.ApiHelpers;
using LeagueBot.Game.Enums;

namespace LeagueBot
{
    public class StartCoop : PatternScript
    {
        private static string[] Champions = new string[]
        {
            "Veigar",
            "Annie"
        };

        private static QueueEnum QueueType = QueueEnum.BotIntermediate;

        public override void Execute()
        {
            bot.initialize();
         
            bot.log("Waiting for league client process...");
        
            bot.waitProcessOpen(CLIENT_PROCESS_NAME);
            
            bot.bringProcessToFront(CLIENT_PROCESS_NAME);
            bot.centerProcess(CLIENT_PROCESS_NAME);

            while (!client.loadSummoner())
            {
                bot.warn("Unable to load summoner. Retrying in 10 seconds.");

                bot.wait(1000);
            }

            bot.log("Summoner loaded "+ client.summoner.displayName);

            client.createLobby(QueueType);

            bot.log("Searching match...");
            
            SearchMatchResult result = client.searchMatch();

            while (result != SearchMatchResult.Ok)
            {
                result = client.searchMatch();

                switch (result)
                {
                    case SearchMatchResult.GatekeeperRestricted:
                       bot.warn("Cannot search match. Queue dodge timer. Retrying in 10 seconds.");
                       break;
                    case SearchMatchResult.QueueNotEnabled:
                       bot.warn("Cannot search match. Recreating Queue. Retrying in 10 seconds.");
                       break;
                           
              
                }
             
                bot.wait(1000 * 10);
            }

            

            bool isMatchFound = false;

            while (!isMatchFound)
            {
                isMatchFound = client.isMatchFound();
                bot.wait(1000);

            }

            while (!client.isInChampSelect())
            {
                client.acceptMatch();
                bot.wait(1000);
            }


            bot.log("Match founded.");

            bot.wait(5000);

            bool picked = false;

            int championIndex = 0;

            while (!picked)
            {
                ChampionPickResult pickResult = client.pickChampion(Champions[championIndex]);

                switch (pickResult)
                {
                    case ChampionPickResult.Ok:
                        bot.log("We picked champion successfully.");
                        picked = true;
                        break;
                    case ChampionPickResult.ChampionNotOwned:
                        bot.log("Error the request champion is not owned.");
                        break;
                    case ChampionPickResult.ChampionPicked:
                        bot.log("Someone already pick your champ!");
                        break;
                    case ChampionPickResult.InvalidChampion:
                        bot.log("Unable to pick champion, invalid champ !");
                        break;
                    default:
                        break;
                }

                championIndex++;

                bot.wait(1000);

            }

            bot.executePattern("Coop");
            
        }
    }
}
