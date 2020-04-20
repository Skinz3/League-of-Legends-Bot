using LeagueBot.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueBot.Img
{
    public class ImageHelper
    {
        private static Dictionary<string, long> ImageTimestamps = new Dictionary<string, long>();

        public static void UpdateImageTimestamp(string image)
        {
            ImageTimestamps[image] = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public static bool ImageTimestampExpired(string image, int step)
        {
            if (!ImageTimestamps.ContainsKey(image)) ImageTimestamps.Add(image, 0);

            if (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond > ImageTimestamps[image] + step)
            {

                return true;

            }

            return false;

        }

        public static Bitmap TakeScreenCapture()
        {

            //Create a new bitmap screen size
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);

            //Create a new Graphics object
            var gfx = Graphics.FromImage(image);

            //Copy the current screen
            gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

            //Dispose of gfx
            gfx.Dispose();

            return image;

        }

    }
}
