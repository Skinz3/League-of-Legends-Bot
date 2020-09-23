using Leaf.xNet;
using LeagueBot.Game;
using LeagueBot.Game.Enums;
using LeagueBot.IO;
using LeagueBot.LCU.Protocol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeagueBot.LCU
{
    public class ClientLCU
    {
        private static string Auth;

        private static int Port;

        private static string Url => "https://127.0.0.1:" + Port + "/";

        private static string CreateURL => Url + "lol-lobby/v2/lobby";
        private static string SearchURL => Url + "lol-lobby/v2/lobby/matchmaking/search";
        private static string ReadyCheckURL => Url + "lol-matchmaking/v1/ready-check/";

        private static string AcceptURL => Url + "lol-matchmaking/v1/ready-check/accept";
        private static string PickURL => Url + "lol-champ-select/v1/session/actions/";
        private static string SessionURL => Url + "lol-champ-select/v1/session";
        private static string SearchStateURL => Url + "lol-lobby/v2/lobby/matchmaking/search-state";

        private static string LoginURL => Url + "rso-auth/v1/session/credentials";

        private static string HonorURL => Url + "lol-honor-v2/v1/honor-player";
        private static string GetHonorDataUrl => Url + "lol-honor-v2/v1/ballot";

        private static string PickableChampionsUrl => Url + "lol-champ-select/v1/pickable-champion-ids";
        private static string RestartUXUrl => Url + "riotclient/kill-and-restart-ux";


        public static void Initialize()
        {
            try
            {
                using (var fileStream = new FileStream(@"C:\Riot Games\League of Legends\lockfile", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var streamReader = new StreamReader(fileStream, Encoding.Default))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            string[] lines = line.Split(':');
                            Port = int.Parse(lines[2]);
                            string riot_pass = lines[3];
                            Auth = Convert.ToBase64String(Encoding.UTF8.GetBytes("riot:" + riot_pass));
                        }
                    }
                }
            }
            catch
            {
                Console.Read();
                Environment.Exit(0);
            }
        }

        public static bool CreateLobby(QueueEnum queueId)
        {
            var request = CreateRequest();

            string response = request.Post(CreateURL, "{\"queueId\": " + (int)queueId + "}", "application/json").StatusCode.ToString();

            if (response == "OK")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static SearchMatchResult SearchMatch()
        {
            var request = CreateRequest();
            string response = request.Post(SearchURL).ToString();

            if (response == string.Empty)
            {
                return SearchMatchResult.Ok;
            }
            else
            {
                SearchMatchResult result = SearchMatchResult.Unknown;

                dynamic obj = JsonConvert.DeserializeObject(response);

                Logger.Write("Search Match Result : " + obj.message, MessageState.WARNING);

                switch (obj.message)
                {
                    case "GATEKEEPER_RESTRICTED":
                        result = SearchMatchResult.GatekeeperRestricted;
                        break;
                    case "QUEUE_NOT_ENABLED":
                        result = SearchMatchResult.QueueNotEnabled;
                        break;
                }
            

                return result;

            }
        }
        public static bool IsMatchFounded()
        {
            var request = CreateRequest();
            var result = request.Get(ReadyCheckURL).ToString();
            return result.Contains("InProgress");
        }
        public static Summoner GetCurrentSummoner()
        {
            var request = CreateRequest();
            var result = request.Get(Url + "lol-summoner/v1/current-summoner").ToString();
            return JsonConvert.DeserializeObject<Summoner>(result);
        }
        public static dynamic GetChampSelectSession()
        {
            var request = CreateRequest();
            var result = request.Get(Url + "lol-champ-select/v1/session").ToString();
            return JsonConvert.DeserializeObject(result);
        }
        public static void AcceptMatch()
        {
            var request = CreateRequest();
            request.Post(AcceptURL);

        }
        public static bool Leaverbuster()
        {
            try
            {
                var request = CreateRequest();
                string response = request.Get(SearchStateURL).ToString();
                return response.Contains("QUEUE_DODGER") || response.Contains("LEAVER_BUSTED");
            }
            catch
            {
                return false;
            }

        }
        public static bool IsInChampSelect()
        {
            try
            {
                var request = CreateRequest();
                return request.Get(SessionURL).ToString().Contains("action");
            }
            catch
            {
                return false;
            }
        }
        public static bool CanPickChampion(ChampionEnum champion)
        {
            return GetPickableChampions().Contains((int)champion);
        }
        public static int[] GetPickableChampions()
        {
            var request = CreateRequest();
            var result = request.Get(PickableChampionsUrl).ToString();
            result = Regex.Match(result, @"\[(.*)\]").Groups[1].Value;
            return result.Split(',').Select(Int32.Parse).ToArray();
        }

        public static ChampionPickResult PickChampion(Summoner currentSummoner, ChampionEnum champion)
        {
            var session = ClientLCU.GetChampSelectSession();
            var cellId = -1;
            var id = -1;

            foreach (var summoner in session.myTeam)
            {
                if (summoner.summonerId == currentSummoner.summonerId)
                {
                    cellId = summoner.cellId;
                }

            }

            foreach (var summoner in session.actions[0])
            {
                if (summoner.championId == (int)champion)
                {
                    return ChampionPickResult.ChampionPicked;
                }

                if (summoner.actorCellId == cellId)
                {
                    id = summoner.id;
                }
            }


            int[] pickableChampions = GetPickableChampions();

            if (!pickableChampions.Contains((int)champion))
            {
                return ChampionPickResult.ChampionNotOwned;
            }

            int championId = (int)champion;

            var request = CreateRequest();

            var result = request.Patch(PickURL + id, "{\"actorCellId\": 0, \"championId\": " + championId + ", \"completed\": true, \"id\": " + id + ", \"isAllyAction\": true, \"type\": \"string\"}", "application/json").ToString();

            return ChampionPickResult.Ok;
        }

        //info: with LCU API you dont have to skip honor. You can wait around 60+ seconds for vote ending and use createLobby()
        //Also if you want implemented login, restarting client is faster than waiting 
        //but if really want honor, have fun with this:

        public static void HonorRandomPlayer()
        {
            var request = CreateRequest();

            request.Get(GetHonorDataUrl).ToString();
            /*sample json
             
            {
            "eligiblePlayers": [
                {
                "championId": 0,
                "skinIndex": 0,
                "skinName": "string",
                "summonerId": 0,
                "summonerName": "string"
                }
             ],
            "gameId": 0
             }
            */
            //

            string honorCategory = "HEART";
            long gameId = 1;
            long summonerId = 1;

            Logger.Write("Player honor");
            request.Post(HonorURL, "{ \"gameId\": " + gameId + ", \"honorCategory\": \"" + honorCategory + "\", \"summonerId\": " + summonerId + "}").ToString();

        }

        public static void RestartClient()
        {
            var request = CreateRequest();
            request.Post(RestartUXUrl);
        }

        private static HttpRequest CreateRequest()
        {
            HttpRequest request = new HttpRequest();
            request.AddHeader("Authorization", "Basic " + Auth);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("charset", "utf8");
            request.IgnoreProtocolErrors = true;
            return request;
        }



    }
}
