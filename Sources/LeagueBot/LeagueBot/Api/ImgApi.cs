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
    public class ImgApi
    {
        private WinApi WinApi
        {
            get;
            set;
        }

        public ImgApi(WinApi winApi)
        {
            this.WinApi = winApi;
        }
        /* 
         * Wait for the client by detecting when the button is displayed
         */
        public void waitForButton(string normalImage,string hoverImage)
        {
            bool normal = ImageRecognition.ImageExists(normalImage);
            bool hover = ImageRecognition.ImageExists(normalImage);

            while (!normal && !hover)
            {
                normal = ImageRecognition.ImageExists(normalImage);
                hover = ImageRecognition.ImageExists(hoverImage);
                Thread.Sleep(1000);
            }
        }


        public void leftClickButton(string normalImage,string hoverImage)
        {
            if (ImageRecognition.ImageExists(normalImage))
            {

                Point coords = ImageRecognition.ImageCoords(normalImage);
                Mouse.Move(coords.X + (PixelCache.GetWidth(normalImage) / 2), coords.Y + (PixelCache.GetHeight(hoverImage) / 2));
                Mouse.PressButton(Mouse.MouseKeys.Left, 150);

            }
            else if (ImageRecognition.ImageExists(hoverImage))
            {

                Point coords = ImageRecognition.ImageCoords(hoverImage);
                Mouse.Move(coords.X + (PixelCache.GetWidth(hoverImage) / 2), coords.Y + (PixelCache.GetHeight(hoverImage) / 2));
                Mouse.PressButton(Mouse.MouseKeys.Left, 150);
            }
        }
        public void waitForImage(string image)
        {
            bool exists = ImageRecognition.ImageExists(image);
            while (!exists)
            {
                exists = ImageRecognition.ImageExists(image);
                Thread.Sleep(1000);
            }

        }
        public void leftClickImage(string image)
        {
            if (ImageRecognition.ImageExists(image))
            {
                Point coords = ImageRecognition.ImageCoords(image);

                Mouse.Move(coords.X, coords.Y);
                Mouse.PressButton(Mouse.MouseKeys.Left, 150);
            }

        }


    }
}
