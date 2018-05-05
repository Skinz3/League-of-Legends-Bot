using LeagueBot.Constants;
using LeagueBot.Patterns.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Patterns
{
    public class StartCoopAgainstIAPattern : Pattern
    {
        public override string ProcessName => LeagueConstants.LoL_LAUNCHER_PROCESS;

        public override PatternAction[] Actions => new PatternAction[]
        {
            new ClickAction(ClickType.LEFT,PixelsConstants.PLAY_BUTTON,"Press Play Button",1),
            new ClickAction(ClickType.LEFT,PixelsConstants.COOP_AGAINST_AI_MBUTTON,"Press Coop Against IA MButon",0.5),
            new ClickAction(ClickType.LEFT,PixelsConstants.COOP_AGAINST_AI_BEGGINER,"Press Begginner Button",0.5),
            new ClickAction(ClickType.LEFT,PixelsConstants.CONFIRM_BUTTON,"Press Play Button",3),
            new ClickAction(ClickType.LEFT,PixelsConstants.CONFIRM_BUTTON,"Press Play Button",3),
        };

        public StartCoopAgainstIAPattern(Bot bot) : base(bot)
        {
        }



        public override void OnProcessClosed()
        {

        }
    }
}

