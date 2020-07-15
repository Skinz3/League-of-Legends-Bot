using LeagueBot.Game;
using LeagueBot.Game.Entities;
using LeagueBot.IO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace LeagueBot.Api
{
    class Spell
    {
        public string name { get; set; }
        public string[] cooldown { get; set; }
        public string[] range { get; set; }
        public string[] cost { get; set; }
        public int maxrank { get; set; }
        public string championName { get; set; }

        public Spell(string name, JObject jo, string championName, int spell)
        {
            this.maxrank = (int)jo.SelectToken("data." + championName + ".spells["+spell+"].maxrank");

            List<string> cooldowns = new List<string>();
            for (int i = 0; i < this.maxrank; i++)
            {
                cooldowns.Add((string)jo.SelectToken("data." + this.championName + ".spells[" + spell + "].cooldown[" + i + "]"));
            }
            
            List<string> ranges = new List<string>();
            for (int i = 0; i < this.maxrank; i++)
            {
                ranges.Add((string)jo.SelectToken("data." + this.championName + ".spells[" + spell + "].range[" + i + "]"));
            }

            List<string> costs = new List<string>();
            for (int i = 0; i < maxrank; i++)
            {
                costs.Add((string)jo.SelectToken("data." + this.championName + ".spells[" + spell + "].cost[" + i + "]"));
            }

            this.name = name;
            this.championName = championName;
            this.range = ranges.ToArray();
            this.cooldown = cooldowns.ToArray();
            this.cost = costs.ToArray();

            Logger.Write("Spell: " + this.name + " linked.");

            foreach (var e in this.cooldown)
            {
                Console.WriteLine(e);
            }

            
            
            //Logger.Write("Range: " + this.range + " linked.");

            //Logger.Write("Cooldowns " + this.cooldown + " linked.");

            //Logger.Write("Maxlvl: " + this.maxrank + " linked.");
        }


        //public int ManaCost
        //{
        //    get
        //    {
        //        if (!IsLearned())
        //        {
        //            return 0;
        //        }
        //        return cost;
        //    }
        //}

        public bool IsLearned()
        {
            return false;
        }

        public bool IsOnCooldown()
        {
            return false;
        }

        public int Level()
        {
            return 0;
        }
    }
}
