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

namespace LeagueBot.Image
{
    public class ImageRecognition
    {

        public const int STEP = 250;


        private static Dictionary<string, int> ImageXMatches = new Dictionary<string, int>();
        private static Dictionary<string, Point> ImagePositionMatches = new Dictionary<string, Point>();


        //Find image coordinates on screen
        public static Point ImageCoords(string image, int resolution = 3 )
        {

            if (ImageUtils.ImageTimestampExpired(image, STEP))
            {

                ImagePositionMatches[image] = FindImagePosition(image, resolution);
                ImageUtils.UpdateImageTimestamp(image);

            }

            //Return image coordinates
            return ImagePositionMatches[image];

        }

        //Does an image exist on our screen
        public static bool ImageExists(string image, int resolution = 3 )
        {
            if (ImageUtils.ImageTimestampExpired(image, STEP))
            {

                ImagePositionMatches[image] = FindImagePosition(image, resolution);
                ImageUtils.UpdateImageTimestamp(image);
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

            if (ImageUtils.ImageTimestampExpired(image, STEP))
            {

                ImageXMatches[image] = MatchImageX(image, resolution);
                ImageUtils.UpdateImageTimestamp(image);
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

        public static Point FindImagePositionNearest(string filename, int resolution = 3)
        {

            //Convert images to pixel arrays
            int[] pixels = PixelCache.GetPixels(PixelCache.SCREENSHOT_IMAGE_NAME);
            int[] search = PixelCache.GetPixels(filename);



            //Set X and Y pointer for search
            int x = 1;
            int y = 1;
            Point nearestPixelPosition = new Point(0, 0);


            //Loop through each pixel in screenshot
            for (int key = 0; key < pixels.Length; ++key)
            {

                //If this pixel matches the first pixel in our image
                if (pixels[key] == search[0])
                {

                    //Create a matched variable
                    bool matched = true;

                    //Foreach X pixel in our resolution
                    for (int i = 1; i < resolution; ++i)
                    {

                        //If the pixel does not match, unset the matched variable
                        if (pixels[key + i] != search[i]) matched = false;

                    }

                    //If the first resolution is matched, move on
                    if (matched)
                    {
                        //Foreach Y pixel in our resolution
                        for (int i = 1; i < resolution; ++i)
                        {
                            //If the pixel does not match, unset the matched variable
                            try
                            {
                                if (pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * i)] != search[(PixelCache.GetWidth(filename) * i)]) matched = false;
                            }
                            catch
                            {
                                continue;
                            }

                        }

                        //If we have matched the resoltion again, move on
                        if (matched)
                        {

                            //The starting pointer ( end of the iamge on screen )
                            int start = (key) + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * (PixelCache.GetHeight(filename) - 1)) + PixelCache.GetWidth(filename);


                            //Increment the amount of pixels to remove
                            for (int i = 1; i < resolution; ++i)
                            {

                                //If the pixel does not match, unset the matched variable
                                if (pixels[start - i] != search[(PixelCache.GetWidth(filename) * PixelCache.GetHeight(filename)) - i]) matched = false;

                            }

                            //Did we match the last X pixels of our image?
                            if (matched)
                            {

                                //Finally, we will match the four center pixels of the image, to ensure this really is what we are looking for
                                try
                                {
                                    if (pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * (PixelCache.GetHeight(filename) / 2)) + (PixelCache.GetWidth(filename) / 2)] == search[(PixelCache.GetWidth(filename) * (PixelCache.GetHeight(filename) / 2) + (PixelCache.GetWidth(filename) / 2))] &&
                                    pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * ((PixelCache.GetHeight(filename) / 2) + 1)) + (PixelCache.GetWidth(filename) / 2)] == search[(PixelCache.GetWidth(filename) * ((PixelCache.GetHeight(filename) / 2) + 1) + (PixelCache.GetWidth(filename) / 2))] &&
                                    pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * (PixelCache.GetHeight(filename) / 2)) + (PixelCache.GetWidth(filename) / 2) + 1] == search[(PixelCache.GetWidth(filename) * (PixelCache.GetHeight(filename) / 2) + (PixelCache.GetWidth(filename) / 2)) + 1] &&
                                    pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * ((PixelCache.GetHeight(filename) / 2) + 1)) + (PixelCache.GetWidth(filename) / 2) + 1] == search[(PixelCache.GetWidth(filename) * ((PixelCache.GetHeight(filename) / 2) + 1) + (PixelCache.GetWidth(filename) / 2)) + 1])
                                    {

                                        //Return the coordinates of this image
                                        //return new Point(x, y);
                                        nearestPixelPosition.X = x;
                                        nearestPixelPosition.Y = y;

                                    }
                                }
                                catch
                                {

                                    //return new Point(x, y);
                                    nearestPixelPosition.X = x;
                                    nearestPixelPosition.Y = y;

                                }

                            }

                        }

                    }

                }

                //If we are at the edge of the screen
                if (x == PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME))
                {

                    //Increment the row
                    y++;

                    //Reset the X position
                    x = 0;
                }

                //Increment the X position
                x++;

            }

            return nearestPixelPosition;

        }


    //Find X/Y coords of an image on screen
    public static Point FindImagePosition(string filename, int resolution = 3 )
        {

            //Convert images to pixel arrays
            int[] pixels = PixelCache.GetPixels(PixelCache.SCREENSHOT_IMAGE_NAME);
            int[] search = PixelCache.GetPixels(filename);

         

            //Set X and Y pointer for search
            int x = 1;
            int y = 1;

            //Loop through each pixel in screenshot
            for (int key = 0; key < pixels.Length; ++key)
            {

                //If this pixel matches the first pixel in our image
                if (pixels[key] == search[0])
                {
                    
                     //Create a matched variable
                     bool matched = true;

                    //Foreach X pixel in our resolution
                    for (int i = 1; i < resolution; ++i)
                    {

                        //If the pixel does not match, unset the matched variable
                        if (pixels[key + i] != search[i]) matched = false;

                    }
                    
                    //If the first resolution is matched, move on
                    if( matched )
                    {
                        //Foreach Y pixel in our resolution
                        for (int i = 1; i < resolution; ++i)
                        {
                            //If the pixel does not match, unset the matched variable
                            if (pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * i)] != search[(PixelCache.GetWidth(filename) * i)]) matched = false;

                        }

                        //If we have matched the resoltion again, move on
                        if( matched )
                        {

                            //The starting pointer ( end of the iamge on screen )
                            int start = ( key ) + ( PixelCache.GetWidth( PixelCache.SCREENSHOT_IMAGE_NAME ) * ( PixelCache.GetHeight( filename ) - 1 ) ) + PixelCache.GetWidth( filename );
                            

                            //Increment the amount of pixels to remove
                            for( int i = 1; i < resolution; ++i)
                            {
                                
                                 //If the pixel does not match, unset the matched variable
                                 if( pixels[ start - i ] != search[ ( PixelCache.GetWidth( filename ) * PixelCache.GetHeight( filename ) ) - i ] ) matched = false;

                            }

                            //Did we match the last X pixels of our image?
                            if( matched)
                            {

                                //Finally, we will match the four center pixels of the image, to ensure this really is what we are looking for
                                if( pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * (PixelCache.GetHeight(filename) / 2)) + (PixelCache.GetWidth(filename) / 2)] == search[(PixelCache.GetWidth(filename) * (PixelCache.GetHeight(filename) / 2) + (PixelCache.GetWidth(filename) / 2))] &&
                                    pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * ((PixelCache.GetHeight(filename) / 2) + 1)) + (PixelCache.GetWidth(filename) / 2)] == search[(PixelCache.GetWidth(filename) * ((PixelCache.GetHeight(filename) / 2) + 1) + (PixelCache.GetWidth(filename) / 2))] &&
                                    pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * (PixelCache.GetHeight(filename) / 2)) + (PixelCache.GetWidth(filename) / 2) + 1] == search[(PixelCache.GetWidth(filename) * (PixelCache.GetHeight(filename) / 2) + (PixelCache.GetWidth(filename) / 2)) + 1] &&
                                    pixels[key + (PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME) * ((PixelCache.GetHeight(filename) / 2) + 1)) + (PixelCache.GetWidth(filename) / 2) + 1] == search[(PixelCache.GetWidth(filename) * ((PixelCache.GetHeight(filename) / 2) + 1) + (PixelCache.GetWidth(filename) / 2)) + 1])
                                {

                                    //Return the coordinates of this image
                                    return new Point(x, y);

                                }

                            }
                            
                        }

                    }
                    
                }

                //If we are at the edge of the screen
                if (x == PixelCache.GetWidth(PixelCache.SCREENSHOT_IMAGE_NAME))
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
        
    }
}
