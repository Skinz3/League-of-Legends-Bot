using LeagueBot.DesignPattern;
using LeagueBot.IO;
using LeagueBot.Windows;
using NLua;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueBot.Patterns
{
    class PatternsManager
    {
        public const string PATH = "Patterns/";

        public const string EXTENSION = ".lua";

        static Dictionary<string, PatternScript> Scripts = new Dictionary<string, PatternScript>();

        [StartupInvoke("Patterns", StartupInvokePriority.SecondPass)]
        public static void Initialize()
        {
            foreach (var file in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, PATH)))
            {
                if (Path.GetExtension(file) == EXTENSION)
                {
                    string filename = Path.GetFileName(file);
                    Lua lua = new Lua();
                    lua.DoFile(file);
                    Scripts.Add(Path.GetFileNameWithoutExtension(filename), new PatternScript(filename, lua));
                }
            }
        }
        public static bool Contains(string name)
        {
            return Scripts.ContainsKey(name);
        }
        public static void Execute(string name)
        {
            if (!Scripts.ContainsKey(name))
            {
                Logger.Write("Unable to execute " + name + EXTENSION + ". Script not found.");
            }
            else
            {
                Scripts[name].Execute();
            }
        }
        public static string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var script in Scripts)
            {
                sb.AppendLine("-" + script.Value.ToString());
            }

            return sb.ToString();
        }
    }
}
