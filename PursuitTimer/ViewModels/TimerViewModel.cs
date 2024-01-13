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
    private static readonly Color SplitNegative = Colors.Yellow;
    private static readonly Color SplitNeutral = Colors.Lime;

    private readonly TimerService _timerService;
    private readonly ISettingsService _settingsService;

    [ObservableProperty]
    private Color splitcolor = Colors.Transparent;
    [ObservableProperty]
    private SplitTime splittime;
    [ObservableProperty]
    private string splittext = AppResources.Start;
    [ObservableProperty]
    private double fontsize = 32;

    public Color Splittextcolor
    {
        get
        {
            var splittextcolor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;

            if (_timerService.TimingSession.SplitTimes.Count > 0 && !_settingsService.Get<bool>("Monochrome"))
            {
                splittextcolor = Splitcolor.GetLuminosity() < (Math.Sqrt(1.05 * 0.05)) - 0.05 ? Colors.White : Colors.Black;
            }

            return splittextcolor;
        }
    }

    public TimerViewModel(TimerService timerService, ISettingsService settingsService)
    {
        _timerService = timerService;
        _settingsService = settingsService;
    }

    public void UpdateModel()
    {
        _timerService.SetTarget(_settingsService.Get<string>("Targetsplit"));
        _timerService.SetTolerance(_settingsService.Get<string>("Targettolerance"));
        _timerService.SetTolerancePositive(_settingsService.Get<string>("Targettolerancepositive"));

        var timingSession = _timerService.TimingSession;

        if (timingSession.SplitTimes.Count > 0)
        {
            var splitTime = timingSession.SplitTimes[timingSession.SplitTimes.Count - 1];

            Splittext = splitTime.Split.ToString(SplitFormat);

            if (!_settingsService.Get<bool>("Monochrome")) 
            {
                if (timingSession.Target > TimeSpan.Zero)
                {
                    if (splitTime.DeltaTarget > timingSession.TolerancePositive)
                    {
                        Splitcolor = SplitPositive;
                    } else if (timingSession.Tolerance == TimeSpan.Zero || splitTime.DeltaTarget > TimeSpan.Zero - timingSession.Tolerance)
                    {
                        Splitcolor = SplitNeutral;
                    } else
                    {
                        Splitcolor = SplitNegative;
                    }
                }
                else
                {
                    Splitcolor = splitTime.DeltaPrevious > TimeSpan.Zero ? SplitPositive : SplitNeutral;
                }
            }
        }
        else
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
