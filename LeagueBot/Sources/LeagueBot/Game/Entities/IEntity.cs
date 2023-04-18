using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Game.Entities
{
    public interface IEntity
    {
        Point Position
        {
            get;
        }
    }
}
