using LeagueBot.Api;
using LeagueBot.ApiHelpers;
using LeagueBot.Image;
using LeagueBot.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueBot.Game.Entities
{
    public class MainPlayer : ApiMember<GameApi>
    {
        private int PlayerLevel;

        public MainPlayer(GameApi api) : base(api)
        {

        }

        /*
 HOW BOT WILL PLAY:
 -1 Spawnea, avanza a su primera torreta y espera los subditos.
 -2 Cuando los subditos avanzan, avanza hacia la torreta mas cercana presionando A.
 -3 Ataca a la torreta y vuelve a resetear la linea.
 -4 Si tiene poca vida, vuelve a base y compra.
*/

        public void waitUntilMinionSpawn()
        {
            //wait calculations
        }

        // --------------------------------------- SPELL ORDER FOR COMBOS TO ENEMIES
        public void processSpellToEnemyChampions()
        {
            tryCastSpellToEnemyChampion(1);
            tryCastSpellToEnemyChampion(3);
            BotHelper.Wait(550);
            tryCastSpellToEnemyChampion(3);
            tryCastSpellToEnemyChampion(4);
        }

        // --------------------------------------- SPELL ORDER FOR COMBOS TO CREEPS
        public void processSpellToEnemyCreeps()
        {
            tryCastSpellToEnemyChampion(3);
            BotHelper.Wait(550);
            tryCastSpellToEnemyChampion(3);
        }

        public void moveNearestBotlaneAllyTower()
        {
            //Primera torreta aliada con placas aun viva.
            if(ImageHelper.GetColor(1410,924) == "#1C4F5D")//ALIVE: #1B4D5A - 1422,904 (buscar color)
            {
                InputHelper.RightClick(1410,911);
                BotHelper.InputIdle();
                return;
            }
            if(ImageHelper.GetColor(1387,834) == "#3592B1")
            {
                InputHelper.RightClick(1410, 911);
                BotHelper.InputIdle();
                return;
            }
            //Primera torreta aliada sin placas
            if (ImageHelper.GetColor(1411,922) == "#2A788D")//ALIVE: #1B4D5A - 1422,904 (buscar color)
            {
                InputHelper.RightClick(1410, 911);
                BotHelper.InputIdle();
                return;
            }
            //Segunda torreta aliada sin placas
            if (ImageHelper.GetColor(1366,916) == "#328AA8")//ALIVE: #1B4D5A - 1422,904 (buscar color)
            {
                InputHelper.RightClick(1366,916);
                BotHelper.InputIdle();
                return;
            }
            //Tercera torreta aliada sin placas
            if (ImageHelper.GetColor(1335,917) == "#2C7B92")//ALIVE: #1B4D5A - 1422,904 (buscar color)
            {
                InputHelper.RightClick(1335, 916);
                BotHelper.InputIdle();
                return;
            }
            //Todas las torretas han caido. Pues al nexo.

            InputHelper.RightClick(1308,905);
            BotHelper.InputIdle();
        }

        public bool dead()
        {
            return ImageHelper.GetColor(762,885) == "#C0FCFA";
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
                BotHelper.Log("Items in Shop Fixed!");
                BotHelper.InputIdle();
            }
        }
        public void upgradeSpell(int indice)
        {
            Point coords = new Point();

            switch (indice)
            {
                case 1:
                    coords = new Point(826, 833);
                    break;
                case 2:
                    coords = new Point(875, 833);
                    break;
                case 3:
                    coords = new Point(917, 833);
                    break;
                case 4:
                    coords = new Point(967, 833);
                    break;
                default:
                    Logger.Write("Unknown spell indice :" + indice, MessageState.WARNING);
                    return;
            }

            InputHelper.LeftClick(coords.X, coords.Y);
            BotHelper.InputIdle();
        }
        public void setLevel(int level)
        {
            PlayerLevel = level;
        }
        public void increaseLevel()
        {
            PlayerLevel++;
        }
        public int getLevel()
        {
            return PlayerLevel;
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
                    upgradeSpell(1); // Q 3
                    break;
                case 6:
                    upgradeSpell(4); // R 1
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
            BotHelper.Wait(1000);
        }
        public void nothingHereMoveAway()
        {
            InputHelper.RightClick(780, 600);
            BotHelper.Wait(1000);
        }
        public bool getCharacterLeveled()
        {
            return TextHelper.TextExists(835, 772, 81, 27, "level up");
        }

        public int getHealthPercent()
        {
            int value = ImageValues.Health();
            //BotHelper.Log("Health is " + value);
            return value;
        }
        public int getManaPercent()
        {
            int value = ImageValues.Mana();
            //BotHelper.Log("Mana is " + value);
            return value;
        }
        public int enemyCreepHealth()
        {
            int value = ImageValues.EnemyCreepHealth();
            return value;
        }
        public int allyCreepHealth()
        {
            int value = ImageValues.AllyCreepHealth();
            return value;
        }
        public void allyCreepPosition()
        {
            Point go = ImageValues.AllyCreepPosition();

            if (go.X == 0 && go.Y == 0)
                return;

            //InputHelper.RightClick(go.X-25,go.Y+120);
            InputHelper.MoveMouse(go.X - 40, go.Y + 135);
            BotHelper.Wait(350);
            InputHelper.PressKey("A");
        }
        public bool isThereAnEnemy()
        {
            Point go = ImageValues.EnemyChampion();

            if (go.X == 0 && go.Y == 0)
                return false;

            InputHelper.PressKey("A");
            return true;
        }

        public bool nearTowerStructure()
        {
            Point go = ImageValues.EnemyTowerStructure();
            Point go2 = ImageValues.EnemyTowerStructure();
            Point go3 = ImageValues.EnemyTowerStructure();
            Point go4 = ImageValues.EnemyTowerStructure();

            bool isNear = false;

            if (go.X != 0 && go.Y != 0)
                isNear = true;

            if (go2.X != 0 && go2.Y != 0)
                isNear = true;

            if (go3.X != 0 && go3.Y != 0)
                isNear = true;

            if (go4.X != 0 && go4.Y != 0)
                isNear = true;

            return isNear;
        }

        public void tryCastSpellToCreep(int indice)
        {
            Point go = ImageValues.EnemyCreepPosition();

            if (go.X == 0 && go.Y == 0)
                return;

            InputHelper.MoveMouse(go.X+28, go.Y+42);
            
            string key = "D" + indice;

            InputHelper.PressKey(key.ToString());
            BotHelper.Wait(65);
        }

        public void moveAwayFromEnemy()
        {
            Point go = ImageValues.EnemyChampion();

            if (go.X == 0 && go.Y == 0)
                return;

            //InputHelper.RightClick(770, 690); 
            InputHelper.RightClick(780, 600);
            BotHelper.Wait(1100); // depends on attack speed
            //InputHelper.MoveMouse(go.X + 50, go.Y + 100);
            InputHelper.MoveMouse(970, 540);
            InputHelper.PressKey("A");
        }
        public void moveAwayFromCreep()
        {
            Point go = ImageValues.EnemyCreepPosition();

            if (go.X == 0 && go.Y == 0)
                return;

            //InputHelper.RightClick(770, 690); //680.680
            InputHelper.RightClick(780, 600);
            BotHelper.Wait(1100); // depends on attack speed
            //InputHelper.MoveMouse(go.X + 50, go.Y + 100);
            InputHelper.MoveMouse(970, 540);
            InputHelper.PressKey("A");
        }

        public void tryCastSpellToEnemyChampion(int indice)
        {
            Point go = ImageValues.EnemyChampion();

            if (go.X == 0 && go.Y == 0)
                return;

            InputHelper.MoveMouse(go.X + 39,go.Y+129);

            string key = "D" + indice;

            InputHelper.PressKey(key.ToString());
            BotHelper.Wait(50);
        }

        public void backBaseRegenerateAndBuy()
        {
            InputHelper.PressKey("B");
            Thread.Sleep(11000); // 11 seconds, we should be in base already.
        }

        public int gameMinute()
        {
            int minute = TextHelper.GetTextFromImage(1426, 171, 16, 16);
            BotHelper.Log("Game is on minute " + minute.ToString());
            return minute;
        }

        public bool tryMoveLightArea(int X, int Y, string color) // Sometimes is better to not.
        {
            if(ImageHelper.GetColor(X, Y) == color)
            {
                InputHelper.RightClick(X, Y);
                BotHelper.InputIdle();
                return true;
            }
            return false;
        }
    }
}
