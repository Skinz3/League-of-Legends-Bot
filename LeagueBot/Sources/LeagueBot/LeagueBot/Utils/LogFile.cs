using LeagueBot.DesignPattern;
using LeagueBot.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Utils
{
    public class LogFile
    {
        public const string PATH = "log.txt";

        [StartupInvoke(StartupInvokePriority.FifthPass)]
        public static void Initialize()
        {
            if (!File.Exists(PATH))
            {
                File.Create(PATH);
            }

        }

        public static void Log(object message)
        {
            try
            {
                string value = DateTime.Now + " : " + message.ToString();
                File.AppendAllLines(PATH, new string[] { value });
            }
            catch (Exception ex)
            {
                Logger.Write("Unable to log: " + ex, MessageState.ERROR);
            }
        }
    }
}
