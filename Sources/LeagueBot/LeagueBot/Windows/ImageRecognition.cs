using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueBot.Windows
{
    /*
     * Thanks to Forerunner
     */
    public class ImageRecognition
    {
        public static Point ImageCoords(string image)
        {
            Bitmap screen = CurrentScreen();
            return FindImagePosition(screen, image);

        }

        public static bool ImageExists(string image)
        {
            Bitmap screen = CurrentScreen();
            Point coords = FindImagePosition(screen, image);

            if (coords.X > 0 && coords.Y > 0) return true;

            return false;

        }

        private static Point FindImagePosition(Bitmap image, string filename)
        {
            Bitmap loaded = ImageFromFile(filename);

            int[] screen = GetPixels(image);
            int[] find = GetPixels(loaded);

            int x = 1;
            int y = 1;


            for(int key = 0; key < screen.Length; ++key )
            {

                //Match 1st pixel
                if (screen[key] == find[0])
                {



                    //Match 4 X pixel
                    if (
                        screen[key + 1] == find[1] &&
                        screen[key + 2] == find[2] &&
                        screen[key + 3] == find[3]
                        )
                    {

                        //Match 4 Y pixel
                        if (

                            screen[key + image.Width] == find[loaded.Width] &&
                            screen[key + (image.Width * 2)] == find[(loaded.Width * 2)] &&
                            screen[key + (image.Width * 3)] == find[(loaded.Width * 3)]

                        )
                        {

                            //Match 4 center pixel
                            if (

                                screen[key + (image.Width * (loaded.Height / 2)) + (loaded.Width / 2)] == find[(loaded.Width * (loaded.Height / 2) + (loaded.Width / 2))] &&
                                screen[key + (image.Width * ((loaded.Height / 2) + 1)) + (loaded.Width / 2)] == find[(loaded.Width * ((loaded.Height / 2) + 1) + (loaded.Width / 2))] &&
                                screen[key + (image.Width * (loaded.Height / 2)) + (loaded.Width / 2) + 1] == find[(loaded.Width * (loaded.Height / 2) + (loaded.Width / 2)) + 1] &&
                                screen[key + (image.Width * ((loaded.Height / 2) + 1)) + (loaded.Width / 2) + 1] == find[(loaded.Width * ((loaded.Height / 2) + 1) + (loaded.Width / 2)) + 1]


                            )
                            {

                                return new Point(x, y);

                            }

                        }

                    }

                }

                if (x == image.Width)
                {
                    y++;
                    x = 0;
                }
                x++;

            }

            return new Point(0, 0);

        }






        private static Bitmap ImageFromFile(string path)
        {
            return new Bitmap("Images/" + path + ".png");

        }

        private static int[] GetPixels(Bitmap image)
        {
            int length = image.Width * image.Height;

            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            BitmapData imageData = image.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            IntPtr ptr = imageData.Scan0;

            int bytes = imageData.Stride * image.Height;
            byte[] rgbValues = new byte[bytes];

            Marshal.Copy(ptr, rgbValues, 0, bytes);

            int count = 0;
            int stride = imageData.Stride;

            int[] pixels = new int[length];

            for (int column = 0; column < imageData.Height; column++)
            {
                for (int row = 0; row < imageData.Width; row++)
                {
                    pixels[count] = (((rgbValues[(column * stride) + (row * 3) + 2]) & 0xff) << 16) + (((rgbValues[(column * stride) + (row * 3) + 1]) & 0xff) << 8) + ((rgbValues[(column * stride) + (row * 3)]) & 0xff);
                    count++;
                }
            }

            return pixels;

        }

        private static Bitmap CurrentScreen()
        {
            var image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            var gfx = Graphics.FromImage(image);
            gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            return image;//scaleImage( image, 0.8 );
        }

        private static Bitmap ScaleImage(Image image, Double scale)
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
    }
}
