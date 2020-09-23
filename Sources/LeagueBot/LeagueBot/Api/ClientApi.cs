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
        private Summoner Summoner
        {
            get;
            set;
        }
        public void loadSummoner()
        {
            this.Summoner = ClientLCU.GetCurrentSummoner();
            Logger.Write("Summoner loaded : " + Summoner.displayName);
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
        public void honorRandomPlayer()
        {
            ClientLCU.HonorRandomPlayer();
        }
        public bool isMatchFound()
        {
            return ClientLCU.IsMatchFounded();
        }

        public ChampionPickResult pickChampion(string name)
        {
            ChampionEnum champion = ChampionEnum.None;

            if (Enum.TryParse<ChampionEnum>(name, out champion) && champion != ChampionEnum.None)
            {
                return ClientLCU.PickChampion(Summoner, champion);
            }
            else
            {
                return ChampionPickResult.InvalidChampion;
            }

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
