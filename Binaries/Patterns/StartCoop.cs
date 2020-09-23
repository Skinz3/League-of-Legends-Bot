
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
        private static ChampionEnum[] Champions = new ChampionEnum[]
        {
            ChampionEnum.Veigar,
            ChampionEnum.Annie,
        };

        private static QueueEnum QueueType = QueueEnum.BotIntermediate;

        public override void Execute()
        {
            bot.log("Waiting for league client process...");

            bot.waitProcessOpen(CLIENT_PROCESS_NAME);
            
            bot.initialize();

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
                switch (result)
                {
                    case SearchMatchResult.GatekeeperRestricted:
                       bot.warn("Cannot search match. Queue dodge timer. Retrying in 10 seconds.");
                       break;
                    case SearchMatchResult.QueueNotEnabled:
                       client.createLobby(QueueType);
                       bot.warn("Cannot search match. Creating lobby...");
                       break;
                    case SearchMatchResult.InvalidLobby:
                       bot.warn("Cannot search match. Client not ready. Retrying in 10 seconds.");
                       break;
                }

                bot.wait(1000 * 10);

                result = client.searchMatch();
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
                if (championIndex > Champions.Length - 1)
                {
                    bot.warn("Unable to continue. No more champions to pick");
                    return;
                }
                ChampionPickResult pickResult = client.pickChampion(Champions[championIndex]);

                switch (pickResult)
                {
                    case ChampionPickResult.Ok:
                        bot.log(Champions[championIndex] + "picked successfully.");
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
