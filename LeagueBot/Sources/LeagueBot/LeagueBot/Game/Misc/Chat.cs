using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InputManager;
using LeagueBot.Api;
using LeagueBot.ApiHelpers;
using LeagueBot.Windows;

namespace LeagueBot.Game.Misc
{
    public class Chat : ApiMember<GameApi>
    {
        public Chat(GameApi api) : base(api)
        {
        }
        public void talkInGame(string message)
        {
            Keyboard.KeyPress(Keys.Enter, 150);

            BotHelper.Wait(100);

            foreach (var character in message)
            {
                Keys key = KeyboardMapper.GetKey(character);
                Keyboard.KeyPress(key, 150);
                BotHelper.Wait(100);
            }

            BotHelper.InputIdle();
            Keyboard.KeyPress(Keys.Enter, 150);
            BotHelper.InputIdle();
        }

        public void talkInChampSelect(string message)
        {
            InputHelper.LeftClick(390, 940, 200);

            BotHelper.Wait(100);

            foreach (var character in message)
            {
                Keys key = KeyboardMapper.GetKey(character);
                Keyboard.KeyPress(key, 150);
                BotHelper.Wait(100);
            }

            BotHelper.InputIdle();
            Keyboard.KeyPress(Keys.Enter, 150);
            BotHelper.InputIdle();
        }
    }
}
