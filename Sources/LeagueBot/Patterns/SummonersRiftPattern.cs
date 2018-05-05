using LeagueBot.Constants;
using LeagueBot.Patterns.Actions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.AI;

namespace LeagueBot.Patterns
{
    public class SummonersRiftPattern : MapPattern
    {
        public SummonersRiftPattern(Bot bot) : base(bot)
        {
        }

        public override AbstractAI AI => new AISummonerRift(Bot, this);

        public override Side GetSide()
        {
            throw new NotImplementedException();
        }
    }
}
