using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Constants
{
    /// <summary>
    /// Pour que le BOT fonctionne, l'écran de l'ordinateur doit posséder une résolution de 1920 * 1080
    /// </summary>
    public static class PixelsConstants
    {
        public static Point GAME_RESOLUTION = new Point(1024, 768);

        #region Launcher
        public static Point PLAY_BUTTON = new Point(323, 138).Resize(GAME_RESOLUTION);

        public static Point COOP_AGAINST_AI_MBUTTON = new Point(350, 213).Resize(GAME_RESOLUTION);
        public static Point PVP_MBUTTON = new Point(242, 209).Resize(GAME_RESOLUTION);
        public static Point TRAINING_MBUTTON = new Point(470, 212).Resize(GAME_RESOLUTION);

        public static Point COOP_AGAINST_AI_BEGGINER = new Point(650, 750).Resize(GAME_RESOLUTION);
        public static Point COOP_AGAINST_AI_INTERMEDIATE = new Point(653, 782).Resize(GAME_RESOLUTION);
        public static Point PRACTICE_TOOL_BUTTON = new Point(1100, 369).Resize(GAME_RESOLUTION);

        public static Point ARAM_BUTTON = new Point(948, 355).Resize(GAME_RESOLUTION);

        public static Point CONFIRM_BUTTON = new Point(842, 943).Resize(GAME_RESOLUTION);
        public static Point ACCEPT_MATCH_BUTTON = new Point(956, 795).Resize(GAME_RESOLUTION);

        public static Point LEAVE_BUTTON = new Point(708, 752).Resize(GAME_RESOLUTION);
        public static Point HONOR_BUTTON = new Point(600, 600).Resize(GAME_RESOLUTION);

        public static Point LEVELUP_BUTTON = new Point(955, 937).Resize(GAME_RESOLUTION);
        #endregion

        #region Champion Select
        public static Point CHAMP1_LOGO = new Point(759, 307).Resize(GAME_RESOLUTION);
        public static Point CHAMP2_LOGO = new Point(885, 304).Resize(GAME_RESOLUTION);
        public static Point LOCK_BUTTON = new Point(957, 850).Resize(GAME_RESOLUTION);

        public static Point CHOOSE_YOUR_LOADOUT_TEXT = new Point(1165, 123).Resize(GAME_RESOLUTION);
        #endregion

        #region Game
        public static Point SUMMONER_1_SLOT = new Point(1008, 856).Resize(GAME_RESOLUTION);
        public static Point SUMMONER_2_SLOT = new Point(1047, 856).Resize(GAME_RESOLUTION);

        public static Point Q_SLOT = new Point(824, 856).Resize(GAME_RESOLUTION);
        public static Point Z_SLOT = new Point(867, 856).Resize(GAME_RESOLUTION);
        public static Point E_SLOT = new Point(912, 856).Resize(GAME_RESOLUTION);
        public static Point R_SLOT = new Point(966, 856).Resize(GAME_RESOLUTION);

        public static Point UP_Q_SLOT = new Point(821, 821).Resize(GAME_RESOLUTION);
        public static Point UP_Z_SLOT = new Point(862, 817).Resize(GAME_RESOLUTION);
        public static Point UP_E_SLOT = new Point(912, 818).Resize(GAME_RESOLUTION);
        public static Point UP_R_SLOT = new Point(961, 816).Resize(GAME_RESOLUTION);

        public static Point SHOP_BUTTON = new Point(1157, 923).Resize(GAME_RESOLUTION);
        public static Point SHOP_ITEM1 = new Point(504, 324).Resize(GAME_RESOLUTION);
        public static Point SHOP_ITEM2 = new Point(780, 318).Resize(GAME_RESOLUTION);

        public static Point LOCK_CAMERA_BUTTON = new Point(1242, 926).Resize(GAME_RESOLUTION);
        public static Point CLOSE_SHOP_BUTTON = new Point(1432, 190).Resize(GAME_RESOLUTION);

        public static Point BACK_BUTTON = new Point(1197, 894).Resize(GAME_RESOLUTION);

        public static Point MINIMAP_TOPLEFT_POINT = new Point(1290, 756).Resize(GAME_RESOLUTION);
        public static Point MINIMAP_BOTTOMRIGHT_POINT = new Point(1463, 930).Resize(GAME_RESOLUTION);

        public static Point LIFE_BAR_CHECKER_POINT = new Point(1017, 904).Resize(GAME_RESOLUTION);
        #endregion

        #region Summoner's Rift
        public static Point BLUESIDE_RED = new Point(1377, 878).Resize(GAME_RESOLUTION);
        public static Point BLUESIDE_WOLFS = new Point(1329, 849).Resize(GAME_RESOLUTION);
        public static Point BLUESIDE_GROMP = new Point(1308, 828).Resize(GAME_RESOLUTION);

        public static Point MIDLANE_MID = new Point(1474, 838).Resize(GAME_RESOLUTION);
        public static Point BOTLANE_BOT = new Point(1438, 899).Resize(GAME_RESOLUTION);

        public static Point BLUESIDE_BOTLANE_T1 = new Point(1414, 917).Resize(GAME_RESOLUTION);
        public static Point REDSITE_BOTLANE_T1 = new Point(1454, 847).Resize(GAME_RESOLUTION);
        #endregion

        #region Howling Abyss
        public static Point HOWLING_ABYSS_MID = new Point(1373, 842).Resize(GAME_RESOLUTION);
        public static Point HOWLING_ABYSS_BLUE_T1 = new Point(1355, 833).Resize(GAME_RESOLUTION);
        public static Point HOWLING_ABYSS_RED_T1 = new Point(1394, 793).Resize(GAME_RESOLUTION);
        public static Point HOWLING_ABYSS_BUSH1 = new Point(1356, 842).Resize(GAME_RESOLUTION);
        public static Point HOWLING_ABYSS_BUSH2 = new Point(1375, 804).Resize(GAME_RESOLUTION);

        public static Point HOWLING_ABYSS_REDNEXUS = new Point(1438, 775).Resize(GAME_RESOLUTION);
        public static Point HOWLING_ABYSS_BLUENEXUS = new Point(1309, 903).Resize(GAME_RESOLUTION);
        #endregion


        public static Point Resize(this Point point, Point resolutionScale)
        {
            int xScale = resolutionScale.X / GAME_RESOLUTION.X;
            int YScale = resolutionScale.Y / GAME_RESOLUTION.Y;
            return new Point(point.X * xScale, point.Y * YScale);
        }
    }
}
