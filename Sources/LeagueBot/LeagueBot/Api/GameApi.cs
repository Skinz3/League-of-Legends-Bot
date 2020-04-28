using InputManager;
using LeagueBot.ApiHelpers;
using LeagueBot.Image;
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
        private bool IsShopOpen
        {
            get;
            set;
        }
        public bool IsCameraLocked
        {
            get;
            set;
        }
        public bool BlueSide
        {
            get;
            set;
        }
        public GameApi()
        {
            IsShopOpen = false;
            IsCameraLocked = false;
        }

        #region Game Informations
        public void waitUntilGameStart()
        {
            ImageHelper.WaitForColor(997, 904, "#00D304");
        }
        public bool isBlueSide()
        {
            this.BlueSide = ImageHelper.GetColor(1343, 868) == "#2A768C";
            return BlueSide;
        }
        public bool isPlayerAlive()
        {
            return ImageHelper.GetColor(765, 904) == "#07140E";
        }
        #endregion

        #region Stats
        public int getHealthPercent()
        {
            int value = ImageValues.Health();
            BotHelper.Log("Health is " + value);
            return value;
        }
        public int getManaPercent()
        {
            int value = ImageValues.Mana();
            BotHelper.Log("Mana is " + value);
            return value;
        }
        #endregion

        #region Spells
        public void castSpell(int indice, int x, int y)
        {
            string key = "D" + indice;
            InputHelper.MoveMouse(x, y);
            InputHelper.PressKey(key);
            BotHelper.InputIdle();
        }
        public void upgradeSpell(int indice)
        {
            Point coords = new Point();

            switch (indice)
            {
                case 1:
                    coords = new Point(826, 833);
                    break;
                case 2:
                    coords = new Point(875, 833);
                    break;
                case 3:
                    coords = new Point(917, 833);
                    break;
                default:
                    Logger.Write("Unknown spell indice :" + indice, MessageState.WARNING);
                    return;
            }

            InputHelper.LeftClick(coords.X, coords.Y);
            BotHelper.InputIdle();
        }
        #endregion

        #region Camera
        public void toggleCamera()
        {
            InputHelper.LeftClick(1241, 920);
            BotHelper.InputIdle();
            IsCameraLocked = !IsCameraLocked;
        }
        public void lockAlly(int allyIndice)
        {
            string key = "F" + allyIndice;
            InputHelper.KeyUp(key);
            BotHelper.InputIdle();
            InputHelper.KeyDown(key);
            BotHelper.InputIdle();
        }


        #endregion

        #region Shop
        public void toogleShop()
        {
            InputHelper.PressKey("P");
            BotHelper.InputIdle();
            IsShopOpen = !IsShopOpen;
        }
        public void buyItem(int indice)
        {
            Point coords = new Point(0, 0);

            switch (indice)
            {
                case 1:
                    coords = new Point(577, 337);
                    break;
                case 2:
                    coords = new Point(782, 336);
                    break;
                default:
                    Logger.Write("Unknown item indice " + indice + ". Skipping", MessageState.WARNING);
                    return;
            }
            InputHelper.RightClick(coords.X, coords.Y);

            BotHelper.InputIdle();
        }
        #endregion

        #region Minions
        public int[] getFirstMinion()
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
        #endregion

        #region Chat
        public void talk(string message)
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
        #endregion

        #region Movement
        public void moveCenterScreen()
        {
            InputHelper.RightClick(886, 521);
            BotHelper.InputIdle();
        }
        #endregion

    }
}
