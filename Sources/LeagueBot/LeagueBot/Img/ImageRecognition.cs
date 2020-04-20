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

namespace LeagueBot.Img
{
    public class ImageRecognition
    {

        public const int STEP = 250;


        private static Dictionary<string, int> ImageXMatches = new Dictionary<string, int>();
        private static Dictionary<string, Point> ImagePositionMatches = new Dictionary<string, Point>();


        //Find image coordinates on screen
        public static Point ImageCoords(string image)
        {

            if (ImageHelper.ImageTimestampExpired(image, STEP))
            {

                ImagePositionMatches[image] = FindImagePosition(image);
                ImageHelper.UpdateImageTimestamp(image);

            }

            //Return image coordinates
            return ImagePositionMatches[image];

        }

        //Does an image exist on our screen
        public static bool ImageExists(string image)
        {

            if (ImageHelper.ImageTimestampExpired(image, STEP))
            {

                ImagePositionMatches[image] = FindImagePosition(image);
                ImageHelper.UpdateImageTimestamp(image);
            }

            //Get coords of image on our screen
            Point coords = ImagePositionMatches[image];

            //If the image was found somewhere on screen, return true
            if (coords.X > 0 && coords.Y > 0) return true;

            //Image was not found
            return false;

        }

        //Get number of matching pixels in sequence of an image
        public static int MatchingXPixels(string image, int resolution = 3)
        {

            if (ImageHelper.ImageTimestampExpired(image, STEP))
            {

                ImageXMatches[image] = MatchImageX(image, resolution);
                ImageHelper.UpdateImageTimestamp(image);
            }

            //Return the match count
            return ImageXMatches[image];

        }








        //Match pixels of an image on an X plane 
        private static int MatchImageX(string filename, int resolution = 3)
        {

            //Convert images to pixel arrays
            int[] pixels = PixelCache.GetPixels(PixelCache.SCREENSHOT_IMAGE_NAME);
            int[] search = PixelCache.GetPixels(filename);

            //Loop through each pixel in screenshot
            for (int key = 0; key < pixels.Length; ++key)
            {

                //If this pixel matches the first pixel in our image
                if (pixels[key] == search[0])
                {

                    //Create a matched variable
                    bool matched = true;

                    //Foreach pixel in our resolution
                    for (int i = 1; i < resolution; ++i)
                    {

                        //If the pixel does not match, unset the matched variable
                        if (pixels[key + i] != search[i]) matched = false;

                    }

                    //If all pixels in resolution matched
                    if (matched)
                    {

                        //Create the value var
                        int value = 0;

                        //Loop through each pixel in the first row of our loaded image
                        for (int i = 0; i < PixelCache.GetWidth(filename); ++i)
                        {

                            //If this pixel matches, increment our value
                            if (pixels[key + i] == search[i]) value++;

                        }

                        //Return the value
                        return value;

                    }

                }

            }

            //No pixels matched
            return 0;

        }


        private static Point FindImagePosition(string filename)
        {

            //Convert images to pixel arrays
            int[] pixels = PixelCache.GetPixels(PixelCache.SCREENSHOT_IMAGE_NAME);
            int[] search = PixelCache.GetPixels(filename);

            ;

            //Set X and Y pointer for search
            int x = 1;
            int y = 1;

            //Loop through each pixel in screenshot
            for (int key = 0; key < pixels.Length; ++key)
            {

                //If this pixel matches the first pixel in our image
                if (pixels[key] == search[0])
                {

                    //If the next 3 X pixels also match our image
                    if (pixels[key + 1] == search[1] &&
                        pixels[key + 2] == search[2] &&
                        pixels[key + 3] == search[3])
                    {

                        //Next we'll check the next 3 Y pixels match our image 
                        if (pixels[key + PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME)] == search[PixelCache.GetWidth(filename)] &&
                            pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * 2)] == search[(PixelCache.GetWidth(filename) * 2)] &&
                            pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * 3)] == search[(PixelCache.GetWidth(filename) * 3)])
                        {


                            //Finally, we will match the four center pixels of the image, to ensure this really is what we are looking for
                            if (pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * (PixelCache.GetHeight(filename) / 2)) + (PixelCache.GetWidth(filename) / 2)] == search[(PixelCache.GetWidth(filename) * (PixelCache.GetWidth(filename) / 2) + (PixelCache.GetWidth(filename) / 2))] &&
                                pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * ((PixelCache.GetHeight(filename) / 2) + 1)) + (PixelCache.GetWidth(filename) / 2)] == search[(PixelCache.GetWidth(filename) * ((PixelCache.GetWidth(filename) / 2) + 1) + (PixelCache.GetWidth(filename) / 2))] &&
                                pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * (PixelCache.GetHeight(filename) / 2)) + (PixelCache.GetWidth(filename) / 2) + 1] == search[(PixelCache.GetWidth(filename) * (PixelCache.GetWidth(filename) / 2) + (PixelCache.GetWidth(filename) / 2)) + 1] &&
                                pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * ((PixelCache.GetHeight(filename) / 2) + 1)) + (PixelCache.GetWidth(filename) / 2) + 1] == search[(PixelCache.GetWidth(filename) * ((PixelCache.GetWidth(filename) / 2) + 1) + (PixelCache.GetWidth(filename) / 2)) + 1])
                            {

                                //Return the coordinates of this image
                                return new Point(x, y);

                            }

                        }

                    }

                }

                //If we are at the edge of the screen
                if (x == PixelCache.GetWidth("screenshot"))
                {

                    //Increment the row
                    y++;

                    //Reset the X position
                    x = 0;
                }

                //Increment the X position
                x++;

            }

            //No image detected, return 0,0
            return new Point(0, 0);

        }





        //FIX ME, downsize images correctly for pixel to pixel matching
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
