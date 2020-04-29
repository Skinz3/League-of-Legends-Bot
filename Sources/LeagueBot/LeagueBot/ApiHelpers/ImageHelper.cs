using InputManager;
using LeagueBot.Image;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueBot.ApiHelpers
{
    class ImageHelper
    {
        public static void WaitForImage(string imagePath)
        {
            bool exists = ImageRecognition.ImageExists(imagePath);
            while (!exists)
            {
                exists = ImageRecognition.ImageExists(imagePath);
                Thread.Sleep(1000);
            }

        }
        public static void LeftClickImage(string image)
        {
            if (ImageRecognition.ImageExists(image))
            {
                Point coords = ImageRecognition.ImageCoords(image);

                Mouse.Move(coords.X, coords.Y);
                Mouse.PressButton(Mouse.MouseKeys.Left, 150);
            }

        }
        public static string GetColor(int x, int y)
        {
            return ColorTranslator.ToHtml(Color.FromArgb(Interop.GetPixelColor(new Point(x, y)).ToArgb()));
        }
        public static void WaitForColor(int x, int y, string colorHex)
        {
            Color color = Interop.GetPixelColor(new Point(x, y));

            while (ColorTranslator.ToHtml(Color.FromArgb(color.ToArgb())) != colorHex)
            {
                color = Interop.GetPixelColor(new Point(x, y));
                Thread.Sleep(1000);
            }
        }


    }
}
