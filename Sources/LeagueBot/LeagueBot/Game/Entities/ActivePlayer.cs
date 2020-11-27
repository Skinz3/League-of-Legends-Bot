using LeagueBot.Api;
using LeagueBot.ApiHelpers;
using LeagueBot.IO;
using System.Drawing;

namespace LeagueBot.Game.Entities
{
    public class ActivePlayer : ApiMember<GameApi>
    {
        public ActivePlayer(GameApi api) : base(api)
        {
        }

        public int CurrentGold => GameLCU.GetPlayerGolds();

        public double HealthPercentage
        {
            get
            {
                var stats = GameLCU.GetStats();
                return stats.currentHealth / stats.maxHealth;
            }
        }

        public bool IsDead => GameLCU.IsPlayerDead;

        public int Level => GameLCU.GetPlayerLevel();

        public string Name => GameLCU.GetPlayerName();

        public double ResourcePercentage
        {
            get
            {
                var stats = GameLCU.GetStats();
                return stats.resourceValue / stats.resourceMax;
            }
        }

        public IEntity GetNearTarget()
        {
            Color championLifebar = Color.FromArgb(203, 98, 88); // champion lifebar
            Color minionLifebar = Color.FromArgb(208, 94, 94); // minion lifebar

            var champion = ScreenHelper.GetColorPosition(championLifebar);

            if (champion != default)
                return new Champion(false, new Point(champion.Value.X + 50, champion.Value.Y + 60)); // lifebarposition + offset to find model
            else
            {
                var minion = ScreenHelper.GetColorPosition(minionLifebar);

                if (minion != default)
                    return new Minion(new Point(minion.Value.X + 30, minion.Value.Y + 40));
            }

            return default;
        }

        public dynamic GetStats() => GameLCU.GetStats();

        public void Recall()
        {
            InputHelper.PressKey("B");
            BotHelper.InputIdle();
        }

        public void TryCastSpellOnTarget(int index)
        {
            IEntity target = GetNearTarget();

            if (target == null ||
                target is Minion && index == 3 ||
                target is Minion && index == 4
                )
                return;

            var key = "D" + index;
            InputHelper.MoveMouse(target.Position.X, target.Position.Y);
            InputHelper.PressKey(key);
            BotHelper.InputIdle();
        }

        public void UpgradeSpell(int index) // <---- replace this by keybinding + league settings
        {
            Point coords;
            switch (index)
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
                    Logger.Write("Unknown spell indice :" + index, LogLevel.WARNING);
                    return;
            }

            InputHelper.LeftClick(coords.X, coords.Y);
            BotHelper.InputIdle();
        }

        public void UpgradeSpellOnLevelUp()
        {
            switch (Level)
            {
                case 1:
                    UpgradeSpell(1); // Q 1
                    break;

                case 2:
                    UpgradeSpell(2); // W 1
                    break;

                case 3:
                    UpgradeSpell(3); // E 1
                    break;

                case 4:
                    UpgradeSpell(1); // Q 2
                    break;

                case 5:
                    UpgradeSpell(1); // Q 3
                    break;

                case 6:
                    UpgradeSpell(4); // R 1
                    break;

                case 7:
                    UpgradeSpell(1); // Q 4
                    break;

                case 8:
                    UpgradeSpell(3); // E 2
                    break;

                case 9:
                    UpgradeSpell(1); // Q max
                    break;

                case 10:
                    UpgradeSpell(3); // E 3
                    break;

                case 11:
                    UpgradeSpell(4); // R 2
                    break;

                case 12:
                    UpgradeSpell(3); // E 4
                    break;

                case 13:
                    UpgradeSpell(3); // E max
                    break;

                case 14:
                    UpgradeSpell(2); // W 2
                    break;

                case 15:
                    UpgradeSpell(2); // W 3
                    break;

                case 16:
                    UpgradeSpell(4); // R max
                    break;

                case 17:
                    UpgradeSpell(2); // W 4
                    break;

                case 18:
                    UpgradeSpell(2); // W max
                    break;

                default:
                    //something not leveled?
                    UpgradeSpell(1);
                    UpgradeSpell(2);
                    UpgradeSpell(3);
                    UpgradeSpell(4);
                    break;
            }
        }
    }
}