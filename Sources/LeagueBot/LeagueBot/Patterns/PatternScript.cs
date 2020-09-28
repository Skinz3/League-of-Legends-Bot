using LeagueBot.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Patterns
{
    public abstract class PatternScript
    {
        public const string ClientProcessName = "LeagueClientUX";

        public const string ClientHostProcessName = "LeagueClient";

        public const string GameProcessName = "League of Legends";

        public const string ClientExecutablePath = @"League of Legends\LeagueClient.exe";

        public virtual bool ThrowException => true;

        public BotApi bot
        {
            protected get;
            set;
        }
        public GameApi game
        {
            protected get;
            set;
        }
        public ClientApi client
        {
            protected get;
            set;
        }

        public abstract void Execute();

        public virtual void End()
        {

        }
    }
}
