using InputManager;
using LeagueBot.Img;
using LeagueBot.IO;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LeagueBot.Windows.Interop;

namespace LeagueBot.Patterns
{
    public class ClientApi
    {





        //Wait for the client by detecting when the play button is displayed
        public void waitForPlayButton()
        {

            bool normal = ImageRecognition.ImageExists("playNormal.png");
            bool hover = ImageRecognition.ImageExists("playHover.png");
        
            while (!normal && !hover)
            {
                normal = ImageRecognition.ImageExists("playNormal.png");
                hover = ImageRecognition.ImageExists("playHover.png");
                Thread.Sleep(1000);
            }

        }















    }
}
