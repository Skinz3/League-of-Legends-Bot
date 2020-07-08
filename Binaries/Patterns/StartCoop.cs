
using System;
using System.Collections.Generic;
using System.Drawing;
using LeagueBot.Patterns;
using LeagueBot.ApiHelpers;

namespace LeagueBot
{
    public class StartCoop : PatternScript
    {
        private const string MODE = "intermediate";
        
        private Random RandomTextSender;
        
        private Random RandomChampion;

        public override void Execute()
        {
            bot.log("Waiting for league client process... Ensure League client window size is 1600x900");
            bot.waitProcessOpen(CLIENT_PROCESS_NAME);
            
            bot.bringProcessToFront(CLIENT_PROCESS_NAME);
            bot.waitUntilProcessBounds(CLIENT_PROCESS_NAME, 1600, 900);
            bot.centerProcess(CLIENT_PROCESS_NAME);

            bot.wait(5000);

            bot.bringProcessToFront(CLIENT_PROCESS_NAME);
            bot.waitUntilProcessBounds(CLIENT_PROCESS_NAME, 1600, 900);
            bot.centerProcess(CLIENT_PROCESS_NAME);

            //REDO

            bot.log("Client ready.");
            bot.wait(2000);
            client.clickPlayButton();
            bot.wait(2000);
            client.clickCoopvsIAText();
            bot.wait(2000);

            if (MODE == "intermediate")
            {
                client.clickIntermediateText();
            }
            else if (MODE == "intro")
            {
                client.clickIntroText();
            }


            bot.wait(2000);
            client.clickConfirmButton();

            bot.wait(5000);

            client.clickFindMatchButton();

            bot.log("Finding match...");

            while (client.mustSelectChamp() == false)
            {
                client.acceptMatch();
                bot.wait(3000);
            }

            bot.log("Match found");

            client.clickChampSearch();
            bot.wait(2000);


            //here you have to give some samples of champions you own
            RandomChampion = new Random();
            switch(RandomChampion.Next(39))
            {
                case 1:
                    bot.inputWords("Taric");
                    break;

                case 2:
                    bot.inputWords("Volibear");
                    break;

                case 3:
                    bot.inputWords("Urgot");
                    break;

                case 4:
                    bot.inputWords("Sona");
                    break;

                case 5:
                    bot.inputWords("Garen");
                    break;

                case 6:
                    bot.inputWords("Amumu");
                    break;

                case 7:
                    bot.inputWords("Vi");
                    break;

                case 8:
                    bot.inputWords("Lux");
                    break;

                case 9:
                    bot.inputWords("Caitlyn");
                    break;

                case 10:
                    bot.inputWords("Miss Fortune");
                    break;

                case 11:
                    bot.inputWords("Tristana");
                    break;

                case 12:
                    bot.inputWords("Ashe");
                    break;

                case 13:
                    bot.inputWords("Soraka");
                    break;

                case 14:
                    bot.inputWords("Kayle");
                    break;

                case 15:
                    bot.inputWords("Janna");
                    break;

                case 16:
                    bot.inputWords("Udyr");
                    break;

                case 17:
                    bot.inputWords("Annie");
                    break;

                case 18:
                    bot.inputWords("Master Yi");
                    break;

                case 19:
                    bot.inputWords("Cassiopeia");
                    break;

                case 20:
                    bot.inputWords("Sivir");
                    break;

                case 21:
                    bot.inputWords("Brand");
                    break;
                
                case 22:
                    bot.inputWords("Ahri");
                    break;

                case 23:
                    bot.inputWords("Darius");
                    break;

                case 24:
                    bot.inputWords("Nidalee");
                    break;

                case 25:
                    bot.inputWords("Warwick");
                    break;

                case 26:
                    bot.inputWords("Shaco");
                    break;

                case 27:
                    bot.inputWords("Nunu");
                    break;

                case 28:
                    bot.inputWords("Rammus");
                    break;

                case 29:
                    bot.inputWords("Lucian");
                    break;

                case 30:
                    bot.inputWords("Xin Zhao");
                    break;

                case 31:
                    bot.inputWords("Mordekaiser");
                    break;

                case 32:
                    bot.inputWords("Dr. Mundo");
                    break;

                case 33:
                    bot.inputWords("Skarner");
                    break;

                case 34:
                    bot.inputWords("Poppy");
                    break;

                case 35:
                    bot.inputWords("Zilean");
                    break;
                
                case 36:
                    bot.inputWords("Singed");
                    break;
                
                case 37:
                    bot.inputWords("Ryze");
                    break;

                case 38:
                    bot.inputWords("Talon");
                    break;

                case 39:
                    bot.inputWords("Fiddlesticks");
                    break;

                
            }

            bot.wait(2000);

            client.selectFirstChampion();

            bot.wait(2000);

            client.lockChampion();

            bot.wait(1500);

            RandomTextSender = new Random();
            switch (RandomTextSender.Next(5))
            {
                case 1:
                    game.chat.talkInChampSelect("hi guys");
                    game.chat.talkInChampSelect("supporting bot");
                    break;
                case 2:
                    game.chat.talkInChampSelect("gl boys dont flame");
                    break;
                case 3:
                    game.chat.talkInChampSelect("sup");
                    break;
                case 4:
                    game.chat.talkInChampSelect("bot");
                    break;
                case 5:
                    game.chat.talkInChampSelect("support");
                    break;
                default:
                    game.chat.talkInChampSelect("Hi guys");
                    game.chat.talkInChampSelect("Going botlane");
                    break;
            }
            //champion not selected?

            bot.executePattern("Coop");
        }
    }
}
