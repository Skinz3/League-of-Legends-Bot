using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Game.Enums
{
    public enum GameflowPhaseEnum
    {
        None,
        Lobby,
        Matchmaking,
        ReadyCheck,
        ChampSelect,
        GameStart,
        FailedToLaunch,
        InProgress,
        Reconnect,
        WaitingForStats,
        EndOfGame,
        TerminatedInError
    }
}
