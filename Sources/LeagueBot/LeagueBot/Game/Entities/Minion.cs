using System.Drawing;

namespace LeagueBot.Game.Entities
{
    public class Minion : IEntity
    {
        public Minion(Point position)
        {
            this.Position = position;
        }

        public Point Position
        {
            get;
            private set;
        }
    }
}