using PursuitTimer.Extensions;

namespace PursuitTimer.Model
{
    public class TimingSession
    {
        private List<SplitTime> _splitTime = new();
        private Targets _targets = new();

        public IReadOnlyList<SplitTime> SplitTimes { get => _splitTime.AsReadOnly(); }
        public DateTime StartTime { get; private set; } = DateTime.UtcNow;
        public TimeSpan Target
        { 
            get => _targets.Target; 
            set 
            { 
                if (_splitTime.Count == 0) _targets.Target = value; 
            } 
        }

        public Targets Targets {
            get => _targets;
            set => _targets = value;
        }

        public TimeSpan Tolerance
        {
            get => _targets.Negative;
            set => _targets.Positive = value;
        }

        public TimeSpan TolerancePositive
        {
            get => _targets.Positive;
            set => _targets.Positive = value;
        }

        public TimeSpan TotalTime { get => _splitTime.Select(x => x.Split).Sum(); }

        public void Reset()
        {
            StartTime = DateTime.UtcNow;
            _splitTime = new();
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
