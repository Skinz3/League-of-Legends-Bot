using LeagueBot.ApiHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Api
{
    public class ClientApi : IApi
    {
        public void clickPlayButton()
        {
            InputHelper.LeftClick(306, 139);
        }
        public void clickAramButton()
        {
            InputHelper.LeftClick(787, 362);
        }
        public void clickCoopvsIAText()
        {
            InputHelper.LeftClick(336, 213);
        }
        public void clickIntroText()
        {
            InputHelper.LeftClick(733, 709);
        }
        public void clickIntermediateText()
        {
            InputHelper.LeftClick(755, 790);
        }
        public void clickConfirmButton()
        {
            InputHelper.LeftClick(832, 949);
        }
        public void clickFindMatchButton()
        {
            InputHelper.LeftClick(832, 949);
        }
        public void skipLevelRewards()
        {
            InputHelper.LeftClick(953, 938);
        }
        public void clickChampSearch()
        {
            InputHelper.LeftClick(1109, 219);
        }
        public void clearChampSearch()
        {
            InputHelper.LeftClick(1301, 219);
        }
        public bool blocked()
        {
            return TextHelper.TextExists(768, 934, 90, 24, "blocked");
        }
        public bool levelUp()
        {
            IO.Logger.WriteColor1(TextHelper.GetTextFromImage(872, 237, 300, 300));
            return TextHelper.TextExists(872, 237, 300, 300, "level up");
        }
        public bool questCompleted()
        {
            return TextHelper.TextExists(872, 237, 300, 300, "mission");
        }
        public bool mustSelectChamp()
        {
            return TextHelper.TextExists(692, 111, 512, 63, "choose your champion");
        }
        public void lockChampion()
        {
            InputHelper.LeftClick(959, 831);
        }
        public void acceptMatch()
        {
            InputHelper.LeftClick(947, 780);
        }
        public void selectFirstChampion()
        {
            InputHelper.LeftClick(645, 275);
        }
        public void skipHonor()
        {
            InputHelper.LeftClick(962, 903);
        }
        public void closeGameRecap()
        {
            InputHelper.LeftClick(716, 947);
        }
    }
}
