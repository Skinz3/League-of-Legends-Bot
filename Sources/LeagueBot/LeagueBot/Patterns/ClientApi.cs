using InputManager;
using LeagueBot.Img;
using LeagueBot.IO;
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

namespace LeagueBot.Patterns
{
    public class ClientApi
    {


        private WinApi WinApi
        {
            get;
            set;
        }

        public ClientApi(WinApi winApi)
        {
            this.WinApi = winApi;
        }




        //Wait for the client by detecting when the play button is displayed
        public void waitForPlayButton()
        {

            bool normal = ImageRecognition.ImageExists("playNormal.png");
            bool hover = ImageRecognition.ImageExists("playHover.png");

            while (!normal && !hover)
            {
                normal = ImageRecognition.ImageExists("playNormal.png");
                hover = ImageRecognition.ImageExists("playHover.png");
                Thread.Sleep(1000);
            }

        }


        public void clickPlayButton()
        {

            if (ImageRecognition.ImageExists("playNormal.png"))
            {

                Point coords = ImageRecognition.ImageCoords("playNormal.png");
                Mouse.Move(coords.X + (PixelCache.GetWidth("playNormal.png") / 2), coords.Y + (PixelCache.GetHeight("playNormal.png") / 2));
                Mouse.PressButton(Mouse.MouseKeys.Left, 150);

            }
            else if (ImageRecognition.ImageExists("playHover.png"))
            {

                Point coords = ImageRecognition.ImageCoords("playHover.png");
                Mouse.Move(coords.X + (PixelCache.GetWidth("playHover.png") / 2), coords.Y + (PixelCache.GetHeight("playHover.png") / 2));
                Mouse.PressButton(Mouse.MouseKeys.Left, 150);
            }




        }

        //Wait for the client by detecting when the play button is displayed
        public void waitForConfirmButton()
        {

            bool normal = ImageRecognition.ImageExists("confirmNormal.png");
            bool hover = ImageRecognition.ImageExists("confirmHover.png");

            while (!normal && !hover)
            {
                normal = ImageRecognition.ImageExists("confirmNormal.png");
                hover = ImageRecognition.ImageExists("confirmHover.png");
                Thread.Sleep(1000);
            }

        }


        public void clickConfirmButton()
        {

            if (ImageRecognition.ImageExists("confirmNormal.png"))
            {

                Point coords = ImageRecognition.ImageCoords("confirmNormal.png");
                Mouse.Move(coords.X + (PixelCache.GetWidth("confirmNormal.png") / 2), coords.Y + (PixelCache.GetHeight("confirmNormal.png") / 2));
                Mouse.PressButton(Mouse.MouseKeys.Left, 150);

            }
            else if (ImageRecognition.ImageExists("confirmHover.png"))
            {

                Point coords = ImageRecognition.ImageCoords("confirmHover.png");
                Mouse.Move(coords.X + (PixelCache.GetWidth("confirmHover.png") / 2), coords.Y + (PixelCache.GetHeight("confirmHover.png") / 2));
                Mouse.PressButton(Mouse.MouseKeys.Left, 150);
            }




        }


        public bool getConfirmButton()
        {

            if (ImageRecognition.ImageExists("confirmNormal.png"))
            {

                return true;

            }
            else
            {

                if (ImageRecognition.ImageExists("confirmHover.png"))
                {

                    return true;

                }
                else
                {

                    return false;

                }

            }
        }




