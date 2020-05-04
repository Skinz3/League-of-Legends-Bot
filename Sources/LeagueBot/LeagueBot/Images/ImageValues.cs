using LeagueBot.ApiHelpers;
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
        //Return health value percentage
        public static int Health()
        {
            //Get matching x pixels of the health bar
            int value = ImageRecognition.MatchingXPixels("Game/health.png", 40);

            //Get total pixels of health bar
            int total = PixelCache.GetWidth("Game/health.png");

            //Get percentage of 100
            return (int)Math.Round(100d * value / total);

        }

        public static int EnemyCreepHealth()
        {
            //Get matching x pixels of the health bar
            int value = ImageRecognition.MatchingXPixels("Game/enemycreephealth.png", 4);

            //Get total pixels of health bar
            int total = PixelCache.GetWidth("Game/enemycreephealth.png");

            //Get percentage of 100
            return (int)Math.Round(100d * value / total);
        }
        public static int AllyCreepHealth()
        {
            //Get matching x pixels of the health bar
            int value = ImageRecognition.MatchingXPixels("Game/allycreephealth.png", 4);

            //Get total pixels of health bar
            int total = PixelCache.GetWidth("Game/allycreephealth.png");

            //Get percentage of 100
            return (int)Math.Round(100d * value / total);
        }
        public static Point AllyCreepPosition()
        {
            Point position = ImageRecognition.FindImagePosition("Game/allycreephealth.png", 4);
            return position;
        }
        public static Point EnemyCreepPosition()
        {
            Point position = ImageRecognition.FindImagePosition("Game/enemycreephealth.png", 4);
            return position;
        }

        public static Point EnemyTowerStructure()
        {
            Point position = ImageRecognition.FindImagePosition("Game/towerstructure.png", 4);
            return position;
        }
        public static Point EnemyTowerStructure2()
        {
            Point position = ImageRecognition.FindImagePosition("Game/towerstructure2.png", 4);
            return position;
        }
        public static Point EnemyTowerStructure3()
        {
            Point position = ImageRecognition.FindImagePosition("Game/towerstructure3.png", 4);
            return position;
        }
        public static Point EnemyTowerStructure4()
        {
            Point position = ImageRecognition.FindImagePosition("Game/towerstructure4.png", 4);
            return position;
        }

        //Return health value percentage
        public static int Mana()
        {
            //Get matching x pixels of the health bar
            int value = ImageRecognition.MatchingXPixels("Game/mana.png", 40);

            //Get total pixels of health bar
            int total = PixelCache.GetWidth("Game/mana.png");

            //Get percentage of 100
            return (int)Math.Round((double)(100 * value) / total);

        }

        internal static Point EnemyChampion()
        {
            Point position = ImageRecognition.FindImagePosition("Game/enemycharacter.png", 4);
            return position;
        }
    }
}
