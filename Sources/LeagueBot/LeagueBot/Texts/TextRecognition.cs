using LeagueBot.DesignPattern;
using LeagueBot.Image;
using LeagueBot.IO;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace LeagueBot.Texts
{
    public class TextRecognition
    {
        public const string TESS_PATH = "tessdata/";
        public const string TESS_LANGUAGE = "eng";

        private static Dictionary<string, Point> TextCache = new Dictionary<string, Point>();
        private static Dictionary<string, Point> PhraseCache = new Dictionary<string, Point>();


        private static TesseractEngine Engine;

        [StartupInvoke("Text Recognition", StartupInvokePriority.FifthPass)]
        public static void Initialize()
        {
            Engine = new TesseractEngine(TESS_PATH, TESS_LANGUAGE);
        }

        public static Point TextCoords(string phrase)
        {

            if (TextUtils.TextTimestampExpired(phrase, 2000))
            {


                ReadText();
                TextUtils.UpdateTextTimestamp(phrase);
            }

            if (TextCache.ContainsKey(phrase)) return TextCache[phrase];
            if (PhraseCache.ContainsKey(phrase)) return PhraseCache[phrase];

            return new Point(0, 0);

        }

        public static bool TextExists2(Rectangle rectangle, string phrase)
        {
            Stopwatch st = Stopwatch.StartNew();

            Bitmap src = PixelCache.GetScreenshot();

            Bitmap target = new Bitmap(rectangle.Width, rectangle.Height);


            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                 rectangle,
                                 GraphicsUnit.Pixel);
            }

            target.Save("test.png");

            Page page = Engine.Process(target);

            string text = page.GetText();

            src.Dispose();

            target.Dispose();

            page.Dispose();

            return text.Contains(phrase);
        }
        public static bool TextExists(string phrase)
        {
            if (TextUtils.TextTimestampExpired(phrase, 2000))
            {
                ReadText();
                TextUtils.UpdateTextTimestamp(phrase);
            }

            if (TextCache.ContainsKey(phrase))
            {
                if (TextCache[phrase].X > 0 && TextCache[phrase].Y > 0) return true;

            }
            if (PhraseCache.ContainsKey(phrase))
            {

                if (PhraseCache[phrase].X > 0 && PhraseCache[phrase].Y > 0) return true;

            }


            return false;

        }
        public static void ReadText()
        {

            Console.WriteLine("Image Preprocessing...");

            List<string> WLines = new List<string>();
            List<Rectangle> WRects = new List<Rectangle>();

            List<string> PLines = new List<string>();
            List<Rectangle> PRects = new List<Rectangle>();

            //Bitmap screenshot = PixelCache.GetScreenshot();

            Bitmap screenshot =

                    ImageUtils.InvertImage(
                        ImageUtils.ContrastImage(
                          ImageUtils.DesaturateImage(
                            PixelCache.GetScreenshot()

                            )
                         , 25));

            Console.WriteLine("Engine Processing...");
            var data = Engine.Process(screenshot, PageSegMode.SparseText

                );
            WRects = data.GetSegmentedRegions(PageIteratorLevel.Word);
            PRects = data.GetSegmentedRegions(PageIteratorLevel.TextLine);

            //Clear text coords only after we've done engine work
            TextCache.Clear();
            PhraseCache.Clear();

            Console.WriteLine("Extracting Words...");

            using (var iterator = data.GetIterator())
            {

                string line = "";
                iterator.Begin();

                do
                {
                    do
                    {
                        do
                        {
                            do
                            {

                                string word = iterator.GetText(PageIteratorLevel.Word).Trim();
                                if (word != "")
                                {
                                    line = line + word + " ";
                                    WLines.Add(word.ToUpper().Trim());
                                }
                            } while (iterator.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));


                            if (line != "")
                            {

                                PLines.Add(line.ToUpper().Trim());

                            }

                            line = "";

                        } while (iterator.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));

                    } while (iterator.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));

                } while (iterator.Next(PageIteratorLevel.Block));

            }


            data.Dispose();
            screenshot.Dispose();

            Console.WriteLine("Saving results...");

            for (int i = 0; i < WLines.Count; ++i)
            {
                if (!TextCache.ContainsKey(WLines[i]))
                {


                    TextCache.Add(
                        WLines[i],
                        new Point(
                             Convert.ToInt32((WRects[i].X + (WRects[i].Width / 2)) * 1),
                           Convert.ToInt32((WRects[i].Y + (WRects[i].Height / 2)) * 1)

                        )
                    );

                }
            }

            for (int i = 0; i < PLines.Count; ++i)
            {
                if (!PhraseCache.ContainsKey(PLines[i]))
                {


                    PhraseCache.Add(
                        PLines[i],
                        new Point(
                           Convert.ToInt32((PRects[i].X + (PRects[i].Width / 2)) * 1),
                           Convert.ToInt32((PRects[i].Y + (PRects[i].Height / 2)) * 1)
                        )
                    );

                }
            }

            Console.WriteLine("Read Complete.");
            Console.WriteLine("-----------------------");

        }




    }
}
