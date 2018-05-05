using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Patterns.Actions;
using LeagueBot.Constants;

namespace LeagueBot.Patterns
{
    public class EndGamePattern : Pattern
    {
        public EndGamePattern(Bot bot) : base(bot)
        {
        }

        public override string ProcessName => LeagueConstants.LoL_LAUNCHER_PROCESS;

        public override PatternAction[] Actions => new PatternAction[]
        {
            new ClickAction(ClickType.LEFT, PixelsConstants.HONOR_BUTTON,"Wp Mates!",5),
            new ClickAction(ClickType.LEFT, PixelsConstants.LEVELUP_BUTTON,"LevelUp!",5),
            new ClickAction(ClickType.LEFT, PixelsConstants.LEAVE_BUTTON, "Leave Game",3),
            new DefinePatternAction(new StartAramPattern(Bot),"Executing Pattern : Aram",0)
        };

        public override void OnProcessClosed()
        {
            throw new NotImplementedException();
        }
    }
}
