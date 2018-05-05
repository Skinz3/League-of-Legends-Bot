using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Windows
{
    public class ApplicationCapture
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private string ProcessName
        {
            get;
            set;
        }
        private Process Process
        {
            get;
            set;
        }
        public ApplicationCapture(string processName)
        {
            this.ProcessName = processName;
            this.Process = Process.GetProcessesByName(processName).FirstOrDefault();

            if (Process == null)
            {
                throw new Exception("Unable to find process : " + processName);
            }
        }
        public Bitmap Capture()
        {
            Process.WaitForInputIdle();
            if (SetForegroundWindow(Process.MainWindowHandle))
            {
                RECT srcRect;
                if (!Process.MainWindowHandle.Equals(IntPtr.Zero))
                {
                    if (GetWindowRect(Process.MainWindowHandle, out srcRect))
                    {
                        int width = srcRect.Right - srcRect.Left;
                        int height = srcRect.Bottom - srcRect.Top;

                        Bitmap bmp = new Bitmap(width, height);
                        Graphics screenG = Graphics.FromImage(bmp);


                        screenG.CopyFromScreen(srcRect.Left, srcRect.Top,
                                0, 0, new Size(width, height),
                                CopyPixelOperation.SourceCopy);

                        bmp.Save("notepad.jpg", ImageFormat.Jpeg);
                        
                        screenG.Dispose();
                        return bmp;
                    }
                }
            }
            throw new Exception("Unable to capture image!");

        }
    }
}
