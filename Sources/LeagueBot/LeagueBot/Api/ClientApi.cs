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
        private static readonly RegexOptions RegexOption = RegexOptions.Multiline;
        HttpRequest request = new HttpRequest();

        private int queueId = 830; //bots intro, forgot intermediate id
        private string URL { get { return "https://127.0.0.1:" + GetPort() + "/"; } }
        /* You can create Objects for this variables and read/write info */
         
        private string create = "lol-lobby/v2/lobby";
        private string search = "lol-lobby/v2/lobby/matchmaking/search";
        private string accept = "lol-matchmaking/v1/ready-check/accept";
        private string pick = "lol-champ-select/v1/session/actions/";
        private string session = "lol-champ-select/v1/session";

        /* */
        private bool inQueue = false;
        private bool inChampSelect = false;


        public void createLobby()
        {
            updateRequest();
            string response = request.Post(URL + create, "{\"queueId\": " + this.queueId + "}", "application/json").StatusCode.ToString();
            Logger.Write("Lobby created");
            /* response should throw "OK" or "Forbidden" */
        }

        public void searchGame()
        {
            updateRequest();
            string response = request.Post(URL + search).ToString();

            while (response.Contains("QUEUE_DODGER") || response.Contains("LEAVER_BUSTED"))
            {
                Logger.Write("Leave buster or queue dodge");
                Thread.Sleep(1000);
                Logger.Write("Waiting for ending leave buster penatly...");
            }

            Logger.Write("In Queue");

            /* you can deserialize response to object and get more data like:
            //queueTime = Math.Round((double)data["estimatedQueueTime"]
            */
        }

        public void acceptGame()
        {
            updateRequest();
            request.Post(URL + accept);
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
            }
        }



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
        public void onGameEnd()
        {
            Console.Title = Assembly.GetEntryAssembly().GetName().Name;
        }


        public void updateRequest()
        {
            this.request = new HttpRequest();
            this.request.AddHeader("Authorization", "Basic " + GetAuth());
            this.request.AddHeader("Accept", "application/json");
            this.request.AddHeader("content-type", "application/json");
            this.request.IgnoreProtocolErrors = true;
        }

        private String GetAuth()
        {
            
            String pattern = "(?<port>port=(\\S+))|(?<token>token=(\\S+))";
            ManagementClass managementClass = new ManagementClass("Win32_Process");
            foreach (ManagementBaseObject manageBaseobj in managementClass.GetInstances())
            {
                ManagementObject manageObj = (ManagementObject)manageBaseobj;
                if (manageObj["Name"].Equals("LeagueClientUx.exe"))
                {
                    String CommandLineArguments = manageObj["CommandLine"].ToString();
                    if (CommandLineArguments.Contains("port"))
                    {
                        MatchCollection matches = Regex.Matches(CommandLineArguments, pattern, RegexOption);
                        foreach (Match match in matches)
                        {
                            
                            if (!string.IsNullOrEmpty(match.Groups["token"].ToString()))
                            {
                                //this.auth = match.Groups[0].ToString().Replace("token=", "");
                                this.auth = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("riot:" + match.Groups[0].ToString().Replace("token=", "")));
                                return auth;

                            }
                        }
                    }



                }
            }

            if (this.auth == null)
            {
                Logger.Write("Cant read LeagueClientUx process.");
                return null;
            }
            return null;
        }

        private String GetPort()
        {
            var processes = Process.GetProcessesByName("LeagueClientUx");

            using (var ns = new Process())
            {
                ProcessStartInfo psi = new ProcessStartInfo("netstat.exe", "-ano");
                psi.RedirectStandardOutput = true;
                psi.UseShellExecute = false;
                ns.StartInfo = psi;
                ns.Start();

                using (StreamReader r = ns.StandardOutput)
                {
                    string output = r.ReadToEnd();
                    ns.WaitForExit();

                    string[] lines = output.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                    foreach (string line in lines)
                    {
                        if (line.Contains(processes[0].Id.ToString()) && line.Contains("0.0.0.0:0"))
                        {
                            var outp = line.Split(' ');
                            return outp[6].Replace("127.0.0.1:", "");
                        }
                    }
                }
            }
            return String.Empty;
        }
    }
}
