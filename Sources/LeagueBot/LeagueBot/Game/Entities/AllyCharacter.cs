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
    public class AllyCharacter : ApiMember<GameApi>
    {
        public AllyCharacter(GameApi api) : base(api)
        {

        }
        public bool isThereAnAlly()
        {
            Point go = ImageValues.AllyChampion();

            if (go.X == 0 && go.Y == 0)
                return false;

            Logger.WritePixel($"Ally character has been found on [X: {go.X}, Y: {go.Y}] ~ isThereAnAlly()");
            return true;
        }


    }
}
