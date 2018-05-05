using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueBot.Patterns.Actions
{
    public class WaitUntilProcessFocusAction : PatternAction
    {
        private string ProcessName
        {
            get;
            set;
        }
        public WaitUntilProcessFocusAction(string processName, string description, double duration = 0) : base(description, duration)
        {
            this.ProcessName = processName;
        }

        public override void Apply(Bot bot, Pattern pattern)
        {
            while (!Interop.IsProcessFocused(ProcessName))
            {
                Console.WriteLine("Wait focus from " + ProcessName + "...");
                Thread.Sleep(2000);
                try
                {
                    Interop.BringWindowToFront(ProcessName);
                }
                catch
                {
                }
            }
        }
    }
}
