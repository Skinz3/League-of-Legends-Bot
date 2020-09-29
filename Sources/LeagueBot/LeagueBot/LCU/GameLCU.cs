using Leaf.xNet;
using LeagueBot.Game.Enums;
using LeagueBot.IO;
using LeagueBot.Patterns;
using LeagueBot.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueBot.ApiHelpers
{
    public class GameLCU
    {
        public static string ApiUrl = "https://127.0.0.1:" + Constants.GameApiPort + "/liveclientdata";

        public static string ActivePlayerUrl = ApiUrl + "/activeplayer";

        public static string PlayerListUrl = ApiUrl + "/playerlist";

        public static string GameStatsUrl = ApiUrl + "/gamestats";

        public static bool IsApiReady()
        {
            try
            {
                using (HttpRequest request = new HttpRequest())
                {
                    request.CharacterSet = Constants.HttpRequestEncoding;
                    request.IgnoreProtocolErrors = true;
                    request.ConnectTimeout = Constants.HttpRequestTimeout;
                    request.ReadWriteTimeout = Constants.HttpRequestTimeout;

                    var response = request.Get(PlayerListUrl);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                Logger.Write("Unable to communicate with LCU Api...", MessageState.WARNING);
                return false;
            }
        }

        public static bool IsPlayerDead()
        {
            return GetStats().currentHealth == 0;
        }

        public static int GetPlayerLevel()
        {
            var resultStr = HttpGetString(ActivePlayerUrl);
            dynamic dyn = JsonConvert.DeserializeObject(resultStr);
            return dyn.level;
        }
        public static string GetPlayerName()
        {
            var resultStr = HttpGetString(ActivePlayerUrl);
            dynamic dyn = JsonConvert.DeserializeObject(resultStr);
            return dyn.summonerName;
        }

        public static double GetGameTime()
        {
            var resultStr = HttpGetString(GameStatsUrl);
            dynamic dyn = JsonConvert.DeserializeObject(resultStr);
            return dyn.gameTime;
        }


        public static dynamic GetAlly(int id)
        {
            var resultStr = HttpGetString(PlayerListUrl);
            dynamic dyn = JsonConvert.DeserializeObject(resultStr);
            return dyn[id - 1];
        }
        public static SideEnum GetPlayerSide()
        {
            string playerName = GetPlayerName();

            var resultStr = HttpGetString(PlayerListUrl);

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

        public static int GetPlayerGolds()
        {
            var resultStr = HttpGetString(ActivePlayerUrl);
            dynamic dyn = JsonConvert.DeserializeObject(resultStr);
            return (int)dyn.currentGold;
        }

        public static dynamic GetAllies()
        {
            var resultStr = HttpGetString(PlayerListUrl);
            dynamic dyn = JsonConvert.DeserializeObject(resultStr);
            return dyn;
        }

        public static dynamic GetStats()
        {
            var resultStr = HttpGetString(ActivePlayerUrl);
            dynamic dyn = JsonConvert.DeserializeObject(resultStr);
            return dyn.championStats;
        }
        /// <summary>
        /// unused yet.
        /// </summary>
        /// <returns></returns>
        private static int GetPort()
        {
            var processes = Process.GetProcessesByName(Constants.ClientProcessName);

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
                            return int.Parse(outp[6].Replace("127.0.0.1:", ""));
                        }
                    }
                }
            }
            return 0;
        }

        public static string HttpGetString(string url)
        {
            using (HttpRequest request = new HttpRequest())
            {
                request.IgnoreProtocolErrors = true;
                request.CharacterSet = Constants.HttpRequestEncoding;
                request.ConnectTimeout = Constants.HttpRequestTimeout;
                request.ReadWriteTimeout = Constants.HttpRequestTimeout;
                return request.Get(url).ToString();
            }
        }
    }
}
