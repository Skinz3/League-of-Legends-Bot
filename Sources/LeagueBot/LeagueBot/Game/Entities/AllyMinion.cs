using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Api;
using LeagueBot.ApiHelpers;
using LeagueBot.Image;
using LeagueBot.IO;

namespace LeagueBot.Game.Entities
{
    public class AllyMinion : ApiMember<GameApi>
    {
        Api.Game game = new Api.Game();
        public AllyMinion(GameApi api) : base(api)
        {

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

        public static bool isThereAnAllyCreep2()
        {
            Point go = ImageValues.AllyCreepPosition();

            if (go.X == 0 && go.Y == 0)
                return false;

            return true;
        }

    }


}
