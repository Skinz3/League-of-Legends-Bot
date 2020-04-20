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

    //Use image recognition to find game values
    public class ImageValues
    {
        
		
		//Return health value percentage
		public static int Health()
        { 
            //Get matching x pixels of the health bar
            int value = ImageRecognition.MatchingXPixels( "health.png" );
            
            //Get total pixels of health bar
            int total = ImageCache.GetBitmapWidth( "health.png" );

            //Get percentage of 100
            return (int) Math.Round( ( double ) ( 100 * value ) / total );

        }


        //Return health value percentage
		public static int Mana()
        { 
            //Get matching x pixels of the health bar
            int value = ImageRecognition.MatchingXPixels( "mana.png" );
            
            //Get total pixels of health bar
            int total = ImageCache.GetBitmapWidth( "mana.png" );

            //Get percentage of 100
            return (int) Math.Round( ( double ) ( 100 * value ) / total );

        }
		

		
		
	}
}
