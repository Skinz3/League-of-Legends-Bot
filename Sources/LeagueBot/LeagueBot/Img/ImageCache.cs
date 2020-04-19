using LeagueBot.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Img
{
    public class ImageCache
    {
        public const string PATH = "Images/";

        public static string[] EXTENSIONS = new string[]
        {
            ".jpg",
            ".png"
        };

        private static Dictionary<string, Bitmap> Cache = new Dictionary<string, Bitmap>();

        public static void Initialize()
        {
            foreach (var file in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, PATH)))
            {
                if (EXTENSIONS.Contains(Path.GetExtension(file)))
                {
                    Cache.Add(Path.GetFileName(file), (Bitmap)Bitmap.FromFile(file));
                }
            }

            Logger.Write(Cache.Count + " image loaded.");
        }

        public static Bitmap GetBitmap(string filename)
        {
            return Cache[filename];
        }
    }
}
