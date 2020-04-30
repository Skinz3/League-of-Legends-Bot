using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Api;
using LeagueBot.ApiHelpers;

namespace LeagueBot.Game.Misc
{
    public class Camera : ApiMember<GameApi>
    {
        public bool Locked
        {
            get;
            set;
        }
        public Camera(GameApi api) : base(api)
        {
            this.Locked = false;
        }

        public void toggle()
        {
            InputHelper.LeftClick(1241, 920);
            BotHelper.InputIdle();
            Locked = !Locked;
        }
        public void lockAlly(int allyIndice)
        {
            string key = "F" + allyIndice;
            InputHelper.KeyUp(key);
            BotHelper.InputIdle();
            InputHelper.KeyDown(key);
            BotHelper.InputIdle();
        }

    }
}
