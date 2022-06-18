using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PursuitTimer.Shared.Services
{
    public class TimerService
    {
        private bool running = false;
        private List<DateTime> times = new();

        public void Start()
        {
            MarkTime();
            running = true;
        }

        public void Stop()
        {
            running = false;
        }

        public long MarkTime()
        {
            times.Add(DateTime.Now);

            int n = times.Count;

            return n > 1 ? ((long)(times[n - 1] - times[n - 2]).TotalMilliseconds) : 0;
        }

        public List<TimeSpan> GetTimes()
        {
            List<TimeSpan> splittimes = new List<TimeSpan>();

            for (int i = 1; i < times.Count; i++)
            {
                splittimes.Add(times[i] - times[i - 1]);
            }

            return splittimes;
        }

        public void Reset()
        {
            Stop();
            times.Clear();
        }

        public bool IsRunning()
        {
            return running;
        }
    }
}
