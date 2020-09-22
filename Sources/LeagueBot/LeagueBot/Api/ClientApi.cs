using LeagueBot.ApiHelpers;
using LeagueBot.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Leaf.xNet;
using System.Threading;

namespace LeagueBot.Api
{
    public class ClientApi : IApi
    {

        private string auth;
        private int port;
        private static readonly string RegexPattern = "\"--remoting-auth-token=(?'token'.*?)\" | \"--app-port=(?'port'|.*?)\"";
        private static readonly RegexOptions RegexOption = RegexOptions.Multiline;

        HttpRequest request = new HttpRequest();

        private int queueId = 830; //bots intro, forgot intermediate id
        private string URL { get { return "https://127.0.0.1:" + this.port + "/"; } }
        /* You can create Objects for this variables and read/write info */

        private string create = "lol-lobby/v2/lobby";
        private string search = "lol-lobby/v2/lobby/matchmaking/search";
        private string accept = "lol-matchmaking/v1/ready-check/accept";
        private string pick = "lol-champ-select/v1/session/actions/";
        private string session = "lol-champ-select/v1/session";
        private string searchState = "lol-lobby/v2/lobby/matchmaking/search-state";
        private string login = "rso-auth/v1/session/credentials";

        private string honor = "lol-honor-v2/v1/honor-player";
        private string getHonorData = "lol-honor-v2/v1/ballot";

        private string pickableChampion = "GET: lol-champ-select/v1/pickable-champion-ids";
        private string restartUX = "riotclient/kill-and-restart-ux";

        /* */

        public ClientApi()
        {
            getLCUData();
        }

        public void createLobby()
        {
            
            updateRequest();
            string response = request.Post(URL + create, "{\"queueId\": " + this.queueId + "}", "application/json").StatusCode.ToString();
            if(response == "OK") { Logger.Write("Lobby created."); } else { Logger.Write("Cant create lobby, forbidden."); }
        }

        public void searchGame()
        {
            updateRequest();
            string response = request.Post(URL + search).ToString();
            Logger.Write("Searching a game.");
            

        }
        public void acceptGame()
        {
            updateRequest();
            Logger.Write("Trying to accept match.");
            request.Post(URL + accept);

        }
        public bool leaverbuster()
        {
            try
            {
                updateRequest();
                string response = request.Get(URL + searchState).ToString();
                return response.Contains("QUEUE_DODGER") || response.Contains("LEAVER_BUSTED");
            }
            catch
            {
                return false;
            }

        }
        public bool isInChampSelect()
        {
            try
            {
                updateRequest();
                return request.Get(URL + session).ToString().Contains("action");
            }
            catch
            {
                return false;
            }
        }

        public void pickChampion()
        {
            int champions = 10;
            for (int i = 0; i < champions; i++)
            {
                updateRequest();
                int ChampionID = 1;
                /* id 1 = annie, id 2 = olaf etc.*/
                request.Patch(URL + pick + i, "{\"actorCellId\": 0, \"championId\": " + ChampionID + ", \"completed\": true, \"id\": " + i + ", \"isAllyAction\": true, \"type\": \"string\"}", "application/json").ToString();
                Logger.Write("Trying to pick champion: " + ChampionID);
            }
        }

        //info: with LCU API you dont have to skip honor. You can wait around 60+ seconds for vote ending and use createLobby()
        //Also if you want implemented login, restarting client is faster than waiting 
        //but if really want honor, have fun with this:

        public void honorRandomPlayer()
        {
            updateRequest();

            request.Get(URL + getHonorData).ToString();
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
            request.Post(URL + honor, "{ \"gameId\": " + gameId + ", \"honorCategory\": \"" + honorCategory + "\", \"summonerId\": " + summonerId + "}").ToString();

        }

        public void restartClient()
        {
            //sometimes after 2-3 games client got crash
            updateRequest();
            request.Post(URL + restartUX);
        }

        public void updateRequest()
        {
            this.request = new HttpRequest();
            this.request.AddHeader("Authorization", "Basic " + this.auth);
            this.request.AddHeader("Accept", "application/json");
            this.request.AddHeader("content-type", "application/json");
            this.request.IgnoreProtocolErrors = true;
        }

        public void getLCUData()
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
                            this.port = int.Parse(lines[2]);
                            string riot_pass = lines[3];
                            this.auth = Convert.ToBase64String(Encoding.UTF8.GetBytes("riot:" + riot_pass));
                        }
                    }
                }
            }
            catch
            {
                Logger.Write("ERROR: lockfile not found. Is the LoL client started? Are you logged in?");
            }

        }

       

        
                    

        /**********************************************/

        public void clickPlayButton()
        {
            InputHelper.LeftClick(306, 139);
        }
        public void clickAramButton()
        {
            InputHelper.LeftClick(631, 373);
        }
        public void clickCoopvsIAText()
        {
            InputHelper.LeftClick(336, 213);
        }
        public void clickIntroText()
        {
            InputHelper.LeftClick(733, 709);
        }
        public void clickIntermediateText()
        {
            InputHelper.LeftClick(755, 790);
        }
        public void clickConfirmButton()
        {
            InputHelper.LeftClick(832, 949);
        }
        public void clickFindMatchButton()
        {
            InputHelper.LeftClick(832, 949);
        }
        public void skipLevelRewards()
        {
            InputHelper.LeftClick(953, 938);
        }
        public void clickChampSearch()
        {
            InputHelper.LeftClick(1109, 219);
        }
        public bool levelUp()
        {
            return TextHelper.TextExists(872, 237, 300, 300, "level up");
        }
        public bool questCompleted()
        {
            return TextHelper.TextExists(872, 237, 300, 300, "mission");
        }
        public bool mustSelectChamp()
        {
            return TextHelper.TextExists(692, 111, 512, 63, "choose your champion");
        }
        public void lockChampion()
        {
            InputHelper.LeftClick(959, 831);
        }
        public void acceptMatch()
        {
            InputHelper.LeftClick(947, 780);
        }
        public void selectFirstChampion()
        {
            InputHelper.LeftClick(645, 275);
        }
        public void skipHonor()
        {
            InputHelper.LeftClick(962, 903);
        }
        public void closeGameRecap()
        {
            InputHelper.LeftClick(716, 947);
        }
        public void leaveQueue()
        {
            closeGameRecap();
        }
        public void onGameEnd()
        {
            Program.GameCount++;

            Console.Title = Assembly.GetEntryAssembly().GetName().Name + " (" + Program.GameCount + " games)";
        }

    }
}
