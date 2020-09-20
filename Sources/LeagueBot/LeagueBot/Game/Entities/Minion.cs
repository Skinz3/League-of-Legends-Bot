using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Game.Entities
{
    public class Minion : IEntity
    {
        public Point Position
        {
            get;
            private set;
        }

        public Minion(Point position)
        {
            this.Position = position;
        }
    }
}
