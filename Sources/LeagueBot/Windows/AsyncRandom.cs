using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueBot.Windows
{
    public sealed class AsyncRandom : Random
    {
        private static int m_incrementer;
        public AsyncRandom()
            : base(Environment.TickCount + Thread.CurrentThread.ManagedThreadId + AsyncRandom.m_incrementer)
        {
            Interlocked.Increment(ref AsyncRandom.m_incrementer);
        }

        public AsyncRandom(int seed)
            : base(seed)
        {
        }

        public double NextDouble(double min, double max)
        {
            return this.NextDouble() * (max - min) + min;
        }
    }
}
