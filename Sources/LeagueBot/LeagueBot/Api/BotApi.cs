using LeagueBot.ApiHelpers;
using LeagueBot.IO;
using LeagueBot.LCU;
using LeagueBot.Patterns;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LeagueBot.Windows.Interop;

namespace LeagueBot.Api
{
    public class BotApi : IApi
    {
        public void log(object message)
        {
            Logger.Write(message);
        }
        public void warn(object message)
        {
            Logger.Write(message, MessageState.WARNING);
        }
        public void error(object message)
        {
            Logger.Write(message, MessageState.ERROR);
        }
        public void wait(int ms)
        {
            BotHelper.Wait(ms);
        }
        public void executePattern(string name)
        {
            PatternsManager.Execute(name);
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

                BotHelper.Wait(1000);

            }
        }
        public bool isProcessOpen(string processName)
        {
            return Process.GetProcessesByName(processName).Length > 0;
        }
        public void centerProcess(string processName)
        {
            if (!Interop.CenterProcessWindow(processName))
            {
                Logger.Write("Unable to center process: " + processName, MessageState.WARNING);
            }
        }
        public void bringProcessToFront(string processName)
        {
            if (!Interop.BringWindowToFront(processName))
            {
                Logger.Write("Unable to bring process to front: " + processName, MessageState.WARNING);
            }
        }
        public void waitProcessOpen(string processName, Action timeoutCallback = null, int timeout = 20)
        {
            Stopwatch st = Stopwatch.StartNew();

            while (!Interop.IsProcessOpen(processName))
            {
                if (timeoutCallback != null)
                {
                    if (st.Elapsed.TotalSeconds > timeout)
                    {
                        timeoutCallback();
                        return;
                    }

                }
                BotHelper.Wait(1000);
            }
        }
        public void inputWords(string words, int keyDelay = 50, int delay = 100)
        {
            InputHelper.InputWords(words, keyDelay, delay);
        }
    }
}
