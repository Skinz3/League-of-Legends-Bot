using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Leaf.xNet;
using LeagueBot.Game.Entities;
using LeagueBot.IO;
using LeagueBot.ApiHelpers;
using System.Windows.Forms;

namespace LeagueBot.Api
{
    public class LCU
    {
        public int port;
        public string auth;
        HttpRequest request = new HttpRequest();


        public LCU()
        {
            this.readLockFile();
        }

        public void startQueue()
        {
            updateRequest();
            String url = "https://127.0.0.1:" + this.port + "/lol-lobby/v2/lobby/matchmaking/search";
            request.AddHeader("Authorization", "Basic " + this.auth);
            String response = request.Post(url).ToString();
        }

        public bool leaverbuster()
        {
            try
            {
                updateRequest();
                String url = "https://127.0.0.1:" + this.port + "/lol-lobby/v2/lobby/matchmaking/search-state";
                request.AddHeader("Authorization", "Basic " + this.auth);
                string response = request.Get(url).ToString();
                return response.Contains("QUEUE_DODGER") || response.Contains("LEAVER_BUSTED");
            }
            catch
            {
                return false;
            }
            
        }

        public bool inChampSelect()
        {
            try
            {
                string stringUrl = "https://127.0.0.1:" + this.port + "/lol-champ-select/v1/session";
                updateRequest();
                return request.Get(stringUrl).ToString().Contains("action");
            }
            catch
            {
                return false;
            }
        }

        public void createLobby(string type)
        {
            string id = (type == "intro") ? "830" : "850";
            updateRequest();
            string url = "https://127.0.0.1:" + this.port + "/lol-lobby/v2/lobby";
            string content = request.Post(url, "{\"queueId\": " + id + "}", "application/json").StatusCode.ToString();
            Console.WriteLine(content);
        }


        public void pickChampion(int ChampionID)
        {
            System.Threading.Thread.Sleep(2500);
            for (int i = 0; i < 10; i++)
            {
                string url = "https://127.0.0.1:" + this.port + "/lol-champ-select/v1/session/actions/" + i;
                updateRequest();
                string statusCode = request.Patch(url, "{\"actorCellId\": 0, \"championId\": " + ChampionID + ", \"completed\": true, \"id\": " + i + ", \"isAllyAction\": true, \"type\": \"string\"}", "application/json").ToString();
            }
        }

        public void pickChampionByName(string name)
        {
            Champions ch = new Champions();
            this.pickChampion(ch.getIdByChamp(name));
        }

        public void acceptQueue()
        {
            string url = "https://127.0.0.1:" + this.port + "/lol-matchmaking/v1/ready-check/accept";
            updateRequest();
            HttpResponse result = request.Post(url);
        }

        public void SingOut()
        {
            InputHelper.LeftClick(1736, 110, 150);
            InputHelper.LeftClick(1000, 587, 150);
            Logger.Write("Logged out");
        }

        public bool isLogged()
        {
            //Console.WriteLine(TextHelper.TextExists(447, 111, 505, 135, "HOME"));
            return TextHelper.TextExists(447, 111, 505, 135, "HOME");
        }

        public bool WrongCredentials()
        {

            //Console.Write(TextHelper.TextExists(586, 438, 100, 40, "match"));
            return TextHelper.TextExists(586, 438, 100, 40, "match");
        }


        public void GetLoginData()
        {
            Accounts acc = new Accounts();

            InputHelper.LeftClick(388, 464, 150);
            BotHelper.Wait(2000);
            SendKeys.SendWait(acc.login);
            BotHelper.Wait(2000);
            InputHelper.LeftClick(388, 527, 150);
            BotHelper.Wait(2000);
            SendKeys.SendWait(acc.password);
            BotHelper.Wait(2000);
            InputHelper.LeftClick(500, 680, 150);
        }

        public void readLockFile()
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
        #region misc

        private void updateRequest()
        {
            this.request = new HttpRequest();
            this.request.AddHeader("Authorization", "Basic " + this.auth);
            this.request.AddHeader("Accept", "application/json");
            this.request.AddHeader("content-type", "application/json");
            this.request.IgnoreProtocolErrors = true;
        }

        
        #endregion
    }
}
