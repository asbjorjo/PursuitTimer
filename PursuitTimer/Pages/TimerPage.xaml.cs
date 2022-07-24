using PursuitTimer.Resources.Strings;
using PursuitTimer.Services;
using PursuitTimer.ViewModels;

namespace PursuitTimer.Pages;

public partial class TimerPage : ContentPage
{
    private const double MinRatio = 4.0;

    readonly TimerService _timerService;
    readonly TapGestureRecognizer _splitTap;

    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create("FontSize", typeof(double), typeof(TimerPage), 32.0);
    public static readonly BindableProperty SplitTextProperty =
        BindableProperty.Create("SplitText", typeof(string), typeof(TimerPage), AppResources.Start);

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set
        {
            SetValue(FontSizeProperty, value);
        }
    }
    public string SplitText
    {
        get => (string)GetValue(SplitTextProperty);
        set
        {
            SetValue(SplitTextProperty, value);
        }
    }


    public TimerPage(TimerService timerService)
    {
        _timerService = timerService;
        TapGestureRecognizer tapGestureRecognizer = new();
        tapGestureRecognizer.Tapped += OnSplitClicked;
        _splitTap = tapGestureRecognizer;

        InitializeComponent();

        TimerLayout.GestureRecognizers.Add(_splitTap);

        DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;

        BindingContext = this;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        DeviceDisplay.KeepScreenOn = true;

        base.OnNavigatedTo(args);
    }

    private void UpdateFontSize()
    {
        double ratio = TimerLayout.Width / LastSplitLabel.Height;
        double fontSize = ratio < MinRatio ? LastSplitLabel.Height / MinRatio * ratio : LastSplitLabel.Height;

        FontSize = fontSize;
    }

    private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        //double ratio = TimerLayout.Width / LastSplitLabel.Height;
        //double fontSize = ratio < MinRatio ? LastSplitLabel.Height / MinRatio * ratio : LastSplitLabel.Height;

        UpdateFontSize();
    }

    private void OnSplitClicked(object sender, EventArgs e)
    {
        _timerService.MarkTime();

        UpdateFontSize();

        if (_timerService.TimingSession.SplitTimes.Count > 0)
        {
            SplitText = _timerService.TimingSession.SplitTimes.Last().Split.ToString("ss'.'fff");
        } else
        {
            SplitText = AppResources.Split;
        }
    }

    private void OnStopClicked(object sender, EventArgs e)
    {
        _timerService.Stop();

        DeviceDisplay.KeepScreenOn = false;

        SummaryViewModel summaryView = new SummaryViewModel(_timerService.TimingSession.SplitTimes);

        var navigationParameters = new Dictionary<string, object>
        {
            {"SummaryView", summaryView}
        };

        Shell.Current.GoToAsync("//SummaryPage", navigationParameters);

        SplitText = AppResources.Start;
    }
}