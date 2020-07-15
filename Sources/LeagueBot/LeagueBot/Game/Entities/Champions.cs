using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LeagueBot.IO;

namespace LeagueBot.Game.Entities
{
    class Champions
    {
        Dictionary<string, int> champlist;

        public Champions()
        {
            try
            {
                readFile(Directory.GetCurrentDirectory() + @"\ids.txt");
            }
            catch
            {
                Logger.Write("Champion IDs could not be found. Are you missing ids.txt?");
            }

        }

        public int getIdByChamp(string champ)
        {
            int id;
            if (!champlist.TryGetValue(champ.ToLower(), out id))
            {
                return 0;
            }
            return id;
        }

        private void readFile(string path)
        {
            champlist = new Dictionary<string, int>();
            if (File.Exists(path))
            {
                string[] allchamps = File.ReadAllLines(path);

                foreach (string champ in allchamps)
                {

                    string[] x = champ.Split(':');
                    champlist.Add(x[1], int.Parse(x[0]));
                }

            }
            else
            {
                Logger.Write("ids.txt could not be found");
            }
        }

    }
}
