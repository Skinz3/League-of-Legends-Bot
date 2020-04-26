using LeagueBot.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace LeagueBot.Img
{
    public class ImageOCR
    {
        private static TesseractEngine TesseractEngine;

        public static void Initialize()
        {
            Logger.Write("ImageOCR not implemented yet.", MessageState.WARNING);
        }
        public static string[] FindWords()
        {
            throw new NotImplementedException();
        }
        public static bool WordExists()
        {
            throw new NotImplementedException();
        }
    }
}
