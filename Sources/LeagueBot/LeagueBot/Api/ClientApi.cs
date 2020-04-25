using InputManager;
using LeagueBot.Game.Enums;
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

namespace LeagueBot.Api
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
            GameType typeEnum = GameType.Unknown;

            bool result = Enum.TryParse<GameType>(type, out typeEnum);

            if (!result)
            {
                Logger.Write("Undefined game mode: " + type, MessageState.WARNING);
                return;
            }

            string buttonNormal = string.Empty;
            string buttonHighlight = string.Empty;

            switch (typeEnum)
            {
                case GameType.Aram:
                    buttonNormal = "aramNormal.png";
                    buttonHighlight = "aramHover.png";
                    break;
                case GameType.Normal:
                    buttonNormal = "summonersRiftNormal.png";
                    buttonHighlight = "summonersRiftHover.png";
                    break;

                default:
                    Logger.Write("Unhandled game mode " + type, MessageState.WARNING);
                    return;
            }

            if (ImageRecognition.ImageExists(buttonNormal, 10))
            {
                Point coords = ImageRecognition.ImageCoords(buttonNormal, 10);
                Mouse.Move(coords.X + (PixelCache.GetWidth(buttonNormal) / 2), coords.Y + (PixelCache.GetHeight(buttonNormal) / 2));
                Mouse.PressButton(Mouse.MouseKeys.Left, 150);
            }
            else
            {

                bool exists = ImageRecognition.ImageExists(buttonHighlight, 10);

                while (!exists)
                {
                    exists = ImageRecognition.ImageExists(buttonHighlight, 10);
                    Thread.Sleep(1000);
                }

                Point coords = ImageRecognition.ImageCoords(buttonHighlight, 10);
                Mouse.Move(coords.X + (PixelCache.GetWidth(buttonHighlight) / 2), coords.Y + (PixelCache.GetHeight(buttonHighlight) / 2));
                Mouse.PressButton(Mouse.MouseKeys.Left, 150);

            }

        }



    }
}
