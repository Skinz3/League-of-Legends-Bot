using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Api;
using LeagueBot.ApiHelpers;
using LeagueBot.Game.Enums;
using LeagueBot.IO;

namespace LeagueBot.Game.Misc
{
    public class Shop : ApiMember<GameApi>
    {
        public bool Opened
        {
            get;
            set;
        }
        public Shop(GameApi api) : base(api)
        {
            this.Opened = false;
        }
        public void toogle()
        {
            InputHelper.PressKey("P");
            BotHelper.InputIdle();
            Opened = !Opened;
        }
    
        public int getPlayerGold()
        {
            return TextHelper.GetTextFromImage(767, 828, 118, 34);
        }

        public void buyItem(int indice)
        {
            Point coords = new Point(0, 0);

            switch (indice)
            {
                case 1:
                    coords = new Point(577, 337);
                    break;
                case 2:
                    coords = new Point(782, 336);
                    break;
                case 3:
                    coords = new Point(913, 345);
                    break;
                case 4:
                    coords = new Point(575, 455);
                    break;
                case 5:
                    coords = new Point(761, 449);
                    break;
                case 6:
                    coords = new Point(910, 457);
                    break;
                case 7:
                    coords = new Point(585, 563);
                    break;
                case 8:
                    coords = new Point(757, 573);
                    break;
                case 9:
                    coords = new Point(937, 565);
                    break;
                case 10:
                    coords = new Point(565, 681);
                    break;
                default:
                    Logger.Write("Unknown item indice " + indice + ". Skipping", MessageState.WARNING);
                    return;
            }
            InputHelper.RightClick(coords.X, coords.Y);

            BotHelper.InputIdle();
        }
    }
}
