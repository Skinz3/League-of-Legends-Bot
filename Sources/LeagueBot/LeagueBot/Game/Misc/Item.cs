namespace LeagueBot.Game.Misc
{
    public class Item
    {
        public Item(string name, int cost)
        {
            Name = name;
            GoldCost = cost;
        }

        public bool Bought { get; }

        public int GoldCost { get; }

        public string Name { get; }
    }
}