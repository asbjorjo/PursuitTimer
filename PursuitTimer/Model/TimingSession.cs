namespace PursuitTimer.Model
{
    public class TimingSession
    {
        public DateTime StartTime { get; }
        public List<SplitTime> SplitTimes { get; }

        public TimingSession(DateTime startTime)
        {
            StartTime = startTime;
            SplitTimes = new();
        }

        public void AddSplit()
        {
            AddSplit(DateTime.UtcNow);
        }

        public void AddSplit(DateTime time)
        {
            var split = SplitTimes.Count > 0 ? time - SplitTimes.Last().Time : time - StartTime;

            SplitTimes.Add(new SplitTime(time, split, SplitTimes.Count > 0 ? split - SplitTimes.Last().Split : default));
        }
    }
}
