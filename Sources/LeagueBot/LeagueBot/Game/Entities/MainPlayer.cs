using LeagueBot.Api;
using LeagueBot.ApiHelpers;
using LeagueBot.Image;
using LeagueBot.IO;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace LeagueBot.Game.Entities
{
    public class MainPlayer : ApiMember<GameApi>
    {
        private int PlayerLevel;
        private Point PlayerPosition;
        private string URL;

        Api.Game game = new Api.Game();

        public MainPlayer(GameApi api) : base(api)
        {
            URL = "https://127.0.0.1:2999/liveclientdata/activeplayer";
            PlayerLevel = 0;
        }

        private void update()
        {
            string json;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                json = new WebClient().DownloadString(URL);
                JObject jo = JObject.Parse(json);
                game = jo.ToObject<Api.Game>();
                game.currentHealth = (int)jo.SelectToken("championStats.currentHealth");
                game.maxHealth = (int)jo.SelectToken("championStats.maxHealth");
                game.resourceValue = (int)jo.SelectToken("championStats.resourceValue");
                game.resourceMax = (int)jo.SelectToken("championStats.resourceMax");
            }
            catch
            {
                game.currentHealth = 1;
                game.maxHealth = 1;
                game.resourceMax = 1;
                game.resourceValue = 1;
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
        public void waitUntilMinionSpawn()
        {
            //wait calculations
        }

        // --------------------------------------- SPELL ORDER FOR COMBOS TO ENEMIES
        public void processSpellToEnemyChampions()
        {
            //LUX
            /*tryCastSpellToEnemyChampion(1);
            tryCastSpellToEnemyChampion(3);
            BotHelper.Wait(550);
            tryCastSpellToEnemyChampion(3);
            tryCastSpellToEnemyChampion(4);*/
            //ASHE
            tryCastSpellToEnemyChampion(2);
            tryCastSpellToEnemyChampion(4);
            tryCastSpellToEnemyChampion(1);
        }

        // --------------------------------------- SPELL ORDER FOR COMBOS TO CREEPS
        public void processSpellToEnemyCreeps()
        {
            //LUX
            /*tryCastSpellToEnemyChampion(3);
            BotHelper.Wait(550);
            tryCastSpellToEnemyChampion(3);*/
            //ASHE
            //tryCastSpellToEnemyCreep(2);
        }

        public void moveNearestBotlaneAllyTower()
        {
            //Primera torreta aliada con placas aun viva.
            if (ImageHelper.GetColor(1410, 924) == "#1C4F5D")//ALIVE: #1B4D5A - 1422,904 (buscar color)
            {
                InputHelper.RightClick(1410, 911);
                BotHelper.InputIdle();
                return;
            }
            if (ImageHelper.GetColor(1387, 834) == "#3592B1")
            {
                InputHelper.RightClick(1410, 911);
                BotHelper.InputIdle();
                return;
            }
            //Primera torreta aliada sin placas
            if (ImageHelper.GetColor(1411, 922) == "#2A788D")//ALIVE: #1B4D5A - 1422,904 (buscar color)
            {
                InputHelper.RightClick(1410, 911);
                BotHelper.InputIdle();
                return;
            }
            //Segunda torreta aliada sin placas
            if (ImageHelper.GetColor(1366, 916) == "#328AA8")//ALIVE: #1B4D5A - 1422,904 (buscar color)
            {
                InputHelper.RightClick(1366, 916);
                BotHelper.InputIdle();
                return;
            }
            //Tercera torreta aliada sin placas
            if (ImageHelper.GetColor(1335, 917) == "#2C7B92")//ALIVE: #1B4D5A - 1422,904 (buscar color)
            {
                InputHelper.RightClick(1335, 916);
                BotHelper.InputIdle();
                return;
            }

            InputHelper.RightClick(1308, 905);
            BotHelper.InputIdle();
        }

        public bool dead()
        {
            update();
            return game.currentHealth == 0;
        }
        public void castSpell(int indice, int x, int y)
        {
            string key = "D" + indice;
            InputHelper.MoveMouse(x, y);
            InputHelper.PressKey(key);
            BotHelper.InputIdle();
        }
        public void fixItemsInShop()
        {
            if (ImageHelper.ImageExist("Game/toggleshopitems.png"))
            {

                BotHelper.InputIdle();
                InputHelper.LeftClick(1050, 240, 150);
                Logger.WriteInformation("Items in Shop Fixed!", game.summonerName);
                BotHelper.InputIdle();
            }
        }
        public void upgradeSpell(int indice)
        {
            Point coords = new Point();

            Logger.Write("Leveling up spell " + indice);

            Keys spell;

            switch (indice)
            {
                case 1:
                    spell = Keys.Q;
                    break;
                case 2:
                    spell = Keys.W;
                    break;
                case 3:
                    spell = Keys.E;
                    break;
                case 4:
                    spell = Keys.R;
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
                    upgradeSpell(2); // Q 1
                    break;
                case 2:
                    upgradeSpell(3); // W 1
                    break;
                case 3:
                    upgradeSpell(1); // E 1
                    break;
                case 4:
                    upgradeSpell(2); // Q 2
                    break;
                case 5:
                    upgradeSpell(2); // Q 3
                    break;
                case 6:
                    upgradeSpell(4); // R 1
                    break;
                case 7:
                    upgradeSpell(2); // Q 4
                    break;
                case 8:
                    upgradeSpell(1); // E 2
                    break;
                case 9:
                    upgradeSpell(2); // Q max
                    break;
                case 10:
                    upgradeSpell(1); // E 3
                    break;
                case 11:
                    upgradeSpell(4); // R 2
                    break;
                case 12:
                    upgradeSpell(1); // E 4
                    break;
                case 13:
                    upgradeSpell(3); // E max
                    break;
                case 14:
                    upgradeSpell(3); // W 2
                    break;
                case 15:
                    upgradeSpell(3); // W 3
                    break;
                case 16:
                    upgradeSpell(4); // R max
                    break;
                case 17:
                    upgradeSpell(3); // W 4
                    break;
                case 18:
                    upgradeSpell(3); // W max
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

            //InputHelper.RightClick(770, 690);
            InputHelper.RightClick(780, 600);
            Logger.WriteInformation("Is just moving away.", game.summonerName);
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
            return (int)(100*game.currentHealth/game.maxHealth);
        }
        public int getManaPercent()
        {
            return (int)(100 * game.resourceValue / game.resourceMax);
        }
        public int enemyCreepHealth()
        {
            int value;
            try
            {
                value = ImageValues.EnemyCreepHealth();
            }
            catch
            {
                value = 0;
            }
            return value;
        }

        public int allyCreepHealth()
        {
            int value;
            try
            {
                value = ImageValues.AllyCreepHealth();
            }
            catch
            {
                value = 0;
            }
            return value;
        }
        public void allyCreepPosition()
        {
            Point go = ImageValues.AllyCreepPosition();

            if (go.X == 0 && go.Y == 0)
                return;

            Logger.WritePixel($"Ally creep has been found on [X: {go.X}, Y: {go.Y}] ~ allyCreepPosition()");
            //InputHelper.RightClick(go.X-25,go.Y+120);
            InputHelper.MoveMouse(go.X - 40, go.Y + 135);
            BotHelper.Wait(350);
            InputHelper.PressKey("A");
        }
        public bool isThereAnAllyCreep()
        {
            Point go = ImageValues.AllyCreepPosition();

            if (go.X == 0 && go.Y == 0)
                return false;

            return true;
        }
        public bool isThereAnEnemyCreep()
        {
            Point go = ImageValues.EnemyCreepPosition();

            if (go.X == 0 && go.Y == 0)
                return false;

            Logger.WritePixel($"Enemy creep has been found on [X: {go.X}, Y: {go.Y}] ~ isThereAnEnemyCreep()");
            return true;
        }
        public bool isThereAnEnemy()
        {
            Point go = ImageValues.EnemyChampion();

            if (go.X == 0 && go.Y == 0)
                return false;

            Logger.WritePixel($"Enemy character has been found on [X: {go.X}, Y: {go.Y}] ~ isThereAnEnemy()");
            InputHelper.PressKey("A");
            return true;
        }
        public bool isThereAnAlly()
        {
            Point go = ImageValues.AllyChampion();

            if (go.X == 0 && go.Y == 0)
                return false;

            Logger.WritePixel($"Ally character has been found on [X: {go.X}, Y: {go.Y}] ~ isThereAnAlly()");
            return true;
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

        public void tryCastSpellToCreep(int indice)
        {
            Point go = ImageValues.EnemyCreepPosition();

            if (go.X == 0 && go.Y == 0)
                return;

            InputHelper.MoveMouse(go.X + 28, go.Y + 42);

            string key = "D" + indice;

            InputHelper.PressKey(key.ToString());
            BotHelper.Wait(65);
        }

        /*public void moveAwayFromEnemy()
        {
            Point go = ImageValues.EnemyChampion();

            if (go.X == 0 && go.Y == 0)
                return;

            //InputHelper.RightClick(770, 690); 
            BotHelper.Wait(100);
            InputHelper.RightClick(780, 600);
            BotHelper.Wait(1250); // depends on attack speed
                                 //InputHelper.MoveMouse(go.X + 50, go.Y + 100);
                                 //InputHelper.MoveMouse(970, 540); //RIGHT
            InputHelper.MoveMouse(go.X + 45, go.Y + 100); //attack enemy, not creeps.
            InputHelper.PressKey("A");
        }
        public void moveAwayFromCreep()
        {
            Point go = ImageValues.EnemyCreepPosition();

            if (go.X == 0 && go.Y == 0)
                return;

            BotHelper.Wait(200);
            //InputHelper.RightClick(770, 690); //680.680
            InputHelper.RightClick(780, 600);
            BotHelper.Wait(850); // depends on attack speed
                                  //InputHelper.MoveMouse(go.X + 50, go.Y + 100);
            InputHelper.MoveMouse(970, 540);
            InputHelper.PressKey("A");
        }*/

        //REWORKED moveAway~() functions.
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

            while (playerHasRange("enemy"))
            {
                if (towerHealthBarFound())
                    break;
                if (enemyTowerHealthBarFound2())
                    break;
                if (!isThereAnAllyCreep())
                    break;
                if (!isPlayerBotInScreen())
                    break;

                go = ImageValues.EnemyChampion();

                if (go.X == 0 && go.Y == 0)
                    break;

                InputHelper.MoveMouse(go.X + 45, go.Y + 100);
                InputHelper.PressKey("A");
            }

            InputHelper.RightClick(780, 600);
            BotHelper.Wait(r.Next(222, 433));
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
                if (isThereAnEnemy())
                    break;
                if (!isThereAnAllyCreep())
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
