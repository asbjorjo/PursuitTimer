using PursuitTimer.Model;

namespace PursuitTimer.Services
{
    public class TimerService
    {
        private bool running = false;
        private DateTime StartTime;
        private List<SplitTime> splits = new();

        public List<SplitTime> Splits
        {
            get => splits;
        }

        public void Start()
        {
            Reset();
            StartTime = DateTime.UtcNow;
            running = true;
        }

        public void Stop()
        {
            running = false;
        }

        public void MarkTime()
        {
            if (!running)
            {
                Start();
            }
            else
            {
                var time = DateTime.UtcNow;
                var split = Splits.Count > 0 ? time - splits.Last().Time : time - StartTime;

                splits.Add(new SplitTime(time, split, splits.Count > 0 ? split - splits.Last().Split : default));
            }
        }

        public void Reset()
        {
            Stop();
            splits = new();
        }

        public bool IsRunning()
        {
            return running;
        }
    }
}
