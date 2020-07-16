using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Api;
using LeagueBot.ApiHelpers;
using LeagueBot.Game.Entities;
using LeagueBot.Image;
using LeagueBot.IO;

namespace LeagueBot.Game.Entities
{
    public class EnemyMinion : ApiMember<GameApi>
    {
        Api.Game game = new Api.Game();
        public EnemyMinion(GameApi api) : base(api)
        {

        }
        public int enemyCreepHealth()
        {
            int value;
            try
            {
                value = ImageValues.EnemyCreepHealth();
            }
            catch
            {
                value = 0;
            }
            return value;
        }

        public bool isThereAnEnemyCreep()
        {
            Point go = ImageValues.EnemyCreepPosition();

            if (go.X == 0 && go.Y == 0)
                return false;

            Logger.WritePixel($"Enemy creep has been found on [X: {go.X}, Y: {go.Y}] ~ isThereAnEnemyCreep()");
            return true;
        }
        

       
    }
}
