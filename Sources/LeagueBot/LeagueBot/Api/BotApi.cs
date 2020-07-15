using LeagueBot.ApiHelpers;
using LeagueBot.IO;
using LeagueBot.Patterns;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LeagueBot.Windows.Interop;
using LeagueBot.Patterns;

namespace LeagueBot.Api
{
    public class BotApi : IApi
    {
        //public int _outMaxTime = 360; // 6 minutes.
        //public int _outActualTime = 0;

        public void log(string message)
        {
            BotHelper.Log(message);
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
        public void waitProcessOpen(string processName)
        {
            while (!Interop.IsProcessOpen(processName))
            {
                BotHelper.Wait(1000);

                /*if(processName == "League of Legends") { 
                    _outActualTime++;

                    if(_outActualTime == _outMaxTime)
                    {
                        Logger.Write("Someone picked your champ... ", MessageState.WARNING);
                    }
                }*/
            }
        }
        public void inputWords(string words, int keyDelay = 50, int delay = 100)
        {
            InputHelper.InputWords(words, keyDelay, delay);
        }

        public void KillProcess(string processName)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName(processName))
                {
                    proc.Kill();
                    Logger.Write("Closing " + processName);
                }
            }
            catch (Exception ex)
            {
                Logger.Write("Cant handle " + processName + " to kill");
            }
        }

        public void StartProcess()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"C:\Riot Games\Riot Client\RiotClientServices.exe";
            startInfo.Arguments = @" --launch-product=league_of_legends --launch-patchline=live";
            Process.Start(startInfo);
        }
    }
}
