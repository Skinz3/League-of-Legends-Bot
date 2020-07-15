using LeagueBot.Image;
using LeagueBot.Texts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueBot.ApiHelpers
{
    class TextHelper
    {
        public static void WaitForText(int x, int y, int width, int heigth, string text)
        {
            Rectangle rect = new Rectangle(x, y, width, heigth);

            bool exists = TextRecognition.TextExists2(rect, text);
            while (!exists)
            {
                BotHelper.Wait(1000);
                exists = TextRecognition.TextExists2(rect, text);
            }

        }
        public static bool TextExists(int x, int y, int width, int heigth, string text)
        {
            bool exist;
            try
            {
                exist = TextRecognition.TextExists2(new Rectangle(x, y, width, heigth), text);
            }
            catch
            {
                exist = false;
            }

            return exist;
        }

        internal static int GetTextFromImage(int x, int y, int width, int heigth)
        {
            Rectangle rect;
            try
            {
                rect = new Rectangle(x, y, width, heigth);
            }
            catch
            {
                return 0;
            }
            return TextRecognition.GetTextValue(rect);
        }
    }
}
