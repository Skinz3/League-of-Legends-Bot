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
        public int spell { get; set; }
        public string championName { get; set; }
        public int level { get; set; }
        public Spell(string name, string championName, int spell)
        {
            this.level = 0;
            this.spell = spell + 1;
            this.name = name;
            this.championName = championName;
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
