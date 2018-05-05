using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueBot.Patterns.Actions
{
    public class WaitUntilProcessOpenAction : PatternAction
    {
        private string ProcessName
        {
            get;
            set;
        }
        private Action TimeoutCallback
        {
            get;
            set;
        }
        private int Timeout
        {
            get;
            set;
        }
      
        public WaitUntilProcessOpenAction(string processName, string description, int timeout, Action timeoutCallback, double duration = 0) : base(description, duration)
        {
            this.ProcessName = processName;
            this.TimeoutCallback = timeoutCallback;
            this.Timeout = timeout;
        }

        public override void Apply(Bot bot, Pattern pattern)
        {
            int x = 0;
            while (!Interop.IsProcessOpen(ProcessName))
            {
                Console.WriteLine("Wait for " + ProcessName + "...");
                Thread.Sleep(1000);
                x++;
                if (x == Timeout)
                {
                    TimeoutCallback();
                    break;
                }
            }
        }
    }
}
