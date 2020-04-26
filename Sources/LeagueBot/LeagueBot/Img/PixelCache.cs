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
    public class PixelCache
    {
        public const string SCREENSHOT_IMAGE_NAME = "screenshot";

        public const string PATH = "Images/";

        public const int STEP = 250;

        public static string[] EXTENSIONS = new string[]
        {
            ".jpg",
            ".png"
        };

        private static Dictionary<string, int[]> ImagePixels = new Dictionary<string, int[]>();
        private static Dictionary<string, int> ImageHeigth = new Dictionary<string, int>();
        private static Dictionary<string, int> ImageWidth = new Dictionary<string, int>();

        private static Bitmap CurrentScreenshot;
    
        public static void Initialize()
        {
            string dir = Path.Combine(Environment.CurrentDirectory, PATH);

            foreach (var file in Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories))
            {
                if (EXTENSIONS.Contains(Path.GetExtension(file)))
                {
                    string key = file.Replace(dir, string.Empty);

                    Bitmap image = (Bitmap)Bitmap.FromFile(file);
                    ImagePixels.Add(key, ConvertImage(image));
                    ImageHeigth.Add(key, image.Height);
                    ImageWidth.Add(key, image.Width);

                    image.Dispose();
                }

            }

        }
        public static bool Exists(string key)
        {
            return ImagePixels.ContainsKey(key);
        }

        public static int[] GetPixels(string filename)
        {
            if (filename == SCREENSHOT_IMAGE_NAME)
            {
                if (ImageHelper.ImageTimestampExpired(SCREENSHOT_IMAGE_NAME, STEP))
                {

                    //Clear image from memory
                    if( CurrentScreenshot != null ) CurrentScreenshot.Dispose();

                    //Get a screen capture
                    CurrentScreenshot = ImageHelper.TakeScreenCapture();

                    //Save the screenshot pixels
                    ImagePixels[SCREENSHOT_IMAGE_NAME] = ConvertImage(CurrentScreenshot);

                    //Set width and height
                    ImageWidth[SCREENSHOT_IMAGE_NAME] = CurrentScreenshot.Width;
                    ImageHeigth[SCREENSHOT_IMAGE_NAME] = CurrentScreenshot.Height;
                    
                    //Set new image screenshot time
                    ImageHelper.UpdateImageTimestamp(SCREENSHOT_IMAGE_NAME);

                }

            }

            return ImagePixels[filename];
        }


        public static int GetHeight(string filename)
        {
            return ImageHeigth[filename];
        }

        public static int GetWidth(string filename)
        {
            return ImageWidth[filename];
        }

        public static int GetLength(string filename)
        {
            return ImagePixels[filename].Length;

        }

        private static int[] ConvertImage(Bitmap image)
        {
            //Create a new canvas
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            //Get bitmap image data
            BitmapData imageData = image.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            //Get pointer 
            IntPtr ptr = imageData.Scan0;

            //Get bytes in image
            int bytes = imageData.Stride * image.Height;

            //Create array for RGB values
            byte[] rgbValues = new byte[bytes];

            //Copy array from the pointer to the RGB array
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            //Counter
            int count = 0;

            //End of row
            int stride = imageData.Stride;

            //Array of pixel values
            int[] pixels = new int[image.Width * image.Height];

            //Foreach pixel column
            for (int column = 0; column < imageData.Height; column++)
            {

                //Foreach pixel row
                for (int row = 0; row < imageData.Width; row++)
                {

                    //Convert the RGB value to hex and save in array
                    pixels[count] = (((rgbValues[(column * stride) + (row * 3) + 2]) & 0xff) << 16) + (((rgbValues[(column * stride) + (row * 3) + 1]) & 0xff) << 8) + ((rgbValues[(column * stride) + (row * 3)]) & 0xff);

                    //Increment our counter
                    count++;

                }

            }

            //Unlock the image data
            image.UnlockBits(imageData);

            //Return array of pixels in hex format
            return pixels;
        }
    }
}
