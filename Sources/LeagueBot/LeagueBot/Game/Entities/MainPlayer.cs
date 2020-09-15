using LeagueBot.Api;
using LeagueBot.ApiHelpers;
using LeagueBot.Game.Objects;
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
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

namespace LeagueBot.Game.Entities
{
    public class MainPlayer : ApiMember<GameApi>
    {
        private int PlayerLevel;
        private Point PlayerPosition;

        Api.Game game = new Api.Game();

        public SummonerSpell summonerSpellOne, summonerSpellTwo;
        public Spell Q, W, E, R;

        public MainPlayer(GameApi api) : base(api)
        {
            
            PlayerLevel = 0;
            
        }

        private void getSummonerSpells()
        {
            string summonerSpells = new WebClient().DownloadString("https://127.0.0.1:" + GetPort() + "/liveclientdata/playersummonerspells?summonerName=" + game.summonerName + "");
            JObject jo2 = JObject.Parse(summonerSpells);
            this.summonerSpellOne = new SummonerSpell("summonerSpellOne", jo2, 5);
            this.summonerSpellTwo = new SummonerSpell("summonerSpellTwo", jo2, 6);
        }
        
        private String GetPort()
        {
            var processes = Process.GetProcessesByName("LeagueClient");

            using (var ns = new Process())
            {
                ProcessStartInfo psi = new ProcessStartInfo("netstat.exe", "-ano");
                psi.RedirectStandardOutput = true;
                psi.UseShellExecute = false;
                ns.StartInfo = psi;
                ns.Start();

                using (StreamReader r = ns.StandardOutput)
                {
                    string output = r.ReadToEnd();
                    ns.WaitForExit();

                    string[] lines = output.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                    foreach (string line in lines)
                    {
                        if (line.Contains(processes[0].Id.ToString()) && line.Contains("0.0.0.0:0"))
                        {
                            var outp = line.Split(' ');
                            return outp[6].Replace("127.0.0.1:", "");
                        }
                    }
                }
            }
            return String.Empty;
        }

        string _port = string.Empty;
        
        string Port
        {
            get
            {
                if (string.IsNullOrEmpty(_port))
                {
                    return GetPort();
                }

                return _port;
            }
        }

        private void update()
        {
            try
            {
                game.currentHealth = LocalPlayer.GetCurrentHealth();
                game.maxHealth = LocalPlayer.GetMaxHealth();
                game.currentGold = LocalPlayer.GetCurrentGold();
                game.currentMana = LocalPlayer.GetCurrentMana();
                game.currentManaMax = LocalPlayer.GetCurrentManaMax();
                game.summonerName = LocalPlayer.GetSummonerName();
                game.championName = LocalPlayer.GetChampionName();
                game.level = LocalPlayer.GetLevel();
                getSummonerSpells();
                this.Q = new Spell("Q", game.championName, 0);
                this.W = new Spell("W", game.championName, 1);
                this.E = new Spell("E", game.championName, 2);
                this.R = new Spell("R", game.championName, 3);

            }
            catch
            {
              
            }
        }

        public void lockcamera()
        {
            InputHelper.PressKey("Y");
        }

        public void heal()
        {
            update();
            if (this.summonerSpellOne.displayName == "heal")
            {
                this.summonerSpellOne.use();
            }
            else if (this.summonerSpellTwo.displayName == "heal")
            {
                this.summonerSpellTwo.use();
            }
            else
            {
                Logger.Write("Heal not setted.");
            }

        }

        //TODO: make spells but move it to SummonerSpells class

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
            this.Q.cast();
            BotHelper.Wait(1000);
            this.W.cast();
            BotHelper.Wait(500);
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
            return (int)(100 * game.currentHealth / game.maxHealth);
        }
        public int getManaPercent()
        {
            return (int)(100 * game.currentMana / game.currentManaMax);
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
