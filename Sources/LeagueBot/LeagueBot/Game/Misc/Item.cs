using System.Drawing;

namespace LeagueBot.Game.Misc
{
    public class Item
    {
        public string name = "NoName";
        public int cost = 0;
        public bool got = false;
        public bool canStack = false;
        public int buyOrder = 0;
        public Point point;

        public Item(string name,int cost, bool got, bool canstack, int buyorder, Point position)
        {
            this.name = name;
            this.cost = cost; 
            this.got = got;
            canStack = canstack;
            buyOrder = buyorder;
            point = position;
        }
    }
}