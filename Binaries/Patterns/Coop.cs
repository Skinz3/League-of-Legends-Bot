using System;
using System.Collections.Generic;
using System.Drawing;
using LeagueBot.Patterns;
using LeagueBot.Game.Enums;
using LeagueBot.Game.Misc;
using LeagueBot.Game.Objects;
using LeagueBot.Api;
using LeagueBot.ApiHelpers;

using System.IO;


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
			
            bool develop_mode = false;
            bool CreepHasBeenFound = false;
            bool fixCamera = false;
            int allyIndex = 2;
			
            bot.log("waiting for league of legends process...");
			
            if (!develop_mode)
			{
                bot.wait(95000);
			}
            bot.log("waiting done");
            if (bot.isProcessOpen(GAME_PROCESS_NAME))
            {
                bot.waitProcessOpen(GAME_PROCESS_NAME);
                bot.log("Champion selected, loading game...");
                
                bot.waitUntilProcessBounds(GAME_PROCESS_NAME, 1030, 797);
                bot.wait(200);

                bot.log("waiting for game to load.");

                bot.bringProcessToFront(GAME_PROCESS_NAME);
                bot.centerProcess(GAME_PROCESS_NAME);

                game.waitUntilGameStart();

                bot.log("We are in game!");
				string championName = LocalPlayer.GetChampionName();
				bot.log(championName + " picked.");	
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

                bot.wait(1000);
				
                game.player.setLevel(0);

                #region items

               
                Item[] items = {
                            new Item("Vampiric Scepter",900,false,false, 0, game.shop.getItemPosition(ShopItemTypeEnum.Early,1)),
                            new Item("Long Sword", 350, false, false, 0, game.shop.getItemPosition(ShopItemTypeEnum.Early,0)),
                            new Item("Berserker's Greaves", 1100, false, false, 0, game.shop.getItemPosition(ShopItemTypeEnum.Essential,2)),
                            new Item("B.F. Sword", 1300, false, false, 0,  game.shop.getItemPosition(ShopItemTypeEnum.Offensive,1)),
                            new Item("Bloodthirster", 3500, false, false, 0,  game.shop.getItemPosition(ShopItemTypeEnum.Offensive,2)),
                            new Item("B.F. Sword", 1300, false, false, 0,  game.shop.getItemPosition(ShopItemTypeEnum.Offensive,1)),
                            new Item("Infinity Edge", 3400, false, false, 0,  game.shop.getItemPosition(ShopItemTypeEnum.Defensive,2)),
                            new Item("Statikk Shiv", 2600, false, false, 0,  game.shop.getItemPosition(ShopItemTypeEnum.Defensive,0)),
                            new Item("Frozen Mallet", 3100, false, false, 0,  game.shop.getItemPosition(ShopItemTypeEnum.Defensive,1)),
                };

                //if want another itemset, just copy and paste and change SELECTED_CHAMPION_SET value

                #endregion

                List<Item> itemsToBuy = new List<Item>(items);

                game.shop.setItemBuild(itemsToBuy);

                game.shop.toogle();

                bot.wait(2000);
                game.shop.fixItemsInShop();
                bot.wait(1000);
                game.shop.buyItem(1);
                game.shop.toogle();

                if (!develop_mode)
                    bot.wait(3000); //wait 3 seconds.

                //if (fixCamera)
                //{
                 //   cameraFix();
                //}
				
				game.player.lockcamera();

                game.player.moveNearestBotlaneAllyTower();
				bot.wait(3000);
				
                 while (bot.isProcessOpen(GAME_PROCESS_NAME)) // Game loop
                {

                    bot.bringProcessToFront(GAME_PROCESS_NAME);
                    bot.centerProcess(GAME_PROCESS_NAME);
                    

                    if (game.player.getCharacterLeveled())
                    {
                        game.player.increaseLevel();
                        game.player.upSpells(); //Change order on MainPlayer.cs
                    }

                    int health = game.player.getHealthPercent();

                    //back base/buy
                    if (health <= 50)
                    {
                        //heal usage if is available
                        if (game.player.isThereAnEnemy())
                            game.player.heal();
                    }

                    if (health <= 88)
                    {
                        //heal usage if is available
                        if (game.player.isThereAnEnemy())
                            game.player.heal();
                    }

                    if (health <= 25)
                    {
						game.player.heal();
                        
                    }

                    //getting attacked by enemy, tower or creep.

                    if (game.player.isThereAnAllyCreep())
                    {
                        //attack enemy and run away
                        if (game.player.isThereAnEnemy())
                        {
                            game.player.combo();
                            game.player.moveAwayFromEnemy();
                        }
                        else
                        {
                            if (game.player.isThereAnEnemyCreep())
                            {
                                game.player.harras();
                                game.player.moveAwayFromCreep();
                            }
                            bot.wait(100);
                            game.player.allyCreepPosition();
                            CreepHasBeenFound = true;
                        }
                    }
                    else
                    {
                       // Just run away, no allies to find.
						if (game.player.dead())
						{
                        //low hp.
                        game.player.justMoveAway();
                        bot.wait(2000);
                        //game.player.backBaseRegenerateAndBuy();
                        // read gold.
                        game.shop.toogle();
                        game.shop.tryBuyItem();
                        game.shop.toogle();
                        bot.wait(200);
						while(game.player.dead())
						{
							bot.log("Player dead.Waiting for player alive");
							bot.wait(1000);
						}
                        game.player.moveNearestBotlaneAllyTower();
                        bot.wait(2000);
                        //prevent getting stucked by doing it again
                        game.player.moveNearestBotlaneAllyTower();
						}

                        if (game.player.isThereAnEnemy())
                        {
                            game.player.combo();
                            game.player.moveAwayFromEnemy();
                        }

                        if (game.player.isThereAnEnemyCreep())
                        {
                            game.player.harras();
                            game.player.moveAwayFromCreep();
                        }

                        if (!game.player.isThereAnAllyCreep() && !game.player.isThereAnEnemy() && !game.player.nearTowerStructure() && !game.player.isThereAnEnemyCreep())
                        {
                            bot.log("Creeps not found or your images are wrong.");
                            if (game.player.tryMoveLightArea(1397, 683, "#65898F")) { }
                            else if (game.player.tryMoveLightArea(966, 630, "#65898F")) { }
                            else if (game.player.tryMoveLightArea(1444, 813, "#919970")) { }
                            else
                            {
                                if (CreepHasBeenFound)
                                    game.camera.lockAlly(allyIndex);
                                else
                                {
                                    allyIndex = incAllyIndex(allyIndex);

                                    bot.wait(5000);
                                }


                                game.moveCenterScreen();
                                if (!game.player.isThereAnAllyCreep() || !game.player.isThereAnEnemyCreep()) //if player just afks, change index.
                                {
                                    allyIndex = incAllyIndex(allyIndex);
                                }

                                bot.wait(500);

                            }
                        }

                    }

                }

                bot.executePattern("EndCoop");

            }
            else
            {
                bot.log("game not load");
                bot.executePattern("StartCoop");


            }
        }

        int incAllyIndex(int allyIndex)
        {
            return (allyIndex < 5) ? ++allyIndex : 1;
        }

        void cameraFix()
        {
            string img = Directory.GetCurrentDirectory() + @"\Images\Game\unfixedcamera.png";
            if (File.Exists(img))
            {
                game.camera.toggleIfUnlocked();
            }
            else
            {
                bot.log("cameraFix error: Image not found.");
            }
        }

    }
}