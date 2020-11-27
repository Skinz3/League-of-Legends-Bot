using LeagueBot.ApiHelpers;
using LeagueBot.IO;
using LeagueBot.Patterns;
using LeagueBot.Windows;
using System;
using System.Diagnostics;
using System.Linq;

namespace LeagueBot.Api
{
    public class BotApi : IApi
    {
        public void BringProcessToFront(string processName)
        {
            if (!Interop.BringWindowToFront(processName))
                Logger.Write("Unable to bring process to front: " + processName, LogLevel.WARNING);
        }

        public void CenterProcess(string processName)
        {
            if (!Interop.CenterProcessWindow(processName))
                Logger.Write("Unable to center process: " + processName, LogLevel.WARNING);
        }

        public void ExecutePattern(string name) => PatternsManager.Execute(name);

        public void InputWords(string words, int keyDelay = 50, int delay = 100) => InputHelper.InputWords(words, keyDelay, delay);

        public bool IsProcessOpen(string processName) => Process.GetProcessesByName(processName).Length > 0;

        public void LogError(object message) => Logger.Write(message, LogLevel.ERROR); // TODO: Pretty redundant, just extend LogMessage

        public void LogMessage(object message) => Logger.Write(message);

        public void LogWarning(object message) => Logger.Write(message, LogLevel.WARNING);

        public void Sleep(int ms) => BotHelper.Sleep(ms);

        public void WaitProcessOpen(string processName, Action timeoutCallback = null, int timeout = 20)
        {
            var stopwatch = Stopwatch.StartNew();
            while (!Interop.IsProcessOpen(processName))
            {
                if (stopwatch.Elapsed.TotalSeconds > timeout)
                {
                    timeoutCallback?.Invoke();
                    return;
                }
                BotHelper.Sleep(1000);
            }
        }

        public void WaitUntilProcessBounds(string processName, int boundsX, int boundsY)
        {
            int width = 0;
            int height = 0;

            while (width != boundsX && height != boundsY)
            {
                var process = Process.GetProcessesByName(processName).FirstOrDefault();

                if (process == null)
                    throw new Exception("Process " + process + " cannot be found.");

                Interop.GetWindowRect(process.MainWindowHandle, out var rect);

                width = rect.Right - rect.Left;
                height = rect.Bottom - rect.Top;

                BotHelper.Sleep(1000);
            }
        }
    }
}