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