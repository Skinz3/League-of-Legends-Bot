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
        /*//Return health value percentage
        public static int Health()
        {
            //Get matching x pixels of the health bar
            int value = ImageRecognition.MatchingXPixels("Game/health.png", 40);

            //Get total pixels of health bar
            int total = PixelCache.GetWidth("Game/health.png");

            //Get percentage of 100
            return (int)Math.Round(100d * value / total);

        }*/

        public static int EnemyCreepHealth()
        {
            //Get matching x pixels of the health bar
            int value = ImageRecognition.MatchingXPixels("Game/enemycreephealth.png", 3);

            //Get total pixels of health bar
            int total = PixelCache.GetWidth("Game/enemycreephealth.png");

            //Get percentage of 100
            return (int)Math.Round(100d * value / total);
        }
        public static int AllyCreepHealth()
        {
            //Get matching x pixels of the health bar
            int value = ImageRecognition.MatchingXPixels("Game/allycreephealth.png", 3);

            //Get total pixels of health bar
            int total = PixelCache.GetWidth("Game/allycreephealth.png");

            //Get percentage of 100
            return (int)Math.Round(100d * value / total);
        }
        /*public static Point characterLeveled()
        {
            Point position = ImageRecognition.FindImagePosition("Game/levelup.png", 5);
            return position;
        }*/
        public static Point AllyCreepPosition()
        {
            Point position = ImageRecognition.FindImagePosition("Game/allycreephealth.png", 3);
            return position;
        }
        public static Point EnemyCreepPosition()
        {
            Point position = ImageRecognition.FindImagePositionNearest("Game/enemycreephealth.png", 3);
            return position;
        }

        public static Point EnemyTowerStructure()
        {
            Point position = ImageRecognition.FindImagePosition("Game/towerstructure.png", 3);
            return position;
        }
        public static Point EnemyTowerStructure2()
        {
            Point position = ImageRecognition.FindImagePosition("Game/towerstructure2.png", 3);
            return position;
        }
        public static Point EnemyTowerStructure3()
        {
            Point position = ImageRecognition.FindImagePosition("Game/towerstructure3.png", 3);
            return position;
        }
        public static Point EnemyTowerStructure4()
        {
            Point position = ImageRecognition.FindImagePosition("Game/towerstructure4.png", 3);
            return position;
        }

        /*//Return mana value percentage
        public static int Mana()
        {
            //Get matching x pixels of the health bar
            int value = ImageRecognition.MatchingXPixels("Game/mana.png", 40);

            //Get total pixels of health bar
            int total = PixelCache.GetWidth("Game/mana.png");

            //Get percentage of 100
            return (int)Math.Round((double)(100 * value) / total);

        }*/

        internal static Point EnemyChampion()
        {
            Point position = ImageRecognition.FindImagePositionNearest("Game/enemycharacter.png", 4);
            return position;
        }
        internal static Point EnemyTowerHp()
        {
            Point position = ImageRecognition.FindImagePositionNearest("Game/enemytowerhp.png", 4);
            return position;
        }

        internal static Point AllyChampion()
        {
            Point position = ImageRecognition.FindImagePositionNearest("Game/allycharacter.png", 4);
            return position;
        }
        internal static Point botChampion()
        {
            Point position = ImageRecognition.FindImagePositionNearest("Game/botcharacter.png", 4);
            return position;
        }

        internal static Point EnemyTowerHp2()
        {
            Point position = ImageRecognition.FindImagePositionNearest("Game/enemytowerhp2.png", 4);
            return position;
        }
    }
}
