using InputManager;
using LeagueBot.Constants;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueBot
{
    public class Summoner
    {
        private Bot Bot
        {
            get;
            set;
        }
        public Summoner(Bot bot)
        {
            Bot = bot;
        }
        public Point Position
        {
            get;
            set;
        }

        public void Summoner1()
        {
            Keyboard.KeyPress(Keys.E);
        }
        public void Summoner2()
        {
            Keyboard.KeyPress(Keys.D5);
        }
        public void Z()
        {
            Keyboard.KeyPress(Keys.D2);
        }
        public void Q()
        {
            Keyboard.KeyPress(Keys.D1);
        }
        public void E()
        {
            Keyboard.KeyPress(Keys.D3);
        }
        public void R()
        {
            Keyboard.KeyPress(Keys.D4);
        }
        public void MaxQ()
        {
            Bot.LeftClick(PixelsConstants.UP_Q_SLOT);
        }
        public void MaxZ()
        {
            Bot.LeftClick(PixelsConstants.UP_Z_SLOT);
        }
        public void MaxE()
        {
            Bot.LeftClick(PixelsConstants.UP_E_SLOT);
        }
        public void Back()
        {
            Keyboard.KeyPress(Keys.B);
            Thread.Sleep(9000);
        }
        public void Move(Point point)
        {
            Position = point;
            Bot.RightClick(point);
        }
        public void LockCamera()
        {
            Bot.LeftClick(PixelsConstants.LOCK_CAMERA_BUTTON);
            Thread.Sleep(1000);
        }

   
        public void Wait(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }
       
     
     
        public void Talk(string message)
        {
            Keyboard.KeyPress(Keys.Return);
            Thread.Sleep(50);
            foreach (var letter in message)
            {
                if (letter == ' ')
                {
                    Keyboard.KeyDown(Keys.Space);
                }
                else
                {
                    var key = (Keys)Enum.Parse(typeof(Keys), char.ToUpper(letter).ToString());
                    Keyboard.KeyDown(key);
                }
                Thread.Sleep(100);
            }
            Keyboard.KeyPress(Keys.Return);
            Thread.Sleep(150);
        }
    }
}
