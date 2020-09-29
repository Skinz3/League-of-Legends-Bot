using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot
{
    public class Constants
    {
        public const string ClientProcessName = "LeagueClientUX";

        public const string ClientHostProcessName = "LeagueClient";

        public const string GameProcessName = "League of Legends";

        public const string ClientExecutablePath = @"League of Legends\LeagueClient.exe";

        public const int HttpRequestTimeout = 10 * 1000;

        public const string DefaultLeaguePath = @"C:\Riot Games";

        public const string ConfigPath = "config.json";

        public const string LeagueGameconfigPath = @"League of Legends\Config\game.cfg";

        public const string LeagueKeyconfigPath = @"League of Legends\Config\input.ini";

        public const string LeaguePersistedSettingsPath = @"League of Legends\Config\PersistedSettings.json";

        public const int GameWidth = 1024; // <--- do not modify , not scalable yet

        public const int GameHeigth = 768;

        public const int GameApiPort = 2999;

    }
}
