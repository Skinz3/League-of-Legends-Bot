using LeagueBot.ApiHelpers;
using LeagueBot.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Leaf.xNet;
using System.Threading;
using LeagueBot.Patterns;
using LeagueBot.LCU;
using LeagueBot.Game;
using LeagueBot.Game.Enums;
using LeagueBot.LCU.Protocol;

namespace LeagueBot.Api
{
    public class ClientApi : IApi
    {
        public Summoner summoner
        {
            get;
            private set;
        }
        public bool loadSummoner()
        {
            this.summoner = ClientLCU.GetCurrentSummoner();
            return summoner != null;
        }
        public void createLobby(QueueEnum queueId)
        {
            ClientLCU.CreateLobby(queueId);
        }
        public SearchMatchResult searchMatch()
        {
            return ClientLCU.SearchMatch();
        }
        public void acceptMatch()
        {
            ClientLCU.AcceptMatch();
        }
        public bool isMatchFound()
        {
            return ClientLCU.IsMatchFounded();
        }

        public ChampionPickResult pickChampion(ChampionEnum champion)
        {
            return ClientLCU.PickChampion(summoner, champion);
        }
        public bool isInChampSelect()
        {
            return ClientLCU.IsInChampSelect();
        }

        public void onGameEnd()
        {
            Program.GameCount++;

            Console.Title = Assembly.GetEntryAssembly().GetName().Name + " (" + Program.GameCount + " games)";
        }

    }
}
