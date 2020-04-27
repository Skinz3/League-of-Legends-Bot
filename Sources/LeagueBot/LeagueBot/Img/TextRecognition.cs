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

        private static Dictionary<string, Point> TextCache = new Dictionary<string, Point>();
        private static Dictionary<string, Point> PhraseCache = new Dictionary<string, Point>();


        private static TesseractEngine Engine;




        public static void Initialize()
        {
            Engine = new TesseractEngine(TESS_PATH, TESS_LANGUAGE);
        }




        public static Point TextCoords( string phrase )
        {

            if ( TextHelper.TextTimestampExpired( phrase, 3000 ) )
            {
                ReadText();
                TextHelper.UpdateTextTimestamp(phrase);
            }

            if( TextCache.ContainsKey( phrase ) ) return TextCache[ phrase ];
            if( PhraseCache.ContainsKey( phrase ) ) return PhraseCache[ phrase ];

            return new Point( 0, 0 );

        }


        public static bool TextExists( string phrase )
        {

            if ( TextHelper.TextTimestampExpired( phrase, 3000 ) )
            {
                ReadText();
                TextHelper.UpdateTextTimestamp(phrase);
            }

            if( TextCache.ContainsKey( phrase ))
            {

                 if (TextCache[ phrase ].X > 0 && TextCache[phrase].Y > 0) return true;

            }
              if( PhraseCache.ContainsKey( phrase ))
            {

                 if (PhraseCache[ phrase ].X > 0 && PhraseCache[phrase].Y > 0) return true;

            }


            return false;

        }





        public static void ReadText()
        {
            
            Console.WriteLine( "Reading text...");

            List< string >    WLines = new List<string>();
            List< Rectangle > WRects = new List<Rectangle>();
            
            List< string >    PLines = new List<string>();
            List< Rectangle > PRects = new List<Rectangle>();
            

            Bitmap screenshot = ImageHelper.InvertImage( 
                ImageHelper.ContrastImage(
                    ImageHelper.DesaturateImage( 
                        PixelCache.GetScreenshot() 
                    )
                , 25 )
            );


            var data = Engine.Process( screenshot, PageSegMode.SparseText
                );
            WRects = data.GetSegmentedRegions( PageIteratorLevel.Word );
            PRects = data.GetSegmentedRegions( PageIteratorLevel.TextLine );
               
            //Clear text coords only after we've done engine work
            TextCache.Clear();

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
                                if( word != "" ) {
                                    line = line + word + " ";
                                    WLines.Add( word.ToUpper().Trim());
                                    }
                            } while (iterator.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                           
                            if( line != "" ) {

                               PLines.Add( line.ToUpper().Trim() );
                            
                            }
                            
                            line = "";

                        } while (iterator.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                    
                    } while (iterator.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                    
                } while (iterator.Next(PageIteratorLevel.Block));
                
            }
            

            data.Dispose();
            screenshot.Dispose();
            

            for( int i = 0; i < WLines.Count; ++i)
            {
                if( !TextCache.ContainsKey( WLines[ i ] )) { 

                    TextCache.Add( 
                        WLines[ i ], 
                        new Point( 
                            WRects[ i ].X + ( WRects[ i ].Width / 2 ), 
                            WRects[ i ].Y + ( WRects[ i ].Height / 2 )  
                        )
                    );
                
                    }
            }

             for( int i = 0; i < PLines.Count; ++i)
            {
                if( !PhraseCache.ContainsKey( PLines[ i ] )) { 

                    PhraseCache.Add( 
                        PLines[ i ], 
                        new Point( 
                            PRects[ i ].X + ( PRects[ i ].Width / 2 ), 
                            PRects[ i ].Y + ( PRects[ i ].Height / 2 )  
                        )
                    );
                
                    }
            }

             Console.WriteLine( "Read items...");
        
        }





        public static bool WordExists()
        {
            throw new NotImplementedException();
        }
    }
}
