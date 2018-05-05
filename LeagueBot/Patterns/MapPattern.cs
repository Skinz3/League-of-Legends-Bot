using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Patterns.Actions;
using LeagueBot.AI;
using LeagueBot.Constants;
using System.Drawing;
using System.Threading;
using LeagueBot.Windows;

namespace LeagueBot.Patterns
{
    public abstract class MapPattern : Pattern
    {
        public abstract AbstractAI AI
        {
            get;
        }

        public MapPattern(Bot bot) : base(bot)
        {
        }


        public override string ProcessName => LeagueConstants.LoL_GAME_PROCESS;

        public override PatternAction[] Actions => new PatternAction[]
        {
           new WaitUntilProcessFocusAction(LeagueConstants.LoL_GAME_PROCESS,"Waiting Game Focus..."),
           new WaitUntilColorAction(PixelsConstants.LIFE_BAR_CHECKER_POINT,ColorConstants.LIFE_BAR_CHECKER_COLOR,"Waiting for Game to load..."),
           new ExecuteAIAction(AI,"Launching'IA ("+AI.GetType().Name+")"),
        };

        public abstract Side GetSide();

        /// <summary>
        /// Appellée par ExecuteAIAction.cs
        /// </summary>
        public void StartAI()
        {
            AI.Start();
        }
        public override void OnProcessClosed()
        {
            AI.Stop();
            Thread.Sleep(8000);
            Bot.ApplyPattern(new EndGamePattern(Bot));
            base.OnProcessClosed();
        }
    }
}
