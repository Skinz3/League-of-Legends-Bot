using LeagueBot.Game.Enums;
using LeagueBot.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueBot.ApiHelpers
{
    public class LCU
    {

        public const string ApiUrl = "https://127.0.0.1:2999/liveclientdata";

        public const string ActivePlayerUrl = ApiUrl + "/activeplayer";

        public const string PlayerListUrl = ApiUrl + "/playerlist";

        public const string GameStatsUrl = ApiUrl + "/gamestats";

        public static bool IsApiReady()
        {
            var response = Http.Get(ApiUrl + "/playerlist");

            if (response == null)
            {
                return false;
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                response.Dispose();
                return true;
            }

            response.Dispose();
            return false;
        }

        public static bool IsPlayerDead()
        {
            return GetStats().currentHealth == 0;
        }

        public static int GetPlayerLevel()
        {
            var resultStr = Http.GetString(ActivePlayerUrl);
            dynamic dyn = JsonConvert.DeserializeObject(resultStr);
            return dyn.level;
        }
        public static string GetPlayerName()
        {
            var resultStr = Http.GetString(ActivePlayerUrl);
            dynamic dyn = JsonConvert.DeserializeObject(resultStr);
            return dyn.summonerName;
        }

        public static double GetGameTime()
        {
            var resultStr = Http.GetString(GameStatsUrl);
            dynamic dyn = JsonConvert.DeserializeObject(resultStr);
            return dyn.gameTime;
        }
        public static bool IsAllyDead(int id)
        {
            var resultStr = Http.GetString(PlayerListUrl);

            dynamic dyn = JsonConvert.DeserializeObject(resultStr);

            return dyn[id - 1].isDead;
        }
        public static SideEnum GetPlayerSide()
        {
            string playerName = GetPlayerName();

            var resultStr = Http.GetString(PlayerListUrl);

            dynamic dyn = JsonConvert.DeserializeObject(resultStr);

            foreach (var element in dyn)
            {
                if (element.summonerName == playerName)
                {
                    if (element.team == "ORDER")
                    {
                        return SideEnum.Blue;
                    }
                    else
                    {
                        return SideEnum.Red;
                    }
                }
            }

            throw new Exception("Wut");

        }

        public static dynamic GetStats()
        {
            var resultStr = Http.GetString(ActivePlayerUrl);
            dynamic dyn = JsonConvert.DeserializeObject(resultStr);
            return dyn.championStats;
        }
    }
}
