using InputManager;
using LeagueBot.Patterns;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LeagueBot.Windows.Interop;

namespace LeagueBot.ApiHelpers
{
    class InputHelper
    {
        public static void InputWords(string message, int keyDelay = 50, int delay = 100)
        {
            BotHelper.Wait(60);

            foreach (var character in message)
            {
                Keys key = KeyboardMapper.GetKey(character);
                Keyboard.KeyPress(key, keyDelay);
                BotHelper.Wait(delay);
            }

            BotHelper.Wait(60);
        }
        public static void KeyUp(string key)
        {
            Keyboard.KeyUp((Keys)Enum.Parse(typeof(Keys), key));
        }
        public static void KeyDown(string key)
        {
            Keyboard.KeyDown((Keys)Enum.Parse(typeof(Keys), key));
        }
        public static void PressKey(string key)
        {
            Keyboard.KeyPress((Keys)Enum.Parse(typeof(Keys), key), 50);
        }
       

        public static void MoveMouse(int x, int y)
        {
            Mouse.Move(x, y);
        }
        public static void RightClick(int x, int y, int delay = 50)
        {
            Mouse.Move(x, y);
            BotHelper.Wait(60);
            Mouse.PressButton(Mouse.MouseKeys.Right, delay);
        }
        public static void LeftClick(int x, int y, int delay = 50)
        {
            Mouse.Move(x, y);
            BotHelper.Wait(60);
            Mouse.PressButton(Mouse.MouseKeys.Left, delay);
        }




    }
}
