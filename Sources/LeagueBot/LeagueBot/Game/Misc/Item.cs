namespace LeagueBot.Game.Misc
{
    public class Item
    {
        public Item(string name, int cost)
        {
            this.Name = name;
            this.Cost = cost;
            this.Buyed = false;
        }

        public bool Buyed
        {
            get;
            set;
        }

        public int Cost
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }
    }
}