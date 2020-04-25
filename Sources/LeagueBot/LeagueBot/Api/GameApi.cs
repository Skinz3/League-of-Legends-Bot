using InputManager;
using LeagueBot.Img;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueBot.Api
{
    public class GameApi
    {
        private WinApi WinApi
        {
            get;
            set;
        }

        public GameApi(WinApi winApi)
        {
            this.WinApi = winApi;
        }



        public int getHealthPercent()
        {
            int value = ImageValues.Health();
            WinApi.log("Health is " + value);
            return value;
        }
        public int getManaPercent()
        {
            int value = ImageValues.Mana();
            WinApi.log("Mana is " + value);
            return value;
        }
        public void talk(string message)
        {
            Keyboard.KeyPress(Keys.Enter, 150);
            WinApi.wait(100);

            foreach (var character in message)
            {
                Keys key = KeyboardMapper.GetKey(character);
                Keyboard.KeyPress(key, 150);
                WinApi.wait(100);
            }

            WinApi.wait(100);
            Keyboard.KeyPress(Keys.Enter, 150);

        }
        
    }
}
