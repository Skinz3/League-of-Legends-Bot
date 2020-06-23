using InputManager;
using LeagueBot.ApiHelpers;
using LeagueBot.Game.Entities;
using LeagueBot.Game.Enums;
using LeagueBot.Game.Misc;
using LeagueBot.Image;
using LeagueBot.IO;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueBot.Api
{
    /*
     * In Game Behaviour
     * -> Move to Nearest tower
     * -> Wait until minion spawn
     * -> Perpetual near entities analysis
     * -> Our objective is lane tower, once tower is destroyed, we go even futher
     * -> Perpetual state analysis States : {Agressive,Farming,Flee,Back,Walking to turret}
     * -> * Agressive if player is agressed and loose life fast
     * -> * Farming if NearChampions == 0 and NearMinions > 0
     * -> * Flee if life percentage is low
     * -> * We back after Flee
     * -> * Walk to our first turret
     */
    public class GameApi : IApi
    {
        public Shop shop
        {
            get;
            private set;
        }
        public Camera camera
        {
            get;
            private set;
        }
        public Chat chat
        {
            get;
            private set;
        }
        private SideEnum side
        {
            get;
            set;
        }
        public MainPlayer player
        {
            get;
            private set;
        }
        public GameApi()
        {
            this.shop = new Shop(this);
            this.camera = new Camera(this);
            this.chat = new Chat(this);
            this.player = new MainPlayer(this);
        }

        public void waitUntilGameStart()
        {
            bool gameStarted = false;
            string URL = "https://127.0.0.1:2999/liveclientdata/playerlist";

            while (!gameStarted)
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                try
                {
                    string json = new WebClient().DownloadString(URL);
                    if (json.Contains("ORDER"))
                    {
                        gameStarted = true;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(5000);
                    }
                }
                catch
                {
                    Thread.Sleep(5000);
                }
            }
        }

        public void detectSide()
        {
            //when side is detected, reset all bot items.
            this.side = ImageHelper.GetColor(1343, 868) == "#2A768C" ? SideEnum.Blue : SideEnum.Red;
        }
        public SideEnum getSide()
        {
            return this.side;
        }

        public void moveCenterScreen()
        {
            /*InputHelper.RightClick(886, 521);
            BotHelper.InputIdle();*/

            Random r = new Random();
            InputHelper.MoveMouse(970, 540);
            InputHelper.PressKey("A");
            BotHelper.Wait(r.Next(700, 800));
            InputHelper.RightClick(886, 521);
            BotHelper.Wait(r.Next(155, 175));
        }
    }
}
