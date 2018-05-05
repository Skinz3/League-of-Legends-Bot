using LeagueBot.Constants;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueBot.Patterns.Actions
{
    public class WaitUntilColorAction : PatternAction
    {
        private Point Point
        {
            get;
            set;
        }
        private Color Color
        {
            get;
            set;
        }
        public WaitUntilColorAction(Point point, Color color, string description, double duration = 0) : base(description, duration)
        {
            this.Point = point;
            this.Color = color;
        }

        public override void Apply(Bot bot, Pattern pattern)
        {
            bool valid = false;
            while (!valid)
            {
                pattern.BringProcessToFront();
                pattern.CenterProcessMainWindow();
                var px = Interop.GetPixelColor(Point);
                if (px.R == Color.R && px.G == Color.G && px.B == Color.B)
                {
                    valid = true;
                }
                else
                {
                    Thread.Sleep(2000);
                }
            }

        }
    }
}
