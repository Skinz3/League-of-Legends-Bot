using Leaf.xNet;
using LeagueBot.Game;
using LeagueBot.Game.Enums;
using LeagueBot.IO;
using LeagueBot.LCU.Protocol;
using LeagueBot.Patterns;
using LeagueBot.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueBot.LCU
{
    public class ClientLCU
    {
        private static string Auth;

        private static int Port;

        private static string Url => "https://127.0.0.1:" + Port + "/";

        private static string LoginUrl => Url + "lol-login/v1/session";


        private static string CreateLobbyURL => Url + "lol-lobby/v2/lobby";
        private static string SearchURL => Url + "lol-lobby/v2/lobby/matchmaking/search";
        private static string ReadyCheckURL => Url + "lol-matchmaking/v1/ready-check/";

        private static string AcceptURL => Url + "lol-matchmaking/v1/ready-check/accept";
        private static string PickURL => Url + "lol-champ-select/v1/session/actions/";
        private static string SessionURL => Url + "lol-champ-select/v1/session";

        private static string LoginURL => Url + "rso-auth/v1/session/credentials";

        private static string HonorURL => Url + "lol-honor-v2/v1/honor-player";
        private static string GetHonorDataUrl => Url + "lol-honor-v2/v1/ballot";

        private static string PickableChampionsUrl => Url + "lol-champ-select/v1/pickable-champion-ids";
        private static string KillUXUrl => Url + "riotclient/kill-ux";


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
                string response = request.Post(CreateLobbyURL, "{\"queueId\": " + (int)queueId + "}", "application/json").StatusCode.ToString();

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
        public static GameflowPhaseEnum GetGameflowPhase()
        {
            using (var request = CreateRequest())
            {
                var result = request.Get(Url + "lol-gameflow/v1/gameflow-phase").ToString();
                result = Regex.Match(result, "\"(.*)\"").Groups[1].Value;
                return (GameflowPhaseEnum)Enum.Parse(typeof(GameflowPhaseEnum), result);
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

            foreach (var summoner in session.actions[0])
            {
                if (summoner.championId == (int)champion)
                {
                    return ChampionPickResult.ChampionPicked;
                }
            }


            int[] pickableChampions = GetPickableChampions();

            if (!pickableChampions.Contains((int)champion))
            {
                return ChampionPickResult.ChampionNotOwned;
            }

            int championId = (int)champion;

            for (int id = 0; id < 10; id++)
            {
                using (var request = CreateRequest())
                {
                    var result = request.Patch(PickURL + id, "{\"actorCellId\": 0, \"championId\": " + championId + ", \"completed\": true, \"id\": " + id + ", \"isAllyAction\": true, \"type\": \"string\"}", "application/json").ToString();
                }
            }

            return ChampionPickResult.Ok;

        }

        public static void CloseClient()
        {
            using (var request = CreateRequest())
            {
                request.Post(KillUXUrl);
            }

            foreach (var process in Process.GetProcessesByName(PatternScript.ClientHostProcessName))
            {
                process.Kill();
            }
        }

        public static void OpenClient()
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = Path.Combine(Configuration.Instance.ClientPath, @"League of Legends\LeagueClient.exe");
            Process.Start(psi);
        }


        private static HttpRequest CreateRequest()
        {
            HttpRequest request = new HttpRequest();
            request.IgnoreProtocolErrors = true;
            request.ConnectTimeout = 5000;
            request.ReadWriteTimeout = 5000;
            request.CharacterSet = Encoding.UTF8;
            request.AddHeader("Authorization", "Basic " + Auth);
            return request;
        }



    }
}
