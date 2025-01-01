namespace PursuitTimer.Models;

public class TimingSession
{
    private List<TimingSplit> _splits = new();
    private DateTime _startTime;
    private TimingTarget _target = new();

    public IReadOnlyList<TimingSplit> Splits 
    { 
        get => _splits.AsReadOnly(); 
        init => _splits = [.. value]; 
    }
    public DateTime StartTime 
    { 
        get => _startTime; 
        init => _startTime = value; 
    }
    public TimingTarget Target 
    { 
        get => _target; 
        init => _target = value; 
    }

    public bool IsRunning { get => _startTime != default; }
    public bool HasSplits { get => _splits.Count > 0; }
    public bool HasTarget { get => _target.Target > TimeSpan.Zero; }

    public void AddSplit()
    {
        AddSplit(DateTime.UtcNow);
    }

    public void AddSplit(DateTime time)
    {
        if (IsRunning)
        {
            TimeSpan split = HasSplits ? time - _splits.Last().Time : time - StartTime;

            _splits.Add(new
                (
                    time,
                    split,
                    HasSplits ? split - _splits.Last().Split : default,
                    HasTarget ? split - Target.Target : default
                ));
        }
        else
        {
            _startTime = time;
        }
    }
}
