using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Game.Modules;
namespace LeagueBot.Game
{
    class OffsetManager
    {
        public static int BaseAddress = Utils.GetLeagueProcess().MainModule.BaseAddress.ToInt32();

        public class Instances
        {
            //Version 10.14.327.1684 [PUBLIC] //8B 44 24 04 BA ? ? ? ? 2B D0	
            public static int LocalPlayer = BaseAddress + 0x3501F1C; //string xref blueHero -> Above "hero" subrtn 
            public static int Renderer = BaseAddress + 0x35290FC; //8B 15 ? ? ? ? 83 EC 08 F3 //["blurKernelSigma", +0x27F] // xref the string, move -0x27f there should be a dword.
            public static int ViewMatrix = BaseAddress + 0x3526418; //B9 ? ? ? ? E8 ? ? ? ? B9 ? ? ? ? E9 ? ? ? ?  //unk_0x....

            public static int SpellBook = 0x2AD0;

            public static int UnderMouseObject = BaseAddress + 0x34EF504; //8B 0D ? ? ? ? 89 0D //Detected?
        }

        public class Object
        {
            public static int ChampionName = 0x35AC;
            public static int Pos = 0x1D8;
            public static int Name = 0x6C;
            public static int Visibility = 0x39C;
            public static int AttackRange = 0x14A4;
        }
    }
}
