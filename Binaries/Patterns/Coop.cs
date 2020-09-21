
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
        private int AllyIndex
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

            GameLoop();

            bot.executePattern("EndCoop");
        }

        private void GameLoop()
        {
            AllyIndex = game.getAllyIdToFollow();
            
            bot.log("Following ally no " + AllyIndex);

            int level = game.player.getLevel();

            bool dead = false;

            bool isRecalling = false;

            while (bot.isProcessOpen(GAME_PROCESS_NAME))
            {
                bot.bringProcessToFront(GAME_PROCESS_NAME);

                bot.centerProcess(GAME_PROCESS_NAME);
                
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
                        isRecalling = false;
                        OnSpawnJoin();
                    }

                    bot.wait(4000);
                    continue;
                }

                if (dead)
                {
                    dead = false;
                    OnRevive();
                }

                if (isRecalling)
                {
                    game.player.recall();
                    bot.wait(4000);

                    if (game.player.getManaPercent() == 1)
                    {
                        OnSpawnJoin();
                        isRecalling = false;
                    }
                    continue;
                }

                

                if (game.player.getManaPercent() <= 0.10d)
                {
                    isRecalling = true;
                    continue;
                }


                CastAndMove();

               

            }
        }
        private void OnSpawnJoin()
        {
            game.shop.toogle();

            for (int i = 10; i >= 3; i--)
            {
                game.shop.buyItem(i);
            }

            game.shop.toogle();

            AllyIndex = game.getAllyIdToFollow();
            game.camera.lockAlly(AllyIndex);
        }
        private void OnRevive()
        {
            AllyIndex = game.getAllyIdToFollow();

            game.camera.lockAlly(AllyIndex);

            bot.log("Following ally no " + AllyIndex);
        }

        private void CastAndMove() // Replace this by Champion pattern script.
        {
            game.moveCenterScreen();

            game.player.tryCastSpellOnTarget(3); // veigar cage

            bot.wait(500);

            game.moveCenterScreen();

            game.player.tryCastSpellOnTarget(2); // Z

            bot.wait(500);

            game.moveCenterScreen();

            game.player.tryCastSpellOnTarget(1); // Q

            bot.wait(500);

            game.moveCenterScreen();

            game.player.tryCastSpellOnTarget(4); // ult 
        }

        public override void End()
        {
            bot.executePattern("EndCoop");
            base.End();
        }
    }
}
