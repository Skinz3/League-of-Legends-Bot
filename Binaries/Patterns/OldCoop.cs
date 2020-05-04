
using System;
using System.Collections.Generic;
using System.Drawing;
using LeagueBot.Patterns;
using LeagueBot.Game.Enums;

namespace LeagueBot
{
    public class OldCoop : PatternScript
    {
        private Point CastTargetPoint
        {
            get;
            set;
        }
        public override void Execute()
        {
            bot.log("waiting for league of legends process...");

            bot.waitProcessOpen(GAME_PROCESS_NAME);

            bot.waitUntilProcessBounds(GAME_PROCESS_NAME, 1030, 797);

            bot.wait(200);

            bot.log("waiting for game to load.");

            bot.bringProcessToFront(GAME_PROCESS_NAME);
            bot.centerProcess(GAME_PROCESS_NAME);

            game.waitUntilGameStart();

            bot.log("We are in game !");

            bot.bringProcessToFront(GAME_PROCESS_NAME);
            bot.centerProcess(GAME_PROCESS_NAME);

            bot.wait(1000);

            game.detectSide();

            if (game.getSide() == SideEnum.Blue)
            {
                CastTargetPoint = new Point(1084, 398);
                bot.log("We are blue side!");
            }
            else
            {
                CastTargetPoint = new Point(644, 761);
                bot.log("We are red side!");
            }


            game.player.upgradeSpell(1);

            game.chat.talkInGame("Hi guys");

            game.shop.toogle();

            game.shop.buyItem(1);
            game.shop.buyItem(2);

            game.shop.toogle();

            game.camera.lockAlly(3);

            while (bot.isProcessOpen(GAME_PROCESS_NAME))
            {
                bot.bringProcessToFront(GAME_PROCESS_NAME);

                bot.centerProcess(GAME_PROCESS_NAME);

                game.moveCenterScreen();

                game.player.castSpell(1, CastTargetPoint.X, CastTargetPoint.Y);


                bot.wait(1000);

                game.moveCenterScreen();

                game.player.castSpell(2, CastTargetPoint.X, CastTargetPoint.Y);

                bot.wait(1000);

                game.moveCenterScreen();

                game.player.castSpell(3, CastTargetPoint.X, CastTargetPoint.Y);

                bot.wait(1000);

                game.moveCenterScreen();

                game.player.castSpell(4, CastTargetPoint.X, CastTargetPoint.Y);

            }
            
            bot.executePattern("EndCoop");
        }
    }
}
