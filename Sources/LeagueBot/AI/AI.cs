using LeagueBot.Constants;
using LeagueBot.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.AI
{
    public abstract class AbstractAI
    {
        protected DateTime GameStartTime
        {
            get;
            set;
        }

        protected TimeSpan GameTimespan
        {
            get
            {
                return DateTime.Now - GameStartTime;
            }
        }
        protected Bot Bot
        {
            get;
            private set;
        }
        protected Summoner Summoner
        {
            get;
            private set;
        }
        protected MapPattern Pattern
        {
            get;
            private set;
        }
        protected Side Side
        {
            get;
            private set;
        }
        public AbstractAI(Bot bot, MapPattern pattern)
        {
            this.Bot = bot;
            this.Pattern = pattern;
            this.Summoner = new Summoner(bot);
            this.Side = Pattern.GetSide();
        }
        public virtual void Start()
        {
            GameStartTime = DateTime.Now;
        }
        public abstract void Stop();
    }
}
