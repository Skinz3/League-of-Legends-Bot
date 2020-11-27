using LeagueBot.Api;

namespace LeagueBot.Patterns
{
    public abstract class PatternScript
    {
        public BotApi bot
        {
            protected get;
            set;
        }

        public ClientApi client
        {
            protected get;
            set;
        }

        public GameApi game
        {
            protected get;
            set;
        }

        public virtual bool ThrowException => true;

        public virtual void End()
        {
        }

        public abstract void Execute();
    }
}