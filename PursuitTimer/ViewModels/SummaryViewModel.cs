using PursuitTimer.Extensions;
using PursuitTimer.Model;

namespace PursuitTimer.ViewModels;

public class SummaryViewModel
{
    public TimingSession TimingSession
    {
        get;
    }

    public IEnumerable<SplitTime> SplitTimes
    {
        get => TimingSession.SplitTimes;
    }

    public TimeSpan SumTimes
    {
        get => TimingSession.TotalTime;
    }

    public SummaryViewModel()
    {
        TimingSession = new();
    }

    public SummaryViewModel(TimingSession timingSession)
    {
        TimingSession = timingSession;
    }

}
