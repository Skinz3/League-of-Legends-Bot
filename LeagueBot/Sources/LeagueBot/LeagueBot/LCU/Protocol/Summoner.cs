using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.LCU.Protocol
{
    public class Summoner
    {
        public long accountId;

        public string displayName;

        public string internalName;

        public bool nameChangeFlag;

        public byte percentCompleteForNextLevel;

        public short profileIconId;

        public string puuid;

        public RerollPoints RerollPoints;

        public long summonerId;

        public short summonerLevel;

        public bool unnamed;

        public int xpSinceLastLevel;

        public int xpUntilNextLevel;

    }
    public class RerollPoints
    {
        public int currentPoints;

        public int maxRolls;

        public int numberOfRolls;

        public int pointsCostToRoll;

        public int pointsToReroll;
    }
}
