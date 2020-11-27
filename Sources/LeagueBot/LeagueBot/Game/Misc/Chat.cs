using InputManager;
using LeagueBot.Api;
using LeagueBot.ApiHelpers;
using LeagueBot.Windows;
using System.Windows.Forms;

namespace LeagueBot.Game.Misc
{
    public class Chat : ApiMember<GameApi>
    {
        public Chat(GameApi api) : base(api)
        {
        }

        public void talkInChampSelect(string message)
        {
            InputHelper.LeftClick(390, 940, 200);

            BotHelper.Sleep(100);

            foreach (var character in message)
            {
                Keys key = KeyboardMapper.GetKey(character);
                Keyboard.KeyPress(key, 150);
                BotHelper.Sleep(100);
            }

            BotHelper.InputIdle();
            Keyboard.KeyPress(Keys.Enter, 150);
            BotHelper.InputIdle();
        }

        public void talkInGame(string message)
        {
            Keyboard.KeyPress(Keys.Enter, 150);

            BotHelper.Sleep(100);

            foreach (var character in message)
            {
                Keys key = KeyboardMapper.GetKey(character);
                Keyboard.KeyPress(key, 150);
                BotHelper.Sleep(100);
            }

            BotHelper.InputIdle();
            Keyboard.KeyPress(Keys.Enter, 150);
            BotHelper.InputIdle();
        }
    }
}