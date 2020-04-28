using LeagueBot.Scripting.Patterns;
using NLua;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Scripting.Behaviours
{
    public class BehaviourManager
    {
        public const string PATH = "Behaviours/";

        public const string EXTENSION = ".lua";

        static Dictionary<string, Script> Scripts = new Dictionary<string, Script>();

        public static void Initialize()
        {
            foreach (var file in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, PATH)))
            {
                if (Path.GetExtension(file) == EXTENSION)
                {
                    string filename = Path.GetFileName(file);
                    Lua lua = new Lua();
                    lua.DoFile(file);
                    Scripts.Add(Path.GetFileNameWithoutExtension(filename), new Script(filename, lua));
                }
            }
        }
        public static bool Contains(string name)
        {
            return Scripts.ContainsKey(name);
        }
        public static void Execute(string name, string gameType)
        {
            Scripts[name].Execute(gameType);
        }
    }
}
