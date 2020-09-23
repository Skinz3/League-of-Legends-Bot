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
        public const string CLIENT_PROCESS_NAME = "LeagueClientUX";

        public const string GAME_PROCESS_NAME = "League of Legends";

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
