using LeagueBot.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Patterns.Actions
{
    public class ExecuteAIAction : PatternAction
    {
        private AbstractAI AI
        {
            get;
            set;
        }
        public ExecuteAIAction(AbstractAI ai, string description, double duration = 0) : base(description, duration)
        {
            this.AI = ai;
        }

        public override void Apply(Bot bot, Pattern pattern)
        {
            var inGamePattern = ((MapPattern)pattern);
            inGamePattern.StartAI();
        }
    }
}
