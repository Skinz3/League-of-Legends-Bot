using InputManager;
using LeagueBot.Game.Settings;
using LeagueBot.Image;
using LeagueBot.Utils;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public static bool ImageExist(string imagePath)
        {
            return ImageRecognition.ImageExists(imagePath);
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
        private static Bitmap GetScreenShot()
        {
            Bitmap result = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            {
                using (Graphics gfx = Graphics.FromImage(result))
                {
                    gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                }
            }
            return result;
        }
        private static Bitmap GetCapture()
        {
            return ApplicationCapture.CaptureApplication("League of Legends");
        }
        public static Point? GetColorPosition(Color color)
        {
            int searchValue = color.ToArgb();

            using (Bitmap bmp = GetCapture())
            {
                using (FastBitmap bitmap = new FastBitmap(bmp))
                {
                    bitmap.Lock();

                    for (int x = 0; x < bmp.Width; x++)
                    {
                        for (int y = 0; y < bmp.Height; y++)
                        {
                            if (searchValue == bitmap.GetPixelInt(x, y))
                            {
                                return new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (LeagueManager.LEAGUE_WIDTH / 2) + x, (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (LeagueManager.LEAGUE_HEIGTH / 2) + y);
                            }
                        }
                    }
                }
            }
            return null;
        }

    }
}
