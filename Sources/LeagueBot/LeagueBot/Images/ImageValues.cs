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

    //Use image recognition to find game values
    public class ImageValues
    {

        public static Point GetNearestMinion()
        {
            var position = ImageRecognition.FindImagePosition("Game/minionHealthBar.png");
            return position;
        }
        //Return health value percentage
        public static int Health()
        {
            //Get matching x pixels of the health bar
            int value = ImageRecognition.MatchingXPixels("Game/health.png", 15);

            //Get total pixels of health bar
            int total = PixelCache.GetWidth("Game/health.png");

            //Get percentage of 100
            return (int)Math.Round(100d * value / total);

        }


        //Return health value percentage
        public static int Mana()
        {
            //Get matching x pixels of the health bar
            int value = ImageRecognition.MatchingXPixels("Game/mana.png", 15);

            //Get total pixels of health bar
            int total = PixelCache.GetWidth("Game/mana.png");

            //Get percentage of 100
            return (int)Math.Round((double)(100 * value) / total);

        }




    }
}
