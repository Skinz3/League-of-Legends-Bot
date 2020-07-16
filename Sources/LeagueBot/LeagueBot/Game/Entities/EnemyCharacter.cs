using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Api;
using LeagueBot.ApiHelpers;
using LeagueBot.Image;
using LeagueBot.IO;

namespace LeagueBot.Game.Entities
{
    public class EnemyCharacter : ApiMember<GameApi>
    {
        Api.Game game = new Api.Game();
        public EnemyCharacter(GameApi api) : base(api)
        {

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

        public static bool isThereAnEnemy2()
        {
            Point go = ImageValues.EnemyChampion();

            if (go.X == 0 && go.Y == 0)
                return false;

            Logger.WritePixel($"Enemy character has been found on [X: {go.X}, Y: {go.Y}] ~ isThereAnEnemy()");
            InputHelper.PressKey("A");
            return true;
        }
    }
}
