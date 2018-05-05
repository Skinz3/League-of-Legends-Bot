using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Constants
{
    public static class LeagueConstants
    {
        /// <summary>
        /// And probably compatible with other versions ;) 8.9+
        /// </summary>
        public const string LoL_VERSION = "8.9"; 

        public const string LoL_LAUNCHER_PROCESS = "LeagueClientUx";
        public const string LoL_GAME_PROCESS = "League of Legends";

    }
    public enum Lane
    {
        TOP,
        MID,
        BOT,
    }
    public enum LaneStructuresEnum
    {
        T1,
        T2,
        T3,
        Inibitor,
    }
    public enum Side
    {
        Blue,
        Red,
    }
}
