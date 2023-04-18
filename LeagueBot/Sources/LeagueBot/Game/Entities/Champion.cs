using LeagueBot.Game.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Game.Entities
{
    public class Champion : IEntity
    {
        public bool Ally
        {
            get;
            private set;
        }
        public Point Position
        {
            get;
            private set;
        }

        public Champion(bool ally, Point position)
        {
            this.Ally = ally;
            this.Position = position;
        }

    }
}
