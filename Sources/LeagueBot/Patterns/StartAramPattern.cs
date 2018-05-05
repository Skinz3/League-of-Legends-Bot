using LeagueBot.Constants;
using LeagueBot.Patterns.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Patterns
{
    public class StartAramPattern : Pattern
    {
        public override string ProcessName => LeagueConstants.LoL_LAUNCHER_PROCESS;

        public override PatternAction[] Actions => new PatternAction[]
        {
            new ClickAction(ClickType.LEFT,PixelsConstants.PLAY_BUTTON,"Press Play Button",1),
            new ClickAction(ClickType.LEFT,PixelsConstants.PVP_MBUTTON,"Press Coop Against IA MButon",0.5),
            new ClickAction(ClickType.LEFT,PixelsConstants.ARAM_BUTTON,"Press Begginner Button",0.5),
            new ClickAction(ClickType.LEFT,PixelsConstants.CONFIRM_BUTTON,"Press Play Button",3),
            new ClickAction(ClickType.LEFT,PixelsConstants.CONFIRM_BUTTON,"Press Play Button",3),
            new ClickUntilColorAction(ColorConstants.CHOOSE_YOUR_LOADOUT_COLOR,PixelsConstants.CHOOSE_YOUR_LOADOUT_TEXT,PixelsConstants.ACCEPT_MATCH_BUTTON,"Finding match...",2),
            new WaitUntilProcessOpenAction(LeagueConstants.LoL_GAME_PROCESS,"Waiting LoL Process...",120,new Action(() => {  Bot.ApplyPattern(new StartAramPattern(Bot), 4); })), // timeout = if someone, leave the game before it start.
            new DefinePatternAction(new HowlingAbyssPattern(Bot),"Executing Pattern : InGame",0),
        };

        public StartAramPattern(Bot bot) : base(bot)
        {
        }



        public override void OnProcessClosed()
        {
            Console.WriteLine("League of Legends Launcher is closed! Press a key to exit");
            Console.Read();
            Environment.Exit(0);
        }
    }
}
