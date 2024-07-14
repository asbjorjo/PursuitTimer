using PursuitTimer.Extensions;
using System.Text.Json.Serialization;

namespace PursuitTimer.Model
{
    public class TimingSession
    {
        private bool _running = false;
        private DateTime _startTime = DateTime.Now;
        private List<SplitTime> _splitTime = new();
        private Targets _targets = new();

        public bool IsRunning { get => _running; init => _running = value; }
        public IReadOnlyList<SplitTime> SplitTimes { get => _splitTime.AsReadOnly(); init => _splitTime = value.ToList(); }
        public DateTime StartTime { get => _startTime; init => _startTime = value; }
        public Targets Targets
        {
            get => _targets;
            set => _targets = value;
        }


        [JsonIgnore]
        public TimeSpan Target
        { 
            get => _targets.Target; 
            set 
            { 
                if (_splitTime.Count == 0) _targets.Target = value;
            } 
        }

        [JsonIgnore]
        public TimeSpan Tolerance
        {
            get => _targets.Negative;
            set => _targets.Positive = value;
        }

        [JsonIgnore]
        public TimeSpan TolerancePositive
        {
            get => _targets.Positive;
            set => _targets.Positive = value;
        }

        [JsonIgnore]
        public TimeSpan TotalTime { get => _splitTime.Select(x => x.Split).Sum(); }

        public TimingSession() { }

        public TimingSession(bool running, List<SplitTime> splitTime, DateTime startTime, Targets targets)
        {
            _running = running;
            _splitTime = splitTime;
            Targets = targets;
            StartTime = startTime;
        }

        public void Reset()
        {
            _splitTime = new();
            _running = false;
        }
        public void AddSplit()
        {
            AddSplit(DateTime.UtcNow);
        }

        public void AddSplit(DateTime time)
        {
            if (_running || _splitTime.Count > 0)
            {
                var split = _splitTime.Count > 0 ? time - _splitTime.Last().Time : time - StartTime;

                _splitTime.Add(new SplitTime(
                    time,
                    split,
                    _splitTime.Count > 0 ? split - _splitTime.Last().Split : default,
                    Target > TimeSpan.Zero ? split - Target : default));
            }
            else
            {
                _startTime = DateTime.UtcNow;
            }

            _running = true;
        }
    }
}
