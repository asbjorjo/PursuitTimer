namespace PursuitTimer.Pages.Models;

public partial class SummaryPageModel : ObservableObject
{
    [ObservableProperty]
    public partial IEnumerable<TimingSplit> SplitTimes { get; set; }

    [ObservableProperty]
    public partial TimeSpan SumTimes { get; set; }

    public SummaryPageModel()
    {
    }

    public void Initialize()
    {
        TimingSession timingSession = WeakReferenceMessenger.Default.Send<TimingSessionRequestMessage>();

        SplitTimes = timingSession.Splits;
        SumTimes = SplitTimes.Select(x => x.Split).Sum();
    }
}
