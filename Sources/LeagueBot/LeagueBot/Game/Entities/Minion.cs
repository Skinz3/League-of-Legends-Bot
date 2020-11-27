using System.Drawing;

namespace LeagueBot.Game.Entities
{
    public class Minion : IEntity
    {
        public Minion(Point position)
        {
            Position = position;
        }

        public Point Position { get; }
    }
}