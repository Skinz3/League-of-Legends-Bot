using LeagueBot.Patterns.Actions;
using LeagueBot.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueBot.Patterns
{
    public abstract class Pattern
    {
        public virtual string ProcessName
        {
            get;
        }
        public virtual PatternAction[] Actions
        {
            get;
        }
        private int I
        {
            get;
            set;
        }
        protected Bot Bot
        {
            get;
            private set;
        }
        public Pattern(Bot bot)
        {
            this.Bot = bot;
        }
        protected bool Disposed
        {
            get;
            private set;
        }
        public virtual void Initialize()
        {
            BringProcessToFront();
        }
        public void BringProcessToFront()
        {
            Interop.BringWindowToFront(ProcessName);
        }
        public void CenterProcessMainWindow()
        {
            Interop.CenterProcessWindow(ProcessName);
        }

        public virtual void OnProcessClosed()
        {

        }

        public void Execute(int i = 0)
        {
            I = i;
            while (I < Actions.Length)
            {
                if (Disposed)
                {
                    return;
                }
                if (!Interop.IsProcessOpen(ProcessName))
                {
                    OnProcessClosed();
                    return;
                }
                try
                {
                    BringProcessToFront();
                    CenterProcessMainWindow();
                    Console.WriteLine("(" + this.GetType().Name + "): " + Actions[I].ToString());
                    Actions[I].Apply(Bot, this);
                    Thread.Sleep((int)(Actions[I].Duration * 1000));
                    I++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            I = 0;
        }

        public virtual void Dispose()
        {
            Disposed = true;
        }
    }
}
