using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Api;
using LeagueBot.ApiHelpers;
using LeagueBot.Game.Enums;
using LeagueBot.IO;

namespace LeagueBot.Game.Misc
{
    public class Shop : ApiMember<GameApi>
    {
        private Dictionary<ShopItemTypeEnum, Point[]> ItemPositions = new Dictionary<ShopItemTypeEnum, Point[]>()
        {
            { ShopItemTypeEnum.Starting,  new Point[]{   new Point(580, 330), new Point(740, 330), new Point(940, 330) } },
            { ShopItemTypeEnum.Early,     new Point[]{   new Point(580, 440),new Point(740, 440), new Point(940, 440) } },
            { ShopItemTypeEnum.Essential, new Point[]{   new Point(580, 550), new Point(740, 550), new Point(940, 550)} },
            { ShopItemTypeEnum.Offensive, new Point[]{   new Point(580, 660), new Point(740, 660), new Point(940, 660) } },
            { ShopItemTypeEnum.Defensive, new Point[]{   new Point(580, 770), new Point(740, 770), new Point(940, 770), new Point(940, 770) } },

        };



        public bool Opened
        {
            get;
            set;
        }
        public List<Item> ItemsToBuy = new List<Item>();

        public Shop(GameApi api) : base(api)
        {
            this.Opened = false;
        }
        public void toogle()
        {
            InputHelper.PressKey("P");
            BotHelper.InputIdle();
            Opened = !Opened;
        }
        public Point getItemPosition(ShopItemTypeEnum type, int indice)
        {
            return ItemPositions[type][indice];
        }
        public void setItemBuild(List<Item> items)
        {
            if (ItemsToBuy != null)
                ItemsToBuy.Clear();

            foreach (Item _item in items)
            {
                ItemsToBuy.Add(_item);

                Logger.WriteInformation($"Added {_item.name} on items list", "SHOP");
            }
        }

        public int getPlayerGold()
        {
            return Api.player.getGold();
        }

        public void tryBuyItem()
        {
            if (ItemsToBuy != null)
            {
                foreach (Item _item in ItemsToBuy)
                {
                    BotHelper.Wait(1000);
                    if (_item.cost <= getPlayerGold())
                    {
                        if (_item.got == false)
                        {
                            Logger.WriteInformation($"Character bought {_item.name}.","SHOP");
                            InputHelper.RightClick(_item.point.X, _item.point.Y, 200);
                            _item.got = true;

                            BotHelper.Wait(500);
                            Logger.WriteInformation($"{getPlayerGold().ToString()} gold remaining.", "SHOP");
                            tryBuyItem();
                            BotHelper.Wait(500);
                        }
                    }
                }
            }
        }
        public void buyItem(int indice)
        {
            //INITIAL BUY
            Point coords = new Point(0, 0);

            switch (indice)
            {
                case 1:
                    coords = new Point(577, 337);
                    break;
                case 2:
                    coords = new Point(782, 336);
                    break;
                case 3:
                    coords = new Point(595, 557);
                    break;
                case 4:
                    coords = new Point(600, 665);
                    break;
                case 5:
                    coords = new Point(760, 540);
                    break;
                default:
                    Logger.Write("Unknown item indice " + indice + ". Skipping", MessageState.WARNING);
                    return;
            }
            InputHelper.RightClick(coords.X, coords.Y);

            BotHelper.InputIdle();
        }
    }
}
