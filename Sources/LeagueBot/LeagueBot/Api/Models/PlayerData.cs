using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LeagueBot.Api.Models
{
    public class PlayerData
    {
        public string ChampionName { get; set; }
        public bool IsBot { get; set; }
        public bool IsDead { get; set; }
        public int Level { get; set; }
        public string RolePosition { get; set; }
        public string RawChampionName { get; set; }
        public float RespawnTimer { get; set; }
        public int SkinID { get; set; }
        public string SummonerName { get; set; }
        public string Team { get; set; }
        public IList<Item> Items { get; set; }
    }
}
