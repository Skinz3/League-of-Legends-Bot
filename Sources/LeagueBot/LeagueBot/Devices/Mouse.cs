using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using LeagueBot.Game.Modules;

namespace LeagueBot.Devices
{
    class Mouse
    {
        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public static void MoveTo(int x, int y)
        {
            NativeImport.mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, (uint)x, (uint)y, 0, 0);
        }

        internal static void MouseMove(int x, int y)
        {
            NativeImport.SetCursorPos(x, y);
        }

        internal static void MouseMoveRelative(int xOffset, int yOffset)
        {
            NativeImport.SetCursorPos(Cursor.Position.X + xOffset, Cursor.Position.Y + yOffset);
        }

        private static void MouseEvent(uint mouseEvent, uint x, uint y)
        {
            Thread.Sleep(100);
            NativeImport.mouse_event(mouseEvent, x, y, 0, 0);
        }

        /// <summary>
        /// Holds down the mouse left button.
        /// </summary>

        internal static void MouseLeftDown()
        {
            MouseEvent(MOUSEEVENTF_LEFTDOWN, (uint)Cursor.Position.X, (uint)Cursor.Position.Y);
        }

        /// <summary>
        /// Releases the mouse left button.
        /// </summary>

        internal static void MouseLeftUp()
        {
            MouseEvent(MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y);
        }

        /// <summary>
        /// Performs a mouse left click.
        /// </summary>

        internal static void MouseClickLeft()
        {
            MouseLeftDown();
            MouseLeftUp();
        }

        /// <summary>
        /// Holds down the mouse right button.
        /// </summary>

        internal static void MouseRightDown()
        {
            MouseEvent(MOUSEEVENTF_RIGHTDOWN, (uint)Cursor.Position.X, (uint)Cursor.Position.Y);
        }

        /// <summary>
        /// Releases the mouse right button.
        /// </summary>

        internal static void MouseRightUp()
        {
            MouseEvent(MOUSEEVENTF_RIGHTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y);
        }

        /// <summary>
        /// Performs a mouse right click.
        /// </summary>

        internal static void MouseClickRight()
        {
            MouseRightDown();
            MouseRightUp();
        }
    }
}
