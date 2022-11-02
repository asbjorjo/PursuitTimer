using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PursuitTimer.Model;
using PursuitTimer.Pages;
using PursuitTimer.Resources.Strings;
using PursuitTimer.Services;

namespace PursuitTimer.ViewModels;

public partial class TimerViewModel : ViewModelBase
{
    private static readonly string SplitFormat = "ss'.'ff";
    private static readonly Color SplitPositive = Colors.Red;
    private static readonly Color SplitNegative = Colors.Lime;

    private readonly TimerService _timerService;

    [ObservableProperty]
    private Color splitcolor = Colors.Transparent;
    [ObservableProperty]
    private SplitTime splittime;
    [ObservableProperty]
    private string splittext = AppResources.Start;
    [ObservableProperty]
    private double fontsize = 32;

    public TimerViewModel(TimerService timerService)
    {
        _timerService = timerService;
    }

    public void UpdateModel()
    {
        var timingSession = _timerService.TimingSession;

        if (timingSession.SplitTimes.Count > 0)
        {
            var splitTime = timingSession.SplitTimes[timingSession.SplitTimes.Count - 1];

            Splittext = splitTime.Split.ToString(SplitFormat);

            if (timingSession.Target > TimeSpan.Zero)
            {
                Splitcolor = splitTime.DeltaTarget > TimeSpan.Zero ? SplitPositive : SplitNegative;
            }
            else
            {
                Splitcolor = splitTime.DeltaPrevious > TimeSpan.Zero ? SplitPositive : SplitNegative;
            }
        } else
        {
            Splittext = AppResources.Start;
            Splitcolor = Colors.Transparent;
        }
    }

    [RelayCommand]
    public void Split()
    {
        DeviceDisplay.KeepScreenOn = true;

        _timerService.MarkTime();

        if (_timerService.TimingSession.SplitTimes.Count > 0)
        {
            UpdateModel();
        }
        else
        {
            Splittext = AppResources.Split;
        }
    }

    [RelayCommand]
    async Task Summary()
    {
        DeviceDisplay.KeepScreenOn = false;

        await Shell.Current.GoToAsync($"//{nameof(SummaryPage)}");
    }
}
