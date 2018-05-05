using InputManager;
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
    public class ClickUntilColorAction : PatternAction
    {
        Color Color
        {
            get;
            set;
        }
        Point Point
        {
            get;
            set;
        }
        Point ClickPoint
        {
            get;
            set;
        }
        public ClickUntilColorAction(Color color, Point point, Point clickPoint, string description, double duration = 0) : base(description, duration)
        {
            ClickPoint = clickPoint;
            Point = point;
            Color = color;
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
                    Mouse.Move(ClickPoint.X, ClickPoint.Y);
                    Mouse.PressButton(Mouse.MouseKeys.Left, 150);
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
