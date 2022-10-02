using PursuitTimer.Extensions;

namespace PursuitTimer.Model
{
    public class TimingSession
    {
        private List<SplitTime> _splitTime = new();
        private TimeSpan _target = TimeSpan.Zero;

        public IReadOnlyList<SplitTime> SplitTimes { get => _splitTime.AsReadOnly(); }
        public DateTime StartTime { get; private set; } = DateTime.UtcNow;
        public TimeSpan Target
        { 
            get => _target; 
            set 
            { 
                if (_splitTime.Count == 0) _target = value; 
            } 
        }
        public TimeSpan TotalTime { get => _splitTime.Select(x => x.Split).Sum(); }

        public void Reset()
        {
            _splitTime.Clear();
            StartTime = DateTime.UtcNow;
        }
        public void AddSplit()
        {
            AddSplit(DateTime.UtcNow);
        }

        public void AddSplit(DateTime time)
        {
            var split = _splitTime.Count > 0 ? time - _splitTime.Last().Time : time - StartTime;

            _splitTime.Add(new SplitTime(
                time, 
                split,
                _splitTime.Count > 0 ? split - _splitTime.Last().Split : default, 
                Target > TimeSpan.Zero ? split - Target : default));
        }
    }
}
