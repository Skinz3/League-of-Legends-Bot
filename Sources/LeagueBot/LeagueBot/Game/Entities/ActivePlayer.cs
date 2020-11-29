using LeagueBot.Api;
using LeagueBot.ApiHelpers;
using LeagueBot.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LeagueBot.Patterns;

namespace LeagueBot.Game.Entities
{
    public class ActivePlayer : ApiMember<GameApi>
    {
        public ActivePlayer(GameApi api) : base(api)
        {

        }

        public bool dead()
        {
            return GameLCU.IsPlayerDead();
        }

        public IEntity getNearTarget()
        {
            Color color = Color.FromArgb(203, 98, 88); // champion lifebar

            Color color2 = Color.FromArgb(208, 94, 94); // minion lifebar

            var target = ScreenHelper.GetColorPosition(color);

            if (target == null)
            {
                var minion = ScreenHelper.GetColorPosition(color2);

                if (minion == null)
                {
                    return null;
                }
                else
                {
                    return new Minion(new Point(minion.Value.X + 30, minion.Value.Y + 40));
                }
            }
            else
            {
                return new Champion(false, new Point(target.Value.X + 50, target.Value.Y + 60)); // lifebarposition + offset to find model
            }
        }

        public void tryCastSpellOnTarget(int indice)
        {
            IEntity target = getNearTarget();

            if (target == null)
                return;

            if (target is Minion && indice == 3)
            {
                return;
            }
            if (target is Minion && indice == 4)
            {
                return;
            }
            string key = "D" + indice;
            InputHelper.MoveMouse(target.Position.X, target.Position.Y);
            InputHelper.PressKey(key);
            BotHelper.InputIdle();
        }

        public void upgradeSpell(int indice) // <---- replace this by keybinding + league settings
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
        public int getLevel()
        {
            return GameLCU.GetPlayerLevel();
        }
        public int getGolds()
        {
            return GameLCU.GetPlayerGolds();
        }
        public string getName()
        {
            return GameLCU.GetPlayerName();
        }
        public void upgradeSpellOnLevelUp()
        {
            int level = getLevel();

            switch (level)
            {
                case 1:
                    Logger.Write("[Q] Level up!");
                    upgradeSpell(1); // Q 1
                    
                    break;
                case 2:
                    Logger.Write("[W] Level up!");
                    upgradeSpell(2); // W 1
                    
                    break;
                case 3:
                    upgradeSpell(3); // E 1
                    Logger.Write("[E] Level up!");
                    break;
                case 4:
                    upgradeSpell(1); // Q 2
                    Logger.Write("[Q] Level up!");
                    break;
                case 5:
                    upgradeSpell(1); // Q 3
                    Logger.Write("[Q] Level up!");
                    break;
                case 6:
                    upgradeSpell(4); // R 1
                    Logger.Write("[R] Level up!");
                    break;
                case 7:
                    upgradeSpell(1); // Q 4
                    Logger.Write("[Q] Level up!");
                    break;
                case 8:
                    upgradeSpell(3); // E 2
                    Logger.Write("[E] Level up!");
                    break;
                case 9:
                    upgradeSpell(1); // Q max
                    Logger.Write("[Q] Level up!");
                    break;
                case 10:
                    upgradeSpell(3); // E 3
                    Logger.Write("[E] Level up!");
                    break;
                case 11:
                    upgradeSpell(4); // R 2
                    Logger.Write("[R] Level up!");
                    break;
                case 12:
                    upgradeSpell(3); // E 4
                    Logger.Write("[E] Level up!");
                    break;
                case 13:
                    upgradeSpell(3); // E max
                    Logger.Write("[E] Level up!");
                    break;
                case 14:
                    upgradeSpell(2); // W 2
                    Logger.Write("[W] Level up!");
                    break;
                case 15:
                    upgradeSpell(2); // W 3
                    Logger.Write("[W] Level up!");
                    break;
                case 16:
                    upgradeSpell(4); // R max
                    Logger.Write("[R] Level up!");
                    break;
                case 17:
                    upgradeSpell(2); // W 4
                    Logger.Write("[W] Level up!");
                    break;
                case 18:
                    upgradeSpell(2); // W max
                    Logger.Write("[W] Level up!");
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

        public dynamic getStats()
        {
            return GameLCU.GetStats();
        }

        public void recall()
        {
            InputHelper.PressKey("B");
            BotHelper.InputIdle();
        }
        public double getHealthPercent()
        {
            var stats = getStats();
            return stats.currentHealth / stats.maxHealth;
        }
        public double getManaPercent()
        {
            var stats = getStats();
            return stats.resourceValue / stats.resourceMax;
        }
    }
}
