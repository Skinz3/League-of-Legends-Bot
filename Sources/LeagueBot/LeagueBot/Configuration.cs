using LeagueBot.DesignPattern;
using LeagueBot.IO;
using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace LeagueBot
{
    public class Configuration
    {
        public static Configuration Instance { get; private set; }

        public string ClientPath { get; private set; }

        public static void CreateConfig(string clientPath)
        {
            Instance = new Configuration
            {
                ClientPath = clientPath,
            };

            Save();

            Logger.Write("Configuration file created!", LogLevel.SUCCES);
        }

        [StartupInvoke("Config", StartupInvokePriority.Initial)]
        public static void InitCall()
        {
            ThreadStart threadStart = new ThreadStart(() => Load());// <--- One STA Thread for windows UI ... Can we do it another way without declare Main() as STA Thread ?

            Thread thread = new Thread(threadStart);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        public static void Save()
        {
            File.WriteAllText(Constants.ConfigPath, Json.Serialize(Instance));
        }

        private static bool Initialize()
        {
            if (File.Exists(Constants.ConfigPath))
            {
                try
                {
                    Instance = Json.Deserialize<Configuration>(File.ReadAllText(Constants.ConfigPath));
                    return true;
                }
                catch
                {
                    File.Delete(Constants.ConfigPath);
                }
            }

            return default;
        }

        private static bool Load()
        {
            if (!Initialize())
            {
                string path = Constants.DefaultLeaguePath;

                if (!Directory.Exists(path))
                {
                    var result = MessageBox.Show("Please select the league of legends 'Riot Game' folder.", "Hello", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk);

                    if (result == MessageBoxResult.Cancel)
                    {
                        Environment.Exit(0);
                        return false;
                    }

                    var folderOpen = new FolderBrowserDialog();
                    folderOpen.Description = "Please select the league of legends 'Riot Game' folder.";

                    if (folderOpen.ShowDialog() == DialogResult.OK)
                    {
                        path = folderOpen.SelectedPath;
                        string dirName = new DirectoryInfo(path).Name;

                        if (!Directory.Exists(path) || dirName != "Riot Games")
                        {
                            MessageBox.Show("Invalid Directory.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return Load();
                        }
                    }
                    else
                        return Load();
                }

                CreateConfig(path);
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}