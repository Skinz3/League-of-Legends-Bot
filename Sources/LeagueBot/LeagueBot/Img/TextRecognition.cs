using LeagueBot.IO;
using LeagueBot.Windows;
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
    public class TextRecognition
    {
        public const string TESS_PATH = "tessdata/";
        public const string TESS_LANGUAGE = "eng";

        private static TesseractEngine Engine;

        public static void Initialize()
        {
            Engine = new TesseractEngine(TESS_PATH, TESS_LANGUAGE);
        }
        public static void FindWords(string processName)
        {
            var bimap = ApplicationCapture.CaptureApplication(processName);
            var data = Engine.Process(bimap, PageSegMode.SingleWord);

        }
        public static bool WordExists()
        {
            throw new NotImplementedException();
        }
    }
}
