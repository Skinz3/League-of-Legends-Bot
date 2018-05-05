using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Patterns.Actions
{
    public abstract class PatternAction
    {
        private string Description
        {
            get;
            set;
        }
        public double Duration
        {
            get;
            set;
        }
        public PatternAction(string description, double duration = 0)
        {
            Description = description;
            Duration = duration;
        }
        public abstract void Apply(Bot bot,Pattern pattern);

        public override string ToString()
        {
            return string.Format("{0} Duration:{1}s", Description, Duration);
        }
    }
}
