using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Patterns.Actions;
using LeagueBot.Constants;

namespace LeagueBot.Patterns
{
    public class StartPracticeGamePattern : Pattern
    {
        public override string ProcessName => LeagueConstants.LoL_LAUNCHER_PROCESS;

        public override PatternAction[] Actions => new PatternAction[]
        {
            new ClickAction(ClickType.LEFT,PixelsConstants.PLAY_BUTTON,"Press Play Button",1),
            new ClickAction(ClickType.LEFT,PixelsConstants.TRAINING_MBUTTON,"Press Training MButon",0.5),
            new ClickAction(ClickType.LEFT,PixelsConstants.PRACTICE_TOOL_BUTTON,"Press Training MButon",0.5),
            new ClickAction(ClickType.LEFT,PixelsConstants.CONFIRM_BUTTON,"Press Play Button",3),
            new ClickAction(ClickType.LEFT,PixelsConstants.CONFIRM_BUTTON,"Press Play Button",3),
            new ClickAction(ClickType.LEFT,PixelsConstants.CHAMP1_LOGO,"Select Champion",1),
            new ClickAction(ClickType.LEFT,PixelsConstants.LOCK_BUTTON,"Lock Champion",1),
            new WaitUntilProcessOpenAction(LeagueConstants.LoL_GAME_PROCESS,"Game is launching...",120,null),
            new DefinePatternAction(new SummonersRiftPattern(Bot),"Executing Pattern : InGame",0),
        };

        public StartPracticeGamePattern(Bot bot) : base(bot)
        {
        }



        public override void OnProcessClosed()
        {
          
        }
    }
}
