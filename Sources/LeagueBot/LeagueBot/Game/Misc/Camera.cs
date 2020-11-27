using LeagueBot.Api;
using LeagueBot.ApiHelpers;

namespace LeagueBot.Game.Misc
{
    public class Camera : ApiMember<GameApi>
    {
        public Camera(GameApi api) : base(api)
        {
        }

        public bool Locked { get; private set; }

        public void LockAlly(int allyIndex)
        {
            string key = "F" + allyIndex;
            InputHelper.KeyUp(key);
            BotHelper.InputIdle();
            InputHelper.KeyDown(key);
            BotHelper.InputIdle();
        }

        public void Toggle()
        {
            InputHelper.LeftClick(1241, 920);
            BotHelper.InputIdle();
            Locked = !Locked;
        }
    }
}