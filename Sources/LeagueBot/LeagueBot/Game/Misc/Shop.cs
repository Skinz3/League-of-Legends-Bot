using LeagueBot.Api;
using LeagueBot.ApiHelpers;

namespace LeagueBot.Game.Misc
{
    public class Shop : ApiMember<GameApi>
    {
        public Shop(GameApi api) : base(api)
        {
            this.Opened = false;
        }

        public bool Opened
        {
            get;
            set;
        }

        public void buySearchedItem()
        {
            BotHelper.InputIdle();

            InputHelper.PressKey("Enter");

            BotHelper.InputIdle();

            InputHelper.PressKey("Enter");

            BotHelper.InputIdle();
        }

        public void searchItem(string name)
        {
            InputHelper.KeyDown("LControlKey");
            InputHelper.KeyDown("L");
            InputHelper.KeyUp("LControlKey");
            InputHelper.KeyUp("L");

            BotHelper.InputIdle();

            InputHelper.InputWords(name);
        }

        public void toogle()
        {
            if (Opened)
            {
                InputHelper.PressKey("Escape");
            }
            else
            {
                InputHelper.PressKey("P");
            }

            BotHelper.InputIdle();
            Opened = !Opened;
        }
    }
}