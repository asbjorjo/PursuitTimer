using PursuitTimer.Resources.Strings;
using PursuitTimer.Services;
using PursuitTimer.ViewModels;

namespace PursuitTimer.Pages;

public partial class TimerPage : ContentPage
{
    private const double MinRatio = 3.9;

    readonly TimerService _timerService;
    TimerViewModel ViewModel => BindingContext as TimerViewModel;

    public TimerPage(TimerService timerService)
    {
        _timerService = timerService;

        InitializeComponent();

        BindingContext = new TimerViewModel(timerService.TimingSession);
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        UpdateFontSize();
    }

    private void UpdateFontSize()
    {
        double ratio = TimerLayout.Width / LastSplitLabel.Height;
        double fontSize = ratio < MinRatio ? LastSplitLabel.Height / MinRatio * ratio : LastSplitLabel.Height;

        ViewModel.Fontsize = fontSize;
    }

    private void OnSplitClicked(object sender, EventArgs e)
    {
        DeviceDisplay.KeepScreenOn = true;

        _timerService.MarkTime();

        UpdateFontSize();

        if (_timerService.TimingSession.SplitTimes.Count > 0)
        {
            var splitTime = _timerService.TimingSession.SplitTimes[_timerService.TimingSession.SplitTimes.Count - 1];

            ViewModel.Splittext = splitTime.Split.ToString("ss'.'ff");

            if (_timerService.TimingSession.Target > TimeSpan.Zero)
            {
                ViewModel.Splitcolor = splitTime.DeltaTarget > TimeSpan.Zero ? Colors.Coral : Colors.LightGreen;
            } else
            {
                ViewModel.Splitcolor = splitTime.DeltaPrevious > TimeSpan.Zero ? Colors.Coral : Colors.LightGreen;
            }
        }
        else
        {
            ViewModel.Splittext = AppResources.Split;
        }
    }

    private void OnStopClicked(object sender, EventArgs e)
    {
        _timerService.Stop();

        DeviceDisplay.KeepScreenOn = false;

        SummaryViewModel summaryView = new(_timerService.TimingSession);

        var navigationParameters = new Dictionary<string, object>
        {
            {"SummaryView", summaryView}
        };

        Shell.Current.GoToAsync("//SummaryPage", navigationParameters);

        ViewModel.Splittext = AppResources.Start;
        ViewModel.Splitcolor = Colors.Transparent;
    }
}