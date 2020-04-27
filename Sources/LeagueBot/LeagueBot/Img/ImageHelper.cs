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


        public static Bitmap ScaleImage(Image image, Double scale)
        {

            var newWidth = Convert.ToInt32(image.Width * scale);
            var newHeight = Convert.ToInt32(image.Height * scale);


            var destRect = new Rectangle(0, 0, newWidth, newHeight);
            var destImage = new Bitmap(newWidth, newHeight);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.Tile);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Bitmap DesaturateImage(Bitmap original)
{
   //create a blank bitmap the same size as original
   Bitmap newBitmap = new Bitmap(original.Width, original.Height);

   //get a graphics object from the new image
   using(Graphics g = Graphics.FromImage(newBitmap)){

       //create the grayscale ColorMatrix
       ColorMatrix colorMatrix = new ColorMatrix(
          new float[][] 
          {
             new float[] {.3f, .3f, .3f, 0, 0},
             new float[] {.59f, .59f, .59f, 0, 0},
             new float[] {.11f, .11f, .11f, 0, 0},
             new float[] {0, 0, 0, 1, 0},
             new float[] {0, 0, 0, 0, 1}
          });

       //create some image attributes
       using(ImageAttributes attributes = new ImageAttributes()){

           //set the color matrix attribute
           attributes.SetColorMatrix(colorMatrix);

           //draw the original image on the new image
           //using the grayscale color matrix
           g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                       0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
       }
   }
   return newBitmap;
}



        public static Bitmap InvertImage( Bitmap original)
        {

           
            for (int y = 0; (y <= (original.Height - 1)); y++) {
                for (int x = 0; (x <= (original.Width - 1)); x++) {
                    Color inv = original.GetPixel(x, y);
                    inv = Color.FromArgb(255, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    original.SetPixel(x, y, inv);
                }
            }
            return original;


        }


        public static Bitmap ContrastImage(Bitmap bmp, int threshold)
{
          
            var contrast = Math.Pow((100.0 + threshold) / 100.0, 2);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++) 
                {
                    var oldColor = bmp.GetPixel(x, y);
                    var red = ((((oldColor.R / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
                    var green = ((((oldColor.G / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
                    var blue = ((((oldColor.B / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
                    if (red > 255) red = 255;
                    if (red < 0) red = 0;
                    if (green > 255) green = 255;
                    if (green < 0) green = 0;
                    if (blue > 255) blue = 255;
                    if (blue < 0) blue = 0;

                    var newColor = Color.FromArgb(oldColor.A, (int)red, (int)green, (int)blue);
                    bmp.SetPixel(x, y, newColor);
                }
            }
           
            return bmp;

        }




    }
}
