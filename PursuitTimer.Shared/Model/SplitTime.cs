namespace PursuitTimer.Shared.Model
{
    public class SplitTime
    {
        public DateTime Time { get; }
        public TimeSpan Split { get; }
        public TimeSpan DeltaPrevious { get; }
        public TimeSpan DeltaTarget { get; }

        public SplitTime(DateTime time, TimeSpan split, TimeSpan deltaPrevious, TimeSpan deltaTarget = default)
        {
            Time = time;
            Split = split;
            DeltaPrevious = deltaPrevious;
            DeltaTarget = deltaTarget;
        }
    }
}
