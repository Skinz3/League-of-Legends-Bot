using InputManager;
using LeagueBot.Windows;
using System;
using System.Windows.Forms;

namespace LeagueBot.ApiHelpers
{
    internal class InputHelper
    {
        public static void InputWords(string message, int keyDelay = 50, int delay = 100)
        {
            BotHelper.Sleep(60);

            foreach (var character in message)
            {
                Keys key = KeyboardMapper.GetKey(character);
                Keyboard.KeyPress(key, keyDelay);
                BotHelper.Sleep(delay);
            }

            BotHelper.Sleep(60);
        }

        public static void KeyDown(string key)
        {
            Keyboard.KeyDown((Keys)Enum.Parse(typeof(Keys), key));
        }

        public static void KeyUp(string key)
        {
            Keyboard.KeyUp((Keys)Enum.Parse(typeof(Keys), key));
        }

        public static void LeftClick(int x, int y, int delay = 50)
        {
            Mouse.Move(x, y);
            BotHelper.Sleep(60);
            Mouse.PressButton(Mouse.MouseKeys.Left, delay);
        }

        public static void MoveMouse(int x, int y)
        {
            Mouse.Move(x, y);
        }

        public static void PressKey(string key)
        {
            Keyboard.KeyPress((Keys)Enum.Parse(typeof(Keys), key), 50);
        }

        public static void RightClick(int x, int y, int delay = 50)
        {
            Mouse.Move(x, y);
            BotHelper.Sleep(60);
            Mouse.PressButton(Mouse.MouseKeys.Right, delay);
        }
    }
}