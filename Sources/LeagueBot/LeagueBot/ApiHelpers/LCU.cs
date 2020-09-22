using LeagueBot.Game.Enums;
using LeagueBot.Patterns;
using LeagueBot.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueBot.ApiHelpers
{
    public class LCU
    {
        public static int ApiPort = 2999;

        public static string ApiUrl = "https://127.0.0.1:" + ApiPort + "/liveclientdata";

        public static string ActivePlayerUrl = ApiUrl + "/activeplayer";

        public static string PlayerListUrl = ApiUrl + "/playerlist";

        public static string GameStatsUrl = ApiUrl + "/gamestats";

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

        public static void Initialize()
        {
            ApiPort = GetPort();
        }

        public static dynamic GetAlly(int id)
        {
            var resultStr = Http.GetString(PlayerListUrl);
            dynamic dyn = JsonConvert.DeserializeObject(resultStr);
            return dyn[id - 1];
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

        public static int GetPlayerGolds()
        {
            var resultStr = Http.GetString(ActivePlayerUrl);
            dynamic dyn = JsonConvert.DeserializeObject(resultStr);
            return (int)dyn.currentGold;
        }

        public static dynamic GetAllies()
        {
            var resultStr = Http.GetString(PlayerListUrl);
            dynamic dyn = JsonConvert.DeserializeObject(resultStr);
            return dyn;
        }

        public static dynamic GetStats()
        {
            var resultStr = Http.GetString(ActivePlayerUrl);
            dynamic dyn = JsonConvert.DeserializeObject(resultStr);
            return dyn.championStats;
        }
        private static int GetPort()
        {
            var processes = Process.GetProcessesByName(PatternScript.CLIENT_PROCESS_NAME);

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
    }
}
