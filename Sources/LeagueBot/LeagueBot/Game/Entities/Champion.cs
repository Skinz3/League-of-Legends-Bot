using System.Drawing;

namespace LeagueBot.Game.Entities
{
    public class Champion : IEntity
    {
        public Champion(bool ally, Point position)
        {
            Ally = ally;
            Position = position;
        }

        public bool Ally { get; }

        public Point Position { get; }
    }
}