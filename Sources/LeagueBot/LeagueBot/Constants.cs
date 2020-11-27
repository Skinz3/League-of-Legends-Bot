using System.Text;

namespace LeagueBot
{
    public class Constants
    {
        public const string ClientExecutablePath = @"League of Legends\LeagueClient.exe";
        public const string ClientHostProcessName = "LeagueClient";
        public const string ClientProcessName = "LeagueClientUX"; // <--- do not modify , Riot Constants.

        // <--- do not modify , Riot Constants.

        public const string ConfigPath = "config.json";
        public const string DefaultLeaguePath = @"C:\Riot Games";
        public const int GameApiPort = 2999;
        public const int GameHeigth = 768;
        public const string GameProcessName = "League of Legends"; // <--- do not modify , Riot Constants.

        // <--- do not modify , Riot Constants.

        public const int GameWidth = 1024;
        public const int HttpRequestTimeout = 10 * 1000;

        public const string LeagueGameconfigPath = @"League of Legends\Config\game.cfg";
        public const string LeagueKeyconfigPath = @"League of Legends\Config\input.ini";
        public const string LeaguePersistedSettingsPath = @"League of Legends\Config\PersistedSettings.json";
        public static Encoding HttpRequestEncoding = Encoding.UTF8;
        // <--- do not modify , Riot Constants.

        // <--- do not modify , Riot Constants.

        // <--- do not modify , Riot Constants.

        // <--- do not modify , not scalable.

        // <--- do not modify , not scalable.

        // <--- do not modify , Riot Constants.
    }
}