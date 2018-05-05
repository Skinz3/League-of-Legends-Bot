using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Constants;

namespace LeagueBot.Patterns.Actions
{
    public class MoveAction : ClickAction
    {
        public MoveAction(Point destination, string description, int duration = 0) : base(ClickType.RIGHT, destination, description, duration)
        {

        }
    }
}
