
using System;
using System.Collections.Generic;
using System.Drawing;
using LeagueBot.Patterns;
using LeagueBot.Game.Enums;

namespace LeagueBot
{
    public class Coop : PatternScript
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

            game.onGameStarted();

            bot.log("We are in game !");
      
            bot.bringProcessToFront(GAME_PROCESS_NAME);
            bot.centerProcess(GAME_PROCESS_NAME);

            bot.wait(3000);

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

            game.player.upgradeSpellOnLevelUp();

            game.shop.toogle();

            game.shop.buyItem(1);
            game.shop.buyItem(2);

            game.shop.toogle();

            int followedAlly = 3;

            bot.wait(2000);

            game.camera.lockAlly(followedAlly); // <--- verify ally exists!

            bot.log("Following ally no "+followedAlly);

            int level = game.player.getLevel();

            bool dead = false;

            while (bot.isProcessOpen(GAME_PROCESS_NAME))
            {
              /*  var stats = game.player.getStats() <--- you can use this
                stats.abilityPower */
                
                int newLevel = game.player.getLevel();

                if (newLevel != level)
                {
                    level = newLevel;
                    game.player.upgradeSpellOnLevelUp();
                }

                if (game.player.dead())
                {
                    if (!dead)
                    {
                        dead = true;

                        bot.log("Oops, active player is dead.");

                        game.shop.toogle();

                        for (int i = 3;i <= 10;i++)
                        {
                             game.shop.buyItem(i);
                        }
                     
                        game.shop.toogle();
                    }

                    bot.wait(4000);
                    continue; 
                }

                if (dead) // we revive ! 
                {
                    dead = false;
                    game.camera.lockAlly(followedAlly);
                }


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
