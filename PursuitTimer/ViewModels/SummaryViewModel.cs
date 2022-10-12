using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PursuitTimer.Model;
using PursuitTimer.Pages;
using PursuitTimer.Services;

namespace PursuitTimer.ViewModels;

public partial class SummaryViewModel : ViewModelBase
{
    private readonly TimerService _timerService;

    [ObservableProperty]
    IEnumerable<SplitTime> splitTimes;

    [ObservableProperty]
    TimeSpan sumTimes;

    public SummaryViewModel(TimerService timerService)
    {
        _timerService = timerService;
    }

    public void Initialize()
    {
        SplitTimes = _timerService.TimingSession.SplitTimes;
        SumTimes = _timerService.TimingSession.TotalTime;
    }

    [RelayCommand]
    async Task Resume()
    {
        await Shell.Current.GoToAsync($"//{nameof(TimerPage)}");
    }

    [RelayCommand]
    async Task Restart()
    {
        _timerService.Reset();

        await Shell.Current.GoToAsync($"//{nameof(TimerPage)}");
    }

    [RelayCommand]
    async Task Main()
    {
        await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
    }
}
