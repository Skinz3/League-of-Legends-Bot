using InputManager;
using LeagueBot.Constants;
using LeagueBot.Patterns;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot
{
    public class Bot
    {
        private Pattern Pattern
        {
            get;
            set;
        }
        public AvailableGameType GameType
        {
            get;
            private set;
        }
      
        public Bot()
        {
          
        }
        public void Start(AvailableGameType gameType)
        {
            GameType = gameType;

            switch (gameType)
            {
                case AvailableGameType.COOP_AGAINST_IA_BEGGINER:
                    ApplyPattern(new StartCoopAgainstIAPattern(this));
                    break;
                case AvailableGameType.COOP_AGAINST_IA_INTERMEDIATE:
                    break;
                case AvailableGameType.PRACTICE_TOOL:
                    ApplyPattern(new StartPracticeGamePattern(this));
                    break;
                case AvailableGameType.IN_GAME_DIRECT_SUMMONERSRIFT:
                    ApplyPattern(new SummonersRiftPattern(this));
                    break;
                case AvailableGameType.IN_GAME_DIRECT_HOWLINGABYSS:
                    ApplyPattern(new HowlingAbyssPattern(this));
                    break;
                case AvailableGameType.ARAM:
                    ApplyPattern(new StartAramPattern(this));
                    break;
            }
        }
        public void ApplyPattern(Pattern p,int i = 0)
        {
            Pattern?.Dispose();
            Pattern = p;
            Pattern.Execute(i);
        }
        public void RightClick(Point point)
        {
            Mouse.Move(point.X, point.Y);
            Mouse.PressButton(Mouse.MouseKeys.Right, 150);
        }
        public void LeftClick(Point point)
        {
            Mouse.Move(point.X, point.Y);
            Mouse.PressButton(Mouse.MouseKeys.Left, 150);
        }
    }
}