        public void clickGameMode(string type)
        {

            if (type == "summoners rift")
            {

                if (ImageRecognition.ImageExists("summonersRiftNormal.png", 10))
                {

                    Point coords = ImageRecognition.ImageCoords("summonersRiftNormal.png", 10);
                    Mouse.Move(coords.X + (PixelCache.GetWidth("summonersRiftNormal.png") / 2), coords.Y + (PixelCache.GetHeight("summonersRiftNormal.png") / 2));
                    Mouse.PressButton(Mouse.MouseKeys.Left, 150);

                }
                else
                {

                    bool exists = ImageRecognition.ImageExists("summonersRiftHover.png", 10);
                    while (!exists)
                    {
                        exists = ImageRecognition.ImageExists("summonersRiftHover.png", 10);
                        Thread.Sleep(1000);
                    }

                    Point coords = ImageRecognition.ImageCoords("summonersRiftHover.png", 10);
                    Mouse.Move(coords.X + (PixelCache.GetWidth("summonersRiftHover.png") / 2), coords.Y + (PixelCache.GetHeight("summonersRiftHover.png") / 2));
                    Mouse.PressButton(Mouse.MouseKeys.Left, 150);

                }

            }
            else if (type == "aram")
            {

                if (ImageRecognition.ImageExists("aramNormal.png", 10))
                {

                    Point coords = ImageRecognition.ImageCoords("aramNormal.png", 10);
                    Mouse.Move(coords.X + (PixelCache.GetWidth("aramNormal.png") / 2), coords.Y + (PixelCache.GetHeight("aramNormal.png") / 2));
                    Mouse.PressButton(Mouse.MouseKeys.Left, 150);

                }
                else
                {

                    bool exists = ImageRecognition.ImageExists("aramHover.png", 10);
                    while (!exists)
                    {
                        exists = ImageRecognition.ImageExists("aramHover.png", 10);
                        Thread.Sleep(1000);
                    }

                    Point coords = ImageRecognition.ImageCoords("aramHover.png", 10);
                    Mouse.Move(coords.X + (PixelCache.GetWidth("aramHover.png") / 2), coords.Y + (PixelCache.GetHeight("aramHover.png") / 2));
                    Mouse.PressButton(Mouse.MouseKeys.Left, 150);

                }

            }
            else if (type == "one for all")
            {

                if (ImageRecognition.ImageExists("oneForAllNormal.png", 10))
                {

                    Point coords = ImageRecognition.ImageCoords("oneForAllNormal.png", 10);
                    Mouse.Move(coords.X + (PixelCache.GetWidth("oneForAllNormal.png") / 2), coords.Y + (PixelCache.GetHeight("oneForAllNormal.png") / 2));
                    Mouse.PressButton(Mouse.MouseKeys.Left, 150);

                }
                else
                {

                    bool exists = ImageRecognition.ImageExists("oneForAllHover.png", 10);
                    while (!exists)
                    {
                        exists = ImageRecognition.ImageExists("oneForAllHover.png", 10);
                        Thread.Sleep(1000);
                    }

                    Point coords = ImageRecognition.ImageCoords("oneForAllHover.png", 10);
                    Mouse.Move(coords.X + (PixelCache.GetWidth("oneForAllHover.png") / 2), coords.Y + (PixelCache.GetHeight("oneForAllHover.png") / 2));
                    Mouse.PressButton(Mouse.MouseKeys.Left, 150);

                }

            }
            else if (type == "teamfight tactics")
            {

                if (ImageRecognition.ImageExists("teamfightTacticsNormal.png", 10))
                {

                    Point coords = ImageRecognition.ImageCoords("teamfightTacticsNormal.png", 10);
                    Mouse.Move(coords.X + (PixelCache.GetWidth("teamfightTacticsNormal.png") / 2), coords.Y + (PixelCache.GetHeight("teamfightTacticsNormal.png") / 2));
                    Mouse.PressButton(Mouse.MouseKeys.Left, 150);

                }
                else
                {

                    bool exists = ImageRecognition.ImageExists("teamfightTacticsHover.png", 10);
                    while (!exists)
                    {
                        exists = ImageRecognition.ImageExists("teamfightTacticsHover.png", 10);
                        Thread.Sleep(1000);
                    }

                    Point coords = ImageRecognition.ImageCoords("teamfightTacticsHover.png", 10);
                    Mouse.Move(coords.X + (PixelCache.GetWidth("teamfightTacticsHover.png") / 2), coords.Y + (PixelCache.GetHeight("teamfightTacticsHover.png") / 2));
                    Mouse.PressButton(Mouse.MouseKeys.Left, 150);

                }

            }
            else
            {

                //Unknown game mode

            }

        }






    }
}
