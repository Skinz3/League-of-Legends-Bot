using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Patterns;
using System.Threading;
using LeagueBot.Constants;
using LeagueBot.Windows;
using System.Drawing;
using InputManager;
using System.Windows.Forms;

namespace LeagueBot.AI
{
    public class AIHowlingAbyss : AbstractAI
    {
       
        public AIHowlingAbyss(Bot bot, MapPattern pattern) : base(bot, pattern)
        {
        }
        public override void Start()
        {
            base.Start();
            Thread.Sleep(3000);
            Interop.CenterProcessWindow(LeagueConstants.LoL_GAME_PROCESS);
            Summoner.LockCamera();
            Thread.Sleep(1000);
            Bot.LeftClick(PixelsConstants.SHOP_BUTTON);
            Thread.Sleep(500);
            Bot.RightClick(PixelsConstants.SHOP_ITEM1);
            Thread.Sleep(1000);
            Bot.RightClick(PixelsConstants.SHOP_ITEM2);
            Thread.Sleep(500);
            Keyboard.KeyPress(Keys.Escape);

            Thread.Sleep(5000);
            Summoner.Talk("Hi team");
            Summoner.MaxE();
            Thread.Sleep(500);
            Summoner.MaxQ();
            Thread.Sleep(500);
            Summoner.MaxZ();
            Thread.Sleep(500);
            Summoner.Move(PixelsConstants.HOWLING_ABYSS_BUSH1);
            Summoner.Wait(20);
            bool playing = true;

            while (playing)
            {
                if (!Interop.IsProcessOpen(Pattern.ProcessName))
                {
                    playing = false;
                    Pattern.OnProcessClosed();
                }
                else
                {
                    if (Side == Side.Blue)
                        Summoner.Move(PixelsConstants.HOWLING_ABYSS_BLUE_T1);
                    else
                        Summoner.Move(PixelsConstants.HOWLING_ABYSS_RED_T1);
                    Thread.Sleep(2000);

                    Point blueSideAim = new Point(1085, 431);
                    Point redSideAim = new Point(790, 590);

                    Point target = Side == Side.Blue ? blueSideAim : redSideAim;

                    Summoner.Q();
                    Thread.Sleep(500);
                    Mouse.Move(target.X, target.Y);
                    Summoner.E();
                    Thread.Sleep(500);
                    Mouse.Move(target.X, target.Y);
                    Summoner.Z();
                    Thread.Sleep(500);
                    Mouse.Move(target.X, target.Y);
                    Summoner.R();

                    if (Side ==  Side.Blue)
                        Summoner.Move(PixelsConstants.HOWLING_ABYSS_REDNEXUS);
                    else
                        Summoner.Move(PixelsConstants.HOWLING_ABYSS_BLUENEXUS);

                    Thread.Sleep(3000);
                    AsyncRandom rd = new AsyncRandom();
                    int shiftX = rd.Next(-3, 3);
                    int shiftY = rd.Next(-3, 3);
                    var closeRandomPt = new Point(Summoner.Position.X + shiftX, Summoner.Position.Y + shiftY);
                    Bot.RightClick(closeRandomPt);
                    Thread.Sleep(1000);
                    Summoner.Summoner1();
                    Summoner.Summoner2();

                }
            }
        }
        private void OnProcessClosed()
        {

        }
        public override void Stop()
        {
        }
    }
}
