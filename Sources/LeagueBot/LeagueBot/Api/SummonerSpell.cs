using LeagueBot.ApiHelpers;
using LeagueBot.IO;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Api
{
    public class SummonerSpell
    {
        public enum SummonerSpellsEnum
        {
            Barrier,
            Clarity,
            Cleanse,
            Exhaust,
            Flash,
            Mark,
            PoroToss,
            Ghost,
            Heal,
            ToTheKing,
            Ignite,
            Smite
        }

        public string summonerName { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public int slot { get; set; }

        public SummonerSpell(string name, JObject jo, int slot)
        {
            this.summonerName = summonerName;
            this.slot = slot;
            this.name = name;
            this.displayName = (string)jo.SelectToken(name+".displayName");
            //Logger.WriteColor2("Your summoner spell:" + this.displayName);

        }

        public void use()
        {
            string key = "D" + this.slot;
            InputHelper.PressKey(key.ToString());
            BotHelper.Wait(50);
        }
    }
}
