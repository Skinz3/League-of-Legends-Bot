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
        public const string TESS_LANGUAGE = "eng2";

        private static TesseractEngine Engine;

        public static void Initialize()
        {
            Engine = new TesseractEngine(TESS_PATH, TESS_LANGUAGE);
        }

        public static void FindWords()
        {

            List< string >    Lines = new List<string>();
            List< Rectangle > Rects = new List<Rectangle>();
            
            Bitmap screenshot = PixelCache.GetScreenshot();

            var data = Engine.Process( screenshot );
            Rects = data.GetSegmentedRegions( PageIteratorLevel.TextLine );

            using( var iterator = data.GetIterator() )
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
                            
                                string word = iterator.GetText( PageIteratorLevel.Word ).Trim();
                                if( word != "" ) line = line + word + " ";
                            
                            } while (iterator.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                           
                            if( line != "" ) {

                               Lines.Add( line.ToUpper().Trim() );
                            
                            }
                            
                            line = "";

                        } while (iterator.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                    
                    } while (iterator.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                    
                } while (iterator.Next(PageIteratorLevel.Block));
                
            }
            
            data.Dispose();

        }





        public static bool WordExists()
        {
            throw new NotImplementedException();
        }
    }
}
