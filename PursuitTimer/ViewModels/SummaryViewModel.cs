using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PursuitTimer.Messages;
using PursuitTimer.Model;

namespace PursuitTimer.ViewModels;

public partial class SummaryViewModel : ObservableObject
{
    [ObservableProperty]
    public partial IEnumerable<SplitTime> SplitTimes { get; set; }

    [ObservableProperty]
    public partial TimeSpan SumTimes { get; set; }

    public SummaryViewModel()
    {
    }

    public void Initialize()
    {
        TimingSession timingSession = WeakReferenceMessenger.Default.Send<TimingSessionRequestMessage>();

        SplitTimes = timingSession.SplitTimes;
        SumTimes = timingSession.TotalTime;
    }

    [RelayCommand]
    async Task Restart()
    {
        await Shell.Current.GoToAsync("//Timing/Timer", new Dictionary<string, object> { { "Reset", true } });
        //await _navigationService.NavgigateToAsync("//Timing/Timer", new Dictionary<string, object> { { "Reset", true} });
    }
}
