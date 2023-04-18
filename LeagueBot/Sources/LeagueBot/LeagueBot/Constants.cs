using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot
{
    public class Constants
    {
        public const string ClientProcessName = "LeagueClientUX"; // <--- do not modify , Riot Constants.

        public const string ClientHostProcessName = "LeagueClient"; // <--- do not modify , Riot Constants.

        public const string GameProcessName = "League of Legends"; // <--- do not modify , Riot Constants.

        public const string ClientExecutablePath = @"League of Legends\LeagueClient.exe"; // <--- do not modify , Riot Constants.

        public const int HttpRequestTimeout = 10 * 1000;

        public static Encoding HttpRequestEncoding = Encoding.UTF8;

        public const string DefaultLeaguePath = @"C:\Riot Games";

        public const string ConfigPath = "config.json";

        public const string LeagueGameconfigPath = @"League of Legends\Config\game.cfg"; // <--- do not modify , Riot Constants.

        public const string LeagueKeyconfigPath = @"League of Legends\Config\input.ini"; // <--- do not modify , Riot Constants.

        public const string LeaguePersistedSettingsPath = @"League of Legends\Config\PersistedSettings.json"; // <--- do not modify , Riot Constants.

        public const int GameWidth = 1024; // <--- do not modify , not scalable.

        public const int GameHeigth = 768; // <--- do not modify , not scalable.

        public const int GameApiPort = 2999; // <--- do not modify , Riot Constants.

    }
}
