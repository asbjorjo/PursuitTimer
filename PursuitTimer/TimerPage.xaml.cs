using PursuitTimer.Shared.Services;

namespace PursuitTimer;

public partial class TimerPage : ContentPage
{
    private const double MinRatio = 4.0;

    readonly TimerService _timerService;
    readonly TapGestureRecognizer _splitTap;
    
    public TimerPage(TimerService timerService)
    {
        _timerService = timerService;
        TapGestureRecognizer tapGestureRecognizer = new();
        tapGestureRecognizer.Tapped += OnSplitClicked;
        _splitTap = tapGestureRecognizer;

        InitializeComponent();

        TimerLayout.GestureRecognizers.Add(_splitTap);

        DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (!_timerService.IsRunning())
        {
            _timerService.Start();
        }

        DeviceDisplay.KeepScreenOn = true;

        base.OnNavigatedTo(args);
    }

    private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        //double ratio = TimerLayout.Width / LastSplitLabel.Height;
        //double fontSize = ratio < MinRatio ? LastSplitLabel.Height / MinRatio * ratio : LastSplitLabel.Height;
        double fontSize = LastSplitLabel.Height / MinRatio;

        LastSplitLabel.FontSize = fontSize;
    }

    private void OnSplitClicked(object sender, EventArgs e)
    {
        _timerService.MarkTime();

        double ratio = TimerLayout.Width / LastSplitLabel.Height;
        double fontSize = ratio < MinRatio ? LastSplitLabel.Height / MinRatio * ratio : LastSplitLabel.Height;

        LastSplitLabel.FontSize = fontSize;
        LastSplitLabel.Text = _timerService.Splits.Last().Split.ToString("ss'.'fff");
    }

    private async void OnStopClicked(object sender, EventArgs e)
    {
        _timerService.Stop();

        LastSplitLabel.Text = "";

        DeviceDisplay.KeepScreenOn = false;

        var navigationParameters = new Dictionary<string, object>
        {
            {"Splits", _timerService.Splits}
        };

        await Shell.Current.GoToAsync("//SummaryPage", navigationParameters);
    }
}