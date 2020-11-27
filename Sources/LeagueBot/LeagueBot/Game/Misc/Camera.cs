using LeagueBot.Api;
using LeagueBot.ApiHelpers;

namespace LeagueBot.Game.Misc
{
    public class Camera : ApiMember<GameApi>
    {
        public Camera(GameApi api) : base(api)
        {
            this.Locked = false;
        }

        public bool Locked
        {
            get;
            set;
        }

        public void lockAlly(int allyIndice)
        {
            string key = "F" + allyIndice;
            InputHelper.KeyUp(key);
            BotHelper.InputIdle();
            InputHelper.KeyDown(key);
            BotHelper.InputIdle();
        }

        public void toggle()
        {
            InputHelper.LeftClick(1241, 920);
            BotHelper.InputIdle();
            Locked = !Locked;
        }
    }
}