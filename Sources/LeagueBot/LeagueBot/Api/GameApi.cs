using LeagueBot.ApiHelpers;
using LeagueBot.Game.Entities;
using LeagueBot.Game.Enums;
using LeagueBot.Game.Misc;
using System.Threading;

namespace LeagueBot.Api
{
    /*
     * In Game Behaviour
     * -> Move to Nearest tower
     * -> Wait until minion spawn
     * -> Perpetual near entities analysis
     * -> Our objective is lane tower, once tower is destroyed, we go even futher
     * -> Perpetual state analysis States : {Agressive,Farming,Flee,Back,Walking to turret}
     * -> * Agressive if player is agressed and loose life fast
     * -> * Farming if NearChampions == 0 and NearMinions > 0
     * -> * Flee if life percentage is low
     * -> * We back after Flee
     * -> * Walk to our first turret
     */

    public class GameApi : IApi // Teams[] CameraIndex PlayerIndex
    {
        private TeamSide? side;

        public GameApi()
        {
            Shop = new Shop(this);
            Camera = new Camera(this);
            Chat = new Chat(this);
            Player = new ActivePlayer(this);
        }

        public Camera Camera { get; }

        public Chat Chat { get; }

        public ActivePlayer Player { get; }

        public Shop Shop { get; }

        private TeamSide Side
        {
            get
            {
                side = side ?? GameLCU.GetPlayerSide();
                return side.Value;
            }
        }

        public dynamic GetAllies() => GameLCU.GetAllies();

        public int GetAllyToFollowId()
        {
            var startIndex = 2;

            int maxKils = -1;
            int index = startIndex;

            int i = index;
            foreach (var ally in GetAllies())
            {
                if (i - startIndex == 4)
                    break;

                if (ally.summonerName != Player.Name &&
                    !ally.isDead &&
                    !HasSmite(ally) &&
                    ally.scores.kills > maxKils
                    )
                {
                    maxKils = ally.scores.kills;
                    index = i;
                }
                i++;
            }

            return index;
        }

        public void MoveCenterScreen()
        {
            InputHelper.RightClick(886, 521);
            BotHelper.InputIdle();
        }

        public void WaitForGameStart()
        {
            while (!(GameLCU.IsApiReady() && GameLCU.GetGameTime() > 2d))
                Thread.Sleep(2000);
        }

        private bool HasSmite(dynamic ally)
        {
            return ally.summonerSpells.summonerSpellOne.displayName.ToString().ToLower().Contains("smite") ||
                    ally.summonerSpells.summonerSpellTwo.displayName.ToString().ToLower().Contains("smite");
        }
    }
}