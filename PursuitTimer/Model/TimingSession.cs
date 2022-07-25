using PursuitTimer.Extensions;

namespace PursuitTimer.Model
{
    public class TimingSession
    {
        public DateTime StartTime { get; } = DateTime.UtcNow;
        public TimeSpan Target { get; set; } = TimeSpan.Zero;
        public List<SplitTime> SplitTimes { get; } = new();
        public TimeSpan TotalTime { get => SplitTimes.Select(x => x.Split).Sum(); }

        public void AddSplit()
        {
            AddSplit(DateTime.UtcNow);
        }

        public void AddSplit(DateTime time)
        {
            var split = SplitTimes.Count > 0 ? time - SplitTimes.Last().Time : time - StartTime;

            SplitTimes.Add(new SplitTime(
                time, 
                split, 
                SplitTimes.Count > 0 ? split - SplitTimes.Last().Split : default, 
                Target > TimeSpan.Zero ? split - Target : default));
        }
    }
}
