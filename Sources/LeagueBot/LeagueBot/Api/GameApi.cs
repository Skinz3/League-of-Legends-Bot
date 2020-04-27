using InputManager;
using LeagueBot.Img;
using LeagueBot.IO;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public void castSpell(int indice, int x, int y)
        {
            string key = "D" + indice;
            WinApi.moveMouse(x, y);
            WinApi.pressKey(key);
            WinApi.wait(200);
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
        public bool playerAlive()
        {
            return WinApi.getColor(765, 904) == "#07140E";
        }
        public void toggleCamera()
        {
            WinApi.leftClick(1241, 920);
            WinApi.wait(200);
        }
        public int[] getNearestMinion()
        {
            Logger.Write("getNearestMinion is not implemented yet.");
            return new int[2];

            var pt = ImageValues.GetNearestMinion();

            if (pt == Point.Empty)
            {
                return null;
            }
            else
            {
                return new int[] { pt.X, pt.Y };
            }
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
