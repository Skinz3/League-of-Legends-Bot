using System.Drawing;

namespace LeagueBot.Game.Entities
{
    public class Champion : IEntity
    {
        public Champion(bool ally, Point position)
        {
            this.Ally = ally;
            this.Position = position;
        }

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
    }
}