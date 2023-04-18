using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            if (Opened)
            {
                InputHelper.PressKey("Escape");
            }
            else
            {
                InputHelper.PressKey("P");
            }

            BotHelper.InputIdle();
            Opened = !Opened;
        }

        public void searchItem(string name)
        {
            InputHelper.KeyDown("LControlKey");
            InputHelper.KeyDown("L");
            InputHelper.KeyUp("LControlKey");
            InputHelper.KeyUp("L");

            BotHelper.InputIdle();

            InputHelper.InputWords(name);
            Logger.Write("Buyed " + name);
        }
        public void buySearchedItem()
        {
            BotHelper.InputIdle();

            InputHelper.PressKey("Enter");

            BotHelper.InputIdle();

            InputHelper.PressKey("Enter");

            BotHelper.InputIdle();
        }
    }
}
