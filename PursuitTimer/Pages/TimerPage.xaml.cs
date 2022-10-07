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

        BindingContext = new TimerViewModel();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        ViewModel.UpdateModel(_timerService.TimingSession);
        UpdateFontSize();

        base.OnNavigatedTo(args);
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
            ViewModel.UpdateModel(_timerService.TimingSession);
        }
        else
        {
            ViewModel.Splittext = AppResources.Split;
        }
    }

    private async void OnStopClickedAsync(object sender, EventArgs e)
    {
        DeviceDisplay.KeepScreenOn = false;

        SummaryViewModel summaryView = new(_timerService.TimingSession);

        var navigationParameters = new Dictionary<string, object>
        {
            {"SummaryView", summaryView}
        };

        await Shell.Current.GoToAsync("//SummaryPage", navigationParameters);
    }
}