using LeagueBot.ApiHelpers;
using LeagueBot.Game;
using LeagueBot.Game.Entities;
using LeagueBot.Image;
using LeagueBot.IO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace LeagueBot.Api
{
    public class Spell
    {
        public string name { get; set; }
        public string[] cooldown { get; set; }
        public string[] range { get; set; }
        public string[] cost { get; set; }
        public int maxrank { get; set; }
        public int spell { get; set; }
        public string championName { get; set; }
        public int level { get; set; }
        public Spell(string name, JObject jo, string championName, int spell)
        {
            this.level = 0;
            this.spell = spell + 1;
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
            

        }

        public void cast()
        {
            Point go = ImageValues.EnemyChampion();

            if (go.X == 0 && go.Y == 0)
                return;

            InputHelper.MoveMouse(go.X + 39, go.Y + 129);

            string key = "D" + this.spell;

            InputHelper.PressKey(key.ToString());
            BotHelper.Wait(50);
        }

        public string ManaCost
        {
            get
            {
                if (!IsLearned())
                {
                    return "not learned";
                }
                return this.cost[0];
            }
        }

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
