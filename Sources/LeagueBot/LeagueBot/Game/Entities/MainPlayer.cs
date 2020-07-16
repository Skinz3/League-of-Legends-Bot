using LeagueBot.Api;
using LeagueBot.ApiHelpers;
using LeagueBot.Game.Entities;
using LeagueBot.Image;
using LeagueBot.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace LeagueBot.Game.Entities
{
    public class MainPlayer : ApiMember<GameApi>
    {
        private int PlayerLevel;
        private Point PlayerPosition;
        public Spell Q,W,E,R;
        public SummonerSpell summonerSpellOne, summonerSpellTwo;
        public string championName;
        public string summonerName;

        Api.Game game = new Api.Game();
        
        

        public MainPlayer(GameApi api) : base(api)
        {
            
            PlayerLevel = 0;
            
            
        }

        private void update()
        {
            string json;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                json = new WebClient().DownloadString("https://127.0.0.1:2999/liveclientdata/activeplayer");
                JObject jo = JObject.Parse(json);
                game = jo.ToObject<Api.Game>();
                game.currentHealth = (int)jo.SelectToken("championStats.currentHealth");
                game.maxHealth = (int)jo.SelectToken("championStats.maxHealth");
                game.resourceValue = (int)jo.SelectToken("championStats.resourceValue");
                game.resourceMax = (int)jo.SelectToken("championStats.resourceMax");
                game.summonerName = (string)jo.SelectToken("summonerName");
                this.summonerName = (string)jo.SelectToken("summonerName");

            }
            catch
            {
                game.currentHealth = 1;
                game.maxHealth = 1;
                game.resourceMax = 1;
                game.resourceValue = 1;
                game.summonerName = "Cant read";
            }
        }


        public string getSummonerName()
        {
            update();
             
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            string json = new WebClient().DownloadString("https://127.0.0.1:2999/liveclientdata/playerlist");
            JArray jo = JArray.Parse(json);

            string summs = new WebClient().DownloadString("https://127.0.0.1:2999/liveclientdata/playersummonerspells?summonerName=" + this.summonerName+"");
            JObject jo2 = JObject.Parse(summs);

            summonerSpellOne = new SummonerSpell("summonerSpellOne", jo2, 5);
            summonerSpellTwo = new SummonerSpell("summonerSpellTwo", jo2, 6);


            for (int i = 0; i<10; i++)
            {
                string summonerName = jo[i]["summonerName"].Value<string>();
                if (summonerName == this.game.summonerName)
                {
                    championName = jo[i]["championName"].Value<string>();
                    Logger.Write(championName + " is picked!");
                    this.game.championName = championName;
                    string json3 = new WebClient().DownloadString("http://ddragon.leagueoflegends.com/cdn/10.14.1/data/en_US/champion/" + this.game.championName + ".json");
                    JObject jo3 = JObject.Parse(json3);


                    this.Q = new Spell("Q", jo3, this.championName, 0);
                    this.W = new Spell("W", jo3, this.championName, 1);
                    this.E = new Spell("E", jo3, this.championName, 2);
                    this.R = new Spell("R", jo3, this.championName, 3);
                    return championName;
                }

            }
            
            return "Cant recognize championName";
            
        }

        public void heal()
        {
            if(this.summonerSpellOne.displayName == "Heal")
            {
                this.summonerSpellOne.use();
                Logger.Write("Healed");
            }
            else if(this.summonerSpellTwo.displayName == "Heal")
            {
                this.summonerSpellTwo.use();
                Logger.Write("Healed");
            }
            else
            {
                Logger.Write("Heal is not your summonerspell");
            }
            
        }
        private void updatePlayerPosition()
        {
            Point tempPlayerPosition = setPlayerPosition();

            if (tempPlayerPosition.X != 0 && tempPlayerPosition.Y != 0)
            {
                PlayerPosition = tempPlayerPosition;
            }
        }
        private Point setPlayerPosition()
        {
            Point go = ImageValues.botChampion();

            if (go.X == 0 && go.Y == 0)
                return new Point(0, 0);

            Logger.WriteInformation($"Champion player position has been set to [X: {go.X}, Y: {go.Y}] ~ setPlayerPosition()", game.summonerName);
            return go;
        }

        public int getGold()
        {
            update();
            return (int)game.currentGold;
        }

        private bool isPlayerBotInScreen()
        {
            Point go = ImageValues.botChampion();


            if (go.X == 0 && go.Y == 0)
                return false;

            return true;
        }
      

        public void combo()
        {
            this.Q.cast();
            this.W.cast();
            this.E.cast();
            this.R.cast();
        }

        public void harras()
        {
            this.W.cast();
        }

        public void farm()
        {
            this.E.cast();
        }


        public void moveNearestBotlaneAllyTower()
        {
            InputHelper.RightClick(1430, 906);
            BotHelper.Wait(5000);
        }

        public bool dead()
        {
            update();
            return game.currentHealth == 0;
        }
      

        public void upgradeSpell(int indice)
        {
           
            Logger.Write("Leveling up spell " + indice);

            Keys spell;

            switch (indice)
            {
                case 1:
                    spell = Keys.Q;
                    this.Q.level += 1;              
                    break;
                case 2:
                    spell = Keys.W;
                    this.W.level += 1;
                    break;
                case 3:
                    spell = Keys.E;
                    this.E.level += 1;
                    break;
                case 4:
                    spell = Keys.R;
                    this.R.level += 1;
                    break;
                default:
                    Logger.Write("Unknown spell indice :" + indice, MessageState.WARNING);
                    spell = Keys.Q;
                    return;
            }

            InputManager.Keyboard.ShortcutKeys(new Keys[] { Keys.ControlKey, spell }, 30);

            BotHelper.InputIdle();
        }
        public void setLevel(int level)
        {
            PlayerLevel = level;
        }
        public void increaseLevel()
        {
            update();
            PlayerLevel = game.level;
        }
        public int getLevel()
        {
            return game.level;
        }

        public void upSpells()
        {
            switch (PlayerLevel)
            {
                case 1:
                    upgradeSpell(1); // Q 1
                    break;
                case 2:
                    upgradeSpell(2); // W 1
                    break;
                case 3:
                    upgradeSpell(3); // E 1
                    break;
                case 4:
                    upgradeSpell(1); // Q 2
                    break;
                case 5:
                    upgradeSpell(2); // W 3
                    break;
                case 6:
                    upgradeSpell(3); // R 1
                    break;
                case 7:
                    upgradeSpell(1); // Q 4
                    break;
                case 8:
                    upgradeSpell(3); // E 2
                    break;
                case 9:
                    upgradeSpell(1); // Q max
                    break;
                case 10:
                    upgradeSpell(3); // E 3
                    break;
                case 11:
                    upgradeSpell(4); // R 2
                    break;
                case 12:
                    upgradeSpell(3); // E 4
                    break;
                case 13:
                    upgradeSpell(3); // E max
                    break;
                case 14:
                    upgradeSpell(2); // W 2
                    break;
                case 15:
                    upgradeSpell(2); // W 3
                    break;
                case 16:
                    upgradeSpell(4); // R max
                    break;
                case 17:
                    upgradeSpell(2); // W 4
                    break;
                case 18:
                    upgradeSpell(2); // W max
                    break;
                default:
                    //something not leveled?
                    upgradeSpell(1);
                    upgradeSpell(2);
                    upgradeSpell(3);
                    upgradeSpell(4);
                    break;
            }
        }
        public bool isGettingAttacked()
        {
            int dmg = TextHelper.GetTextFromImage(881, 507, 65, 41);

            if (dmg != 0)
                return true;
            else
                return false;
        }
        public void justMoveAway()
        {
            Point go = ImageValues.EnemyChampion();

            if (go.X == 0 && go.Y == 0)
                return;

            InputHelper.RightClick(1411, 922);
            Logger.Write("Is just moving away.");
            BotHelper.Wait(1000);
        }
        public void nothingHereMoveAway()
        {
            InputHelper.RightClick(780, 600);
            Logger.WriteInformation("Nothing here, moving away.", game.summonerName);
            BotHelper.Wait(1000);
        }
        public bool getCharacterLeveled()
        {
            update();
            return game.level > this.PlayerLevel;
        }

        public int getHealthPercent()
        {
            update();
            Logger.WriteColor1("Your heal: "+ (game.currentHealth / game.maxHealth) * 100);
            return (int)((game.currentHealth/game.maxHealth)*100);
        }
        public int getManaPercent()
        {
            return (int)(100 * game.resourceValue / game.resourceMax);
        }



        public void moveAwayFromEnemy()
        {
            Point go = ImageValues.EnemyChampion();

            if (go.X == 0 && go.Y == 0)
                return;

            Random r = new Random();
            InputHelper.MoveMouse(go.X + 45, go.Y + 100);
            InputHelper.PressKey("A");
            BotHelper.Wait(r.Next(866, 1233));

            //PLAYER POSITION UPDATER
            updatePlayerPosition();

            //while (playerHasRange("enemy"))
            //{
                //if (towerHealthBarFound())
                //    break;
                //if (enemyTowerHealthBarFound2())
                //    break;
                //if (!AllyMinion.isThereAnAllyCreep2())
                //    break;
                //if (!isPlayerBotInScreen())
                //    break;

                go = ImageValues.EnemyChampion();

                //if (go.X == 0 && go.Y == 0)
                //    break;

                InputHelper.MoveMouse(go.X + 45, go.Y + 100);
                InputHelper.PressKey("A");


            //}

            InputHelper.RightClick(780, 600);
            
            BotHelper.Wait(r.Next(222, 433));
        }




        public bool nearTowerStructure()
        {
            Point go = ImageValues.EnemyTowerStructure();
            Point go2 = ImageValues.EnemyTowerStructure2();
            Point go3 = ImageValues.EnemyTowerStructure3();
            Point go4 = ImageValues.EnemyTowerStructure4();

            bool isNear = false;

            if (go.X != 0 && go.Y != 0)
                isNear = true;

            if (go2.X != 0 && go2.Y != 0)
                isNear = true;

            if (go3.X != 0 && go3.Y != 0)
                isNear = true;

            if (go4.X != 0 && go4.Y != 0)
                isNear = true;

            if (isNear)
                Logger.WritePixel($"Tower structure has been found on [X: {go.X}, Y: {go.Y}] ~ nearTowerStructure()");

            return isNear;
        }

       
        

        private bool towerHealthBarFound()
        {
            Point go = ImageValues.EnemyTowerHp();

            if (go.X == 0 && go.Y == 0)
                return false;

            return true;
        }
        private bool enemyTowerHealthBarFound2()
        {
            Point go = ImageValues.EnemyTowerHp2();

            if (go.X == 0 && go.Y == 0)
                return false;

            return true;
        }

        private bool playerHasRange(string type)
        {
            Random r = new Random();
            Point go;
            updatePlayerPosition();

            switch (type)
            {
                case "enemy":
                    go = ImageValues.EnemyChampion();
                    if (go.X != 0 && go.Y != 0)
                    {
                        BotHelper.Wait(r.Next(250, 400));
                        return true;
                    }
                    break;
                case "creep":
                    go = ImageValues.EnemyCreepPosition();
                    if (go.X != 0 && go.Y != 0)
                    {
                        BotHelper.Wait(r.Next(250, 400));
                        return true;
                    }
                    break;
            }

            return false;
        }

        public void moveAwayFromCreep()
        {
            Point go = ImageValues.EnemyCreepPosition();

            if (go.X == 0 && go.Y == 0)
                return;

            Random r = new Random();
            InputHelper.MoveMouse(970, 540);
            InputHelper.PressKey("A");
            BotHelper.Wait(r.Next(800, 1000));

            //PLAYER POSITION UPDATER
            updatePlayerPosition();

            while (playerHasRange("creep"))
            {
                if (EnemyCharacter.isThereAnEnemy2())
                    break;
                if (!AllyMinion.isThereAnAllyCreep2())
                    break;
                if (towerHealthBarFound())
                    break;
                if (enemyTowerHealthBarFound2())
                    break;
                if (!isPlayerBotInScreen())
                    break;

                go = ImageValues.EnemyCreepPosition();

                if (go.X == 0 && go.Y == 0)
                    break;

                InputHelper.MoveMouse(go.X + 28, go.Y + 42);
                InputHelper.PressKey("A");
            }

            //..
            InputHelper.RightClick(780, 600);
            BotHelper.Wait(r.Next(155, 205));
        }
        public void tryCastSpellToEnemyChampion(int indice)
        {
            Point go = ImageValues.EnemyChampion();

            if (go.X == 0 && go.Y == 0)
                return;

            InputHelper.MoveMouse(go.X + 39, go.Y + 129);

            string key = "D" + indice;

            InputHelper.PressKey(key.ToString());
            BotHelper.Wait(50);
        }

        public void tryCastSpellToEnemyCreep(int indice)
        {
            Point go = ImageValues.EnemyCreepPosition();

            if (go.X == 0 && go.Y == 0)
                return;

            InputHelper.MoveMouse(go.X + 39, go.Y + 129);

            string key = "D" + indice;

            InputHelper.PressKey(key.ToString());
            BotHelper.Wait(50);
        }

        public void backBaseRegenerateAndBuy()
        {
            InputHelper.PressKey("B");
            Logger.WriteInformation($"Player is low hp or dead.", game.summonerName);
            Thread.Sleep(11000); // 11 seconds, we should be in base already.
        }

        public int gameMinute()
        {
            int minute = TextHelper.GetTextFromImage(1426, 171, 16, 16);
            Logger.WriteInformation("Game is on minute " + minute.ToString(), game.summonerName);
            return minute;
        }

        public bool tryMoveLightArea(int X, int Y, string color) // Sometimes is better to not.
        {
            if (ImageHelper.GetColor(X, Y) == color)
            {
                InputHelper.RightClick(X, Y);
                BotHelper.InputIdle();
                return true;
            }
            return false;
        }
    }
}
