using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Game.Modules;
using LeagueBot.Game.Enums;
using LeagueBot.Devices;
using System.Drawing;
using System.Windows.Forms;

namespace LeagueBot.Game
{
    class Engine
    {
        public static int GetLocalPlayer { get; } = Memory.Read<int>(OffsetManager.Instances.LocalPlayer);

        public static float GetGameTime()
        {
            return Api.GameStats.GetGameTime();
        }

        public static int GetObjectUnderMouse() //Use at risk [Possible Detection?]
        {
            return Memory.Read<int>(OffsetManager.Instances.UnderMouseObject);
        }

        public static void IssueOrder(GameObjectOrder Order, Point Vector2D = new Point())
        {
            if (Utils.IsGameOnDisplay())
            {
                switch (Order)
                {
                    case GameObjectOrder.HoldPosition:
                        Keyboard.SendKey((short)Keyboard.KeyBoardScanCodes.KEY_S);
                        break;
                    case GameObjectOrder.MoveTo:
                        if (Vector2D.X == 0 && Vector2D.Y == 0)
                        {
                            Mouse.MouseClickRight();
                            break;
                        }
                        if (Vector2D == new Point(Cursor.Position.X, Cursor.Position.Y))
                        {
                            Mouse.MouseClickRight();
                            break;
                        }
                        Mouse.MouseMove(Vector2D.X, Vector2D.Y);
                        Mouse.MouseClickRight();
                        break;
                    case GameObjectOrder.AttackUnit:
                        if (Vector2D.X == 0 && Vector2D.Y == 0)
                        {
                            Mouse.MouseMove(Cursor.Position.X, Cursor.Position.Y);
                            Mouse.MouseClickRight();
                            break;
                        }
                        Mouse.MouseMove(Vector2D.X, Vector2D.Y);
                        Mouse.MouseClickRight();
                        break;
                    case GameObjectOrder.AutoAttack:
                        Keyboard.SendKey((short)Keyboard.KeyBoardScanCodes.KEY_OPENING_BRACKETS);
                        break;
                    case GameObjectOrder.Stop:
                        Keyboard.SendKey((short)Keyboard.KeyBoardScanCodes.KEY_S);
                        break;
                }
            }
        }
    }
}
