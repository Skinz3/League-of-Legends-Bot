using InputManager;
using LeagueBot.IO;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LeagueBot.Windows.Interop;

namespace LeagueBot.Patterns
{
    public class BotApi
    {
        private PatternScript Script
        {
            get;
            set;
        }
        public void keyUp(string key)
        {
            Keyboard.KeyUp((Keys)Enum.Parse(typeof(Keys), key));
        }
        public void keyDown(string key)
        {
            Keyboard.KeyDown((Keys)Enum.Parse(typeof(Keys), key));
        }
        public void pressKey(string key)
        {
            Keyboard.KeyPress((Keys)Enum.Parse(typeof(Keys), key), 150);
        }
        public void moveMouse(int x, int y)
        {
            Mouse.Move(x, y);
        }
        public void rightClick(int x, int y)
        {
            Mouse.Move(x, y);
            Mouse.PressButton(Mouse.MouseKeys.Right, 150);
        }
        public void leftClick(int x, int y)
        {
            Mouse.Move(x, y);
            Mouse.PressButton(Mouse.MouseKeys.Left, 150);
        }
        public string getColor(int x, int y)
        {
            return ColorTranslator.ToHtml(Color.FromArgb(Interop.GetPixelColor(new Point(x, y)).ToArgb()));
        }
        public void waitForColor(int x, int y, string colorHex)
        {
            Color color = Interop.GetPixelColor(new Point(x, y));

            while (ColorTranslator.ToHtml(Color.FromArgb(color.ToArgb())) != colorHex)
            {
                color = Interop.GetPixelColor(new Point(x, y));
                Thread.Sleep(1000);
            }
        }
        public void waitUntilProcessBounds(string processName, int boundsX, int boundsY)
        {
            RECT rect = new RECT();

            int width = 0;
            int height = 0;

            while (width != boundsX && height != boundsY)
            {
                var process = Process.GetProcessesByName(processName).FirstOrDefault();

                if (process == null)
                {
                    throw new Exception("Process " + process + " cannot be found.");
                }

                Interop.GetWindowRect(process.MainWindowHandle, out rect);

                width = rect.Right - rect.Left;
                height = rect.Bottom - rect.Top;

                Thread.Sleep(1000);

            }
        }
        public bool isProcessOpen(string processName)
        {
            return Process.GetProcessesByName(processName).Length > 0;
        }
        public void centerProcess(string processName)
        {
            Interop.CenterProcessWindow(processName);
        }
        public void bringProcessToFront(string processName)
        {
            Interop.BringWindowToFront(processName);
        }
        public void waitProcessOpen(string processName)
        {
            while (!Interop.IsProcessOpen(processName))
            {
                Thread.Sleep(1000);
            }

        }
        public void executePattern(string filename)
        {
            PatternsManager.Execute(filename);
        }
        public void log(string message)
        {
            Logger.Write(message, MessageState.INFO2);
        }
        public void wait(int ms)
        {
            Thread.Sleep(ms);
        }

    }
}
