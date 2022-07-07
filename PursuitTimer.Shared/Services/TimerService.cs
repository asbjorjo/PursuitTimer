using PursuitTimer.Shared.Model;

namespace PursuitTimer.Shared.Services
{
    public class TimerService
    {
        private bool running = false;
        private List<DateTime> times = new();
        private List<SplitTime> splits = new();

        public List<SplitTime> Splits
        {
            get => splits;
        }

        public void Start()
        {
            Reset();
            MarkTime();
            running = true;
        }

        public void Stop()
        {
            running = false;
        }

        public long MarkTime()
        {
            var time = DateTime.UtcNow;

            if (times.Count > 0)
            {
                var split = time - times.Last();
                
                splits.Add(new SplitTime(time, split, splits.Count > 0 ? split - splits.Last().Split : default));
            }
            
            times.Add(time);

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
            times = new();
            splits = new();
        }

        public bool IsRunning()
        {
            return running;
        }
    }
}
