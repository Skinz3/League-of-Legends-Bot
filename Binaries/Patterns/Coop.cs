
using System;
using System.Collections.Generic;
using System.Drawing;
using LeagueBot.Patterns;
using LeagueBot.Game.Enums;
using LeagueBot.Game.Misc;

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

            //bot._outActualTime = 0;
            bot.waitProcessOpen(GAME_PROCESS_NAME);
            bot.log("Champion selected, loading game...");
            //bot._outActualTime = 0;

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

            bot.wait(1000);

            game.player.setLevel(0);
            
            // On itemset for tristana
            /*Item[] items = { 
                        new Item("Botas Iniciales",300,false,false,0,new Point(934,443)),
                        new Item("Daga Kircheis", 700, false, false, 0, new Point(732,441)),
                        new Item("BFSword", 1300, false, false, 0, new Point(573,437)),
                        new Item("Stormrazor", 3200, false, false, 0, new Point(580,540)),
                        new Item("Botas Berserker", 1100, false, false, 0, new Point(937,540)),
                        new Item("Filo Rapido", 2600, false, false, 0, new Point(755,540)),
                        new Item("Filo infinito", 3400, false, false, 0, new Point(590,660)),
                        new Item("Lord Dominik", 2800, false, false, 0, new Point(750,550)),
                        new Item("Sanginaria", 3500, false, false, 0, new Point(930,660))
            };*/
            // On itemset for ziggs
            /*Item[] items = {
                        new Item("Boots of Speed",300,false,false,0, ITEM_POSITIONS[1,0]),
                        new Item("Lost Chapter", 1300, false, false, 0, ITEM_POSITIONS[1,1]),
                        new Item("Blasting Wand", 850, false, false, 0, ITEM_POSITIONS[1,2]),
                        new Item("Sorcerer's Shoes", 1100, false, false, 0, ITEM_POSITIONS[2,0]),
                        new Item("Luden's Echo", 3200, false, false, 0, ITEM_POSITIONS[2,1]),
                        new Item("Lich Bane", 3200, false, false, 0, ITEM_POSITIONS[2,2]),
                        new Item("Morello", 3000, false, false, 0, ITEM_POSITIONS[3,0]),
                        new Item("Rabadon's Deathcap", 3600, false, false, 0, ITEM_POSITIONS[3,2]),
                        new Item("Void Staff", 2650, false, false, 0, ITEM_POSITIONS[3,1])
            };*/
            //On Itemset for chogat
            /*Item[] items = {
                        new Item("Boots of Speed",300,false,false,0, ITEM_POSITIONS[1,2]),
                        new Item("Catalyst", 1100, false, false, 0, ITEM_POSITIONS[1,0]),
                        new Item("Glacial", 900, false, false, 0, ITEM_POSITIONS[1,1]),
                        new Item("Mercurys", 1100, false, false, 0, ITEM_POSITIONS[2,2]),
                        new Item("Banshees", 3000, false, false, 0, ITEM_POSITIONS[3,1]),
                        new Item("Deadmans plate", 2900, false, false, 0, ITEM_POSITIONS[3,0]),
                        new Item("Abysal mask", 3000, false, false, 0, ITEM_POSITIONS[2,1]),
                        new Item("Glory", 2650, false, false, 0, ITEM_POSITIONS[2,0]),
                        new Item("Frozen Heart", 2650, false, false, 0, ITEM_POSITIONS[4,1])
            };*/
            //On Itemset for lux
            Item[] items = {
                        new Item("Lost Chapter",1300,false,false,0,game.shop.getItemPosition(ShopItemTypeEnum.Early,1)),
                        new Item("Basic Bots", 300, false, false, 0, game.shop.getItemPosition(ShopItemTypeEnum.Early,0)),
                        new Item("Ludens", 3200, false, false, 0, game.shop.getItemPosition(ShopItemTypeEnum.Essential,1)),
                        new Item("Upgraded bots", 1100, false, false, 0,  game.shop.getItemPosition(ShopItemTypeEnum.Essential,0)),
                        new Item("Morello", 3000, false, false, 0, game.shop.getItemPosition(ShopItemTypeEnum.Essential,2)),
                        new Item("Rabadon", 3600, false, false, 0,  game.shop.getItemPosition(ShopItemTypeEnum.Offensive,0)),
                        new Item("Void Staff", 2650, false, false, 0,  game.shop.getItemPosition(ShopItemTypeEnum.Offensive,1)),
                        new Item("First morello part", 1600, false, false, 0, game.shop.getItemPosition(ShopItemTypeEnum.Early,2)),
                        new Item("Zhonya", 2900, false, false, 0, game.shop.getItemPosition(ShopItemTypeEnum.Offensive,2))
            };

            //if want another itemset, just copy and paste and change SELECTED_CHAMPION_SET value

            List<Item> itemsToBuy = new List<Item>(items);

            game.shop.setItemBuild(itemsToBuy);

            game.camera.toggle();

            game.shop.toogle();
            bot.wait(1000);
            game.player.fixItemsInShop();
            bot.wait(1000);
            game.shop.buyItem(1);
            game.shop.toogle();

            bot.wait(20000); //wait 20 seconds.

            game.player.moveNearestBotlaneAllyTower();

            while (bot.isProcessOpen(GAME_PROCESS_NAME))
            {
                bot.bringProcessToFront(GAME_PROCESS_NAME);
                bot.centerProcess(GAME_PROCESS_NAME);

                if (game.player.getCharacterLeveled())
                {
                    game.player.increaseLevel();
                    game.player.upSpells(); //Change order on MainPlayer.cs
                }

                //back base/buy
                if (game.player.getHealthPercent() <= 50)
                {
                    //heal usage if is available
                    if (game.player.isThereAnEnemy())
                        game.player.tryCastSpellToCreep(6);
                }

                if (game.player.getHealthPercent() <= 65)
                {
                    //heal usage if is available
                    if (game.player.isThereAnEnemy())
                        game.player.tryCastSpellToCreep(5);
                }

                if (game.player.getHealthPercent() <= 15)
                {
                    //low hp.
                    bot.wait(50);
                    game.player.moveNearestBotlaneAllyTower();
                    bot.wait(8000);
                    game.player.backBaseRegenerateAndBuy();
                    // read gold.
                    game.shop.toogle();
                    game.shop.tryBuyItem();
                    game.shop.toogle();
                    bot.wait(200);

                    game.player.moveNearestBotlaneAllyTower();
                    bot.wait(6000);
                    //prevent getting stucked by doing it again
                    game.player.moveNearestBotlaneAllyTower();
                }

                bot.wait(500);

                //getting attacked by enemy, tower or creep.
                
                if (game.player.allyCreepHealth() != 0)
                {
                    //attack enemy and run away
                    if (game.player.isThereAnEnemy())
                    {
                        game.player.processSpellToEnemyChampions();
                        game.player.moveAwayFromEnemy();
                    }
                    else
                    {
                        if (game.player.enemyCreepHealth() != 0)
                        {
                            game.player.processSpellToEnemyCreeps();
                            game.player.moveAwayFromCreep();
                        }
                        bot.wait(500);
                        game.player.allyCreepPosition();

                       /* if (game.player.isGettingAttacked())
                        {
                            game.player.JustMoveAway();
                            bot.wait(600);
                        }*/
                    }
                }
                else
                {
                    // Just run away, no allies to find.

                    if (game.player.isThereAnEnemy())
                    {
                        game.player.processSpellToEnemyChampions();
                        game.player.moveAwayFromEnemy();
                    }

                    if (game.player.enemyCreepHealth() != 0)
                    {
                        game.player.processSpellToEnemyCreeps();
                        game.player.moveAwayFromCreep();
                    }

                    if (game.player.nearTowerStructure())
                    {
                        game.player.justMoveAway();
                    }

                    //early game botlane, mid game mid.
                    /*if (game.player.gameMinute() >= 14)
                    {
                        //move midlane
                        if (game.player.tryMoveLightArea(1365, 848, "#919970")) { }
                        else if (game.player.tryMoveLightArea(1353,863, "#919970")) { }
                        else if (game.player.tryMoveLightArea(1334,869, "#919970")) { }
                    }
                    else
                    {*/
                    //move botlane - find light areas to move (vision zones)
                    /*if (game.player.tryMoveLightArea(1449, 850, "#919970")) { }
                    else if (game.player.tryMoveLightArea(966, 630, "#65898F")) { }
                    else if (game.player.tryMoveLightArea(1444, 813, "#919970")) { }
                    else if (game.player.tryMoveLightArea(1437, 791, "#919970")) { }
                    else if (game.player.tryMoveLightArea(1147, 827, "#919970")) { }
                    else if (game.player.tryMoveLightArea(495, 289, "#73979F")) { }
                    else if (game.player.tryMoveLightArea(1107, 614, "#65898F")) { }
                    else if (game.player.tryMoveLightArea(1397, 683, "#65898F")) { }
                    else { 
                        //game.player.MoveNearestBotlaneAllyTower(); 
                    }*/

                    /* }*/

                    /*bot.wait(2250);*/
                }
            }
            
            bot.executePattern("EndCoop");
        }
    }
}
