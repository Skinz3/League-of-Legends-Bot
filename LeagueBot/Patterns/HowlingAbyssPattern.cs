using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Patterns.Actions;
using LeagueBot.Constants;
using System.Drawing;
using LeagueBot.AI;
using LeagueBot.Windows;

namespace LeagueBot.Patterns
{
    public class HowlingAbyssPattern : MapPattern
    {
        public HowlingAbyssPattern(Bot bot) : base(bot)
        {
        }

        public override AbstractAI AI => new AIHowlingAbyss(Bot, this);

        public override Side GetSide()
        {
            var pt = Interop.GetPixelColor(new Point(1338, 873));

            if (pt.R == 51 && pt.G == 136 && pt.B == 170)
            {
                return Side.Blue;
            }
            else
            {
                return Side.Red;
            }
        }
    }
}
