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
    public class TextHelper
    {
        private static Dictionary<string, long> TextTimestamps = new Dictionary<string, long>();

        public static void UpdateTextTimestamp(string phrase)
        {
            TextTimestamps[phrase] = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public static bool TextTimestampExpired(string phrase, int step)
        {
            if (!TextTimestamps.ContainsKey(phrase)) TextTimestamps.Add(phrase, 0);

            if (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond > TextTimestamps[phrase] + step)
            {

                return true;

            }

            return false;

        }

    }
}
