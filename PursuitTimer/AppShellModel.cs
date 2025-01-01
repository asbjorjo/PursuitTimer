namespace PursuitTimer;

public partial class AppShellModel : ObservableRecipient, IRecipient<TimingStateChangedMessage>, IRecipient<TimingVisibilityChangedMessage>
{
    [ObservableProperty]
    public partial string TimingLabel { get; set; } = AppResources.Timing;

    [ObservableProperty]
    public partial bool TimerRunning { get; set; } = false;

    [ObservableProperty]
    public partial string AboutLabel { get; set; } = AppResources.About;

    public AppShellModel()
    {
        IsActive = true;
    }

    public void Receive(TimingStateChangedMessage message)
    {
        TimingState state = message.Value;
        TimerRunning = state.IsRunning;
        TimingLabel = TimerRunning ? AppResources.Reset : AppResources.Timing;
    }

    public void Receive(TimingVisibilityChangedMessage message)
    {
        bool timingVisible = message.Value;
        TimingLabel = (timingVisible && TimerRunning) ? AppResources.Reset : AppResources.Timing;
    }
}
