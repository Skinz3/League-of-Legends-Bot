using LeagueBot.Game.Enums;
using LeagueBot.LCU;
using LeagueBot.LCU.Protocol;
using System;
using System.Reflection;
using System.Threading;

namespace LeagueBot.Api
{
    public class ClientApi : IApi
    {
        private Summoner summoner;

        public GameflowPhaseEnum GameflowPhase => ClientLCU.GetGameflowPhase();

        public bool IsMatchFound => ClientLCU.IsMatchFound();

        public Summoner Summoner
        {
            get
            {
                summoner = summoner ?? ClientLCU.GetCurrentSummoner();
                return summoner;
            }
        }

        public void AcceptMatch() => ClientLCU.AcceptMatch();

        public void CloseClient() => ClientLCU.CloseClient();

        public void CreateLobby(QueueEnum queueId) => ClientLCU.CreateLobby(queueId);

        public void Initialize() => ClientLCU.Initialize();

        public void OnGameEnd()
        {
            Program.GameCount++;

            Console.Title = Assembly.GetEntryAssembly().GetName().Name + " (" + Program.GameCount + " games)";
        }

        public void OpenClient() => ClientLCU.OpenClient();

        public ChampionPickResult PickChampion(ChampionEnum champion) => ClientLCU.PickChampion(summoner, champion);

        public SearchMatchResult SearchMatch() => ClientLCU.SearchMatch();

        public void WaitClientReady()
        {
            while (!ClientLCU.IsApiReady())
                Thread.Sleep(2000);
        }
    }
}