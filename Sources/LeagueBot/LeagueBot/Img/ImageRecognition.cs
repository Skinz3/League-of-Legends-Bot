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
		
        //Find image coordinates on screen
        public static Point ImageCoords(string image)
        {
            //Return image coordinates
            return FindImagePosition( image );

        }
        
        //Does an image exist on our screen
        public static bool ImageExists(string image)
        {
			
            //Get coords of image on our screen
            Point coords = FindImagePosition( image );
            
            //If the image was found somewhere on screen, return true
            if (coords.X > 0 && coords.Y > 0) return true;

            //Image was not found
            return false;

        }

        //Get number of matching pixels in sequence of an image
        public static int MatchingXPixels(string image)
        {

            //Return the match count
            return MatchImageX( image );

        }
        






		
		//Match pixels of an image on an X plane 
		private static int MatchImageX(string filename)
		{
			
			//Get current screen and cached image
			Bitmap screen = CurrentScreen();
			Bitmap loaded = ImageCache.GetBitmap(filename); 
			
			//Convert images to pixel arrays
			int[] pixels = GetPixels( screen );
			int[] search = GetPixels( loaded );
			
			//Loop through each pixel in screenshot
			for( int key = 0; key < pixels.Length; ++key )
			{
				
				//If this pixel matches the first pixel in our image
				if( pixels[ key ] == search[ 0 ] )
				{
					
					//If the next 3 pixels also match
					if( pixels[ key + 1 ] == search[ 1 ] &&
						pixels[ key + 2 ] == search[ 2 ] &&
						pixels[ key + 3 ] == search[ 3 ] )
					{
						
						//Create the value var
						int value = 0;
						
						//Loop through each pixel in the first row of our loaded image
						for( int i = 0; i < loaded.Width; ++i )
						{
							
							//If this pixel matches, increment our value
							if( pixels[ key + i ] == search[ i ] ) value++;
							
						}

                        //Dispose of unndeeded objects
                        screen.Dispose();

						//Return the value
						return value;
						
					}
					
				}
				
			}
			
            //Dispose of unndeeded objects
            screen.Dispose();
 
            //No pixels matched
			return 0;
			
		}
		
        private static Point FindImagePosition(string filename)
        {
			
			//Get current screen and cached image
			Bitmap screen = CurrentScreen();
			Bitmap loaded = ImageCache.GetBitmap(filename); 
			
			//Convert images to pixel arrays
            int[] pixels = GetPixels(screen);
            int[] search = GetPixels(loaded);
			
			//Set X and Y pointer for search
            int x = 1;
            int y = 1;

			//Loop through each pixel in screenshot
            for( int key = 0; key < pixels.Length; ++key )
            {
				
                //If this pixel matches the first pixel in our image
                if (pixels[key] == search[0])
                {
					
                    //If the next 3 X pixels also match our image
                    if( pixels[ key + 1 ] == search[ 1 ] &&
                        pixels[ key + 2 ] == search[ 2 ] &&
                        pixels[ key + 3 ] == search[ 3 ] )
                    {

                        //Next we'll check the next 3 Y pixels match our image 
                        if( pixels[ key + screen.Width ] == search[ loaded.Width ] &&
                            pixels[ key + ( screen.Width * 2 ) ] == search[ ( loaded.Width * 2 ) ] &&
                            pixels[ key + ( screen.Width * 3 ) ] == search[ ( loaded.Width * 3 ) ] )
                        {

                            //Finally, we will match the four center pixels of the image, to ensure this really is what we are looking for
                            if( pixels[ key + ( screen.Width * ( loaded.Height / 2 ) ) + ( loaded.Width / 2 ) ] == search[ ( loaded.Width * ( loaded.Height / 2 ) + ( loaded.Width / 2 ) ) ] &&
                                pixels[ key + ( screen.Width * ( ( loaded.Height / 2 ) + 1 ) ) + ( loaded.Width / 2 ) ] == search[ ( loaded.Width * ( ( loaded.Height / 2 ) + 1 ) + ( loaded.Width / 2 ) ) ] &&
                                pixels[ key + ( screen.Width * ( loaded.Height / 2 ) ) + ( loaded.Width / 2 ) + 1 ] == search[ ( loaded.Width * ( loaded.Height / 2 ) + ( loaded.Width / 2 ) ) + 1 ] &&
                                pixels[ key + ( screen.Width * ( ( loaded.Height / 2 ) + 1 ) ) + ( loaded.Width / 2 ) + 1 ] == search[ ( loaded.Width * ( ( loaded.Height / 2 ) + 1 ) + ( loaded.Width / 2 ) ) + 1 ] )
                            {
								
                                //Dispose of unndeeded objects
                                screen.Dispose();

								//Return the coordinates of this image
                                return new Point(x, y);

                            }

                        }

                    }

                }
				
				//If we are at the edge of the screen
                if( x == screen.Width )
                {	
					
					//Increment the row
                    y++;
					
					//Reset the X position
                    x = 0;
                }
				
				//Increment the X position
                x++;

            }
			
            //Dispose of unndeeded objects
            screen.Dispose();

			//No image detected, return 0,0
            return new Point(0, 0);

        }
		
        //Return array of hex colour values of each pixel in an image
        private static int[] GetPixels(Bitmap image)
        {

            //Create a new canvas
            Rectangle rect = new Rectangle( 0, 0, image.Width, image.Height );

            //Get bitmap image data
            BitmapData imageData = image.LockBits( rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb );
            
            //Get pointer 
            IntPtr ptr = imageData.Scan0;

            //Get bytes in image
            int bytes = imageData.Stride * image.Height;
            
            //Create array for RGB values
            byte[] rgbValues = new byte[ bytes ];

            //Copy array from the pointer to the RGB array
            Marshal.Copy( ptr, rgbValues, 0, bytes );

            //Counter
            int count = 0;

            //End of row
            int stride = imageData.Stride;

            //Array of pixel values
            int[] pixels = new int[ image.Width * image.Height ];

            //Foreach pixel column
            for( int column = 0; column < imageData.Height; column++ )
            {
                
                //Foreach pixel row
                for( int row = 0; row < imageData.Width; row++ )
                {
                    
                    //Convert the RGB value to hex and save in array
                    pixels[ count ] = ( ( ( rgbValues[ ( column * stride ) + ( row * 3 ) + 2 ] ) & 0xff ) << 16 ) + ( ( ( rgbValues[ ( column * stride ) + ( row * 3 ) + 1 ] ) & 0xff ) << 8 ) + ( ( rgbValues[ ( column * stride ) + ( row * 3 ) ] ) & 0xff );
                    
                    //Increment our counter
                    count++;

                }

            }

            //Unlock the image data
            image.UnlockBits( imageData );

            //Return array of pixels in hex format
            return pixels;

        }





        //Capture current screen as Bitmap
        private static Bitmap CurrentScreen()
        {

            //Create a new bitmap screen size
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            
            //Create a new Graphics object
            var gfx = Graphics.FromImage(image);
            
            //Copy the current screen
            gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            
            //Dispose of gfx
            gfx.Dispose();

            //Return image as bitmap
            return image; 
        
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
