using LeagueBot.DesignPattern;
using LeagueBot.IO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace LeagueBot
{
    public class Configuration
    {
        public const string DEFAULT_LEAGUE_PATH = @"C:\Riot Games";

        public const string CONFIG_PATH = "config.json";

        public static Configuration Instance
        {
            get;
            private set;
        }
        public string ClientPath
        {
            get;
            set;
        }

        [StartupInvoke("Config", StartupInvokePriority.Initial)]
        public static void LoadConfig()
        {
            if (!Initialize())
            {
                CreateConfig(DEFAULT_LEAGUE_PATH);
            }

            if (!Directory.Exists(Instance.ClientPath))
            {
                var result = MessageBox.Show("Please edit " + CONFIG_PATH + " to locate your 'Riot Games' folder.", "Configuration", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Process.Start(CONFIG_PATH);
                Environment.Exit(0);
            }

        }

        private static bool Initialize()
        {
            if (File.Exists(CONFIG_PATH))
            {
                try
                {
                    Instance = Json.Deserialize<Configuration>(File.ReadAllText(CONFIG_PATH));
                    return true;
                }
                catch
                {
                    File.Delete(CONFIG_PATH);
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
        public static void CreateConfig(string clientPath)
        {
            Instance = new Configuration()
            {
                ClientPath = clientPath,
            };

            Save();

            Logger.Write("Configuration file created!", MessageState.SUCCES);
        }
        public static void Save()
        {
            File.WriteAllText(CONFIG_PATH, Json.Serialize(Instance));
        }

        private static bool IsValidDofusPath(string path)
        {
            string combined = Path.Combine(path, @"content/maps");
            return Directory.Exists(combined);
        }
    }
}
