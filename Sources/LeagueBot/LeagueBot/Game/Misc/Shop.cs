using LeagueBot.Api;
using LeagueBot.ApiHelpers;

namespace LeagueBot.Game.Misc
{
    public class Shop : ApiMember<GameApi>
    {
        public Shop(GameApi api) : base(api)
        {
        }

        public bool IsOpen { get; private set; }

        public void BuyCurrentItem()
        {
            BotHelper.InputIdle();

            InputHelper.PressKey("Enter");

            BotHelper.InputIdle();

            InputHelper.PressKey("Enter");

            BotHelper.InputIdle();
        }

        public void SearchForItem(string name)
        {
            InputHelper.KeyDown("LControlKey");
            InputHelper.KeyDown("L");
            InputHelper.KeyUp("LControlKey");
            InputHelper.KeyUp("L");

            BotHelper.InputIdle();

            InputHelper.InputWords(name);
        }

        public void ToggleOpen()
        {
            if (IsOpen)
                InputHelper.PressKey("Escape");
            else
                InputHelper.PressKey("P");

            BotHelper.InputIdle();
            IsOpen = !IsOpen;
        }
    }
}