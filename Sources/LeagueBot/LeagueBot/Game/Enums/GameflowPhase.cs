namespace LeagueBot.Game.Enums
{
    public enum GameflowPhase
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