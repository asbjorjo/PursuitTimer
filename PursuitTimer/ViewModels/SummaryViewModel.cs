using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PursuitTimer.Messages;
using PursuitTimer.Model;
using PursuitTimer.Services;

namespace PursuitTimer.ViewModels;

public partial class SummaryViewModel : ObservableObject
{
    [ObservableProperty]
    IEnumerable<SplitTime> splitTimes;

    [ObservableProperty]
    TimeSpan sumTimes;
    private INavigationService _navigationService;

    public SummaryViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public void Initialize()
    {
        TimingSession timingSession = StrongReferenceMessenger.Default.Send<TimingSessionRequestMessage>();

        SplitTimes = timingSession.SplitTimes;
        SumTimes = timingSession.TotalTime;
    }

    [RelayCommand]
    async Task Resume()
    {
        await _navigationService.NavgigateToAsync("..");
    }

    [RelayCommand]
    async Task Restart()
    {
        await _navigationService.NavgigateToAsync("//Timing/Timer", new Dictionary<string, object> { { "Reset", true} });
    }

    [RelayCommand]
    async Task Main()
    {
        await _navigationService.NavgigateToAsync("..");
    }
}
