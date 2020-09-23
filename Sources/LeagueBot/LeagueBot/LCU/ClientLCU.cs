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

        private static string LoginURL => Url + "rso-auth/v1/session/credentials";

        private static string HonorURL => Url + "lol-honor-v2/v1/honor-player";
        private static string GetHonorDataUrl => Url + "lol-honor-v2/v1/ballot";

        private static string PickableChampionsUrl => Url + "lol-champ-select/v1/pickable-champion-ids";
        private static string RestartUXUrl => Url + "riotclient/kill-and-restart-ux";


        public static void Initialize()
        {
            string path = Path.Combine(Configuration.Instance.ClientPath, @"League of Legends\lockfile");

            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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

        public static bool CreateLobby(QueueEnum queueId)
        {
            using (var request = CreateRequest())
            {
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
        }

        public static SearchMatchResult SearchMatch()
        {
            using (var request = CreateRequest())
            {
                string response = request.Post(SearchURL).ToString();

                if (response == string.Empty)
                {
                    return SearchMatchResult.Ok;
                }
                else
                {
                    SearchMatchResult result = SearchMatchResult.Unknown;

                    dynamic obj = JsonConvert.DeserializeObject(response);

                    string message = obj.message;

                    switch (message)
                    {
                        case "GATEKEEPER_RESTRICTED":
                            result = SearchMatchResult.GatekeeperRestricted;
                            break;
                        case "QUEUE_NOT_ENABLED":
                            result = SearchMatchResult.QueueNotEnabled;
                            break;
                        case "INVALID_LOBBY":
                            result = SearchMatchResult.InvalidLobby;
                            break;
                        default:
                            Logger.Write("Unknown search match result : " + obj.message, MessageState.WARNING);
                            break;
                    }


                    return result;

                }
            }
        }
        public static bool IsMatchFounded()
        {
            using (var request = CreateRequest())
            {
                var result = request.Get(ReadyCheckURL).ToString();
                return result.Contains("InProgress");
            }
        }
        public static Summoner GetCurrentSummoner()
        {
            using (var request = CreateRequest())
            {
                var result = request.Get(Url + "lol-summoner/v1/current-summoner").ToString();
                return JsonConvert.DeserializeObject<Summoner>(result);
            }
        }
        public static dynamic GetChampSelectSession()
        {
            using (var request = CreateRequest())
            {
                var result = request.Get(Url + "lol-champ-select/v1/session").ToString();
                return JsonConvert.DeserializeObject(result);
            }
        }
        public static void AcceptMatch()
        {
            using (var request = CreateRequest())
            {
                request.Post(AcceptURL);
            }
        }
        public static bool IsInChampSelect()
        {
            try
            {
                using (var request = CreateRequest())
                {
                    return request.Get(SessionURL).ToString().Contains("action");
                }
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
            using (var request = CreateRequest())
            {
                var result = request.Get(PickableChampionsUrl).ToString();
                result = Regex.Match(result, @"\[(.*)\]").Groups[1].Value;
                return result.Split(',').Select(Int32.Parse).ToArray();
            }
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

            using (var request = CreateRequest())
            {
                var result = request.Patch(PickURL + id, "{\"actorCellId\": 0, \"championId\": " + championId + ", \"completed\": true, \"id\": " + id + ", \"isAllyAction\": true, \"type\": \"string\"}", "application/json").ToString();
                return ChampionPickResult.Ok;
            }
        }

        public static void RestartClient()
        {
            using (var request = CreateRequest())
            {
                request.Post(RestartUXUrl);
            }
        }

        private static HttpRequest CreateRequest()
        {
            HttpRequest request = new HttpRequest();
            request.IgnoreProtocolErrors = true;
            request.CharacterSet = Encoding.UTF8;
            request.AddHeader("Authorization", "Basic " + Auth);
            return request;
        }



    }
}
