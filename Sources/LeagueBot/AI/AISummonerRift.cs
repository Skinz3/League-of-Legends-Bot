using InputManager;
using LeagueBot.Constants;
using LeagueBot.Patterns;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueBot.AI
{
    public class AISummonerRift : AbstractAI
    {

        public AISummonerRift(Bot bot, SummonersRiftPattern pattern) : base(bot, pattern)
        {

        }

        public override void Stop()
        {

        }

        public override void Start()
        {
            base.Start();
            Thread.Sleep(3000);
            Summoner.LockCamera();
            // BuyItems();
            Summoner.Move(PixelsConstants.BOTLANE_BOT);
            //   Bot.Summoner.Back();
            //   MaxQ();
            //   StartRandomizer();
            //      Thread.Sleep(150);
            //   Bot.Summoner.Q();
            Console.Read();


        }
     




       
    }
}
