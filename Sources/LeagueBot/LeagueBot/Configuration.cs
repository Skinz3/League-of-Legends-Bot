using LeagueBot.IO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        public static bool LoadConfig()
        {
            if (!Initialize())
            {
                string path = DEFAULT_LEAGUE_PATH;

                if (!Directory.Exists(path))
                {
                    var result = MessageBox.Show("Please select the league of legends 'Riot Game' folder.", "Hello", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk);

                    if (result == MessageBoxResult.Cancel)
                    {
                        Environment.Exit(0);
                        return false;
                    }
                    FolderBrowserDialog folderOpen = new FolderBrowserDialog();
                    folderOpen.Description = "Please select the league of legends 'Riot Game' folder.";

                    if (folderOpen.ShowDialog() == DialogResult.OK)
                    {
                        path = folderOpen.SelectedPath;
                        string dirName = new DirectoryInfo(path).Name;

                        if (!Directory.Exists(path) || dirName != "Riot Games")
                        {
                            MessageBox.Show("Invalid Directory.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return LoadConfig();
                        }
                    }
                    else
                        return LoadConfig();

                }

                CreateConfig(path);
                return true;

            }
            else
            {
                return true;
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
