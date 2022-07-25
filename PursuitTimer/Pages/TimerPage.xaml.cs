using PursuitTimer.Resources.Strings;
using PursuitTimer.Services;
using PursuitTimer.ViewModels;

namespace PursuitTimer.Pages;

public partial class TimerPage : ContentPage
{
    private const double MinRatio = 3.9;

    readonly TimerService _timerService;

    TimerViewModel viewModel => BindingContext as TimerViewModel;

    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create("FontSize", typeof(double), typeof(TimerPage), 32.0);
    public static readonly BindableProperty SplitColorProperty =
        BindableProperty.Create("SplitColor", typeof(Color), typeof(TimerPage), Colors.White);
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
    public Color SplitColor
    {
        get => (Color)GetValue(SplitColorProperty);
        set
        {
            SetValue(SplitColorProperty, value);
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

        InitializeComponent();

        DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;

        BindingContext = new TimerViewModel();
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

        FontSize = fontSize;
        viewModel.Fontsize = FontSize;
    }

    private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        UpdateFontSize();
    }

    private void OnSplitClicked(object sender, EventArgs e)
    {
        DeviceDisplay.KeepScreenOn = true;

        _timerService.MarkTime();

        UpdateFontSize();

        if (_timerService.TimingSession.SplitTimes.Count > 0)
        {
            var splitTime = _timerService.TimingSession.SplitTimes.Last();

            SplitText = splitTime.Split.ToString("ss'.'fff");
            viewModel.Splittext = SplitText;

            if (splitTime.DeltaPrevious > TimeSpan.Zero)
            {
                SplitColor = Colors.Coral;
            } else {
                SplitColor = Colors.LightGreen;
            }
            viewModel.Splitcolor = SplitColor;
        }
        else
        {
            SplitText = AppResources.Split;
            viewModel.Splittext = SplitText;
        }
    }

    private void OnStopClicked(object sender, EventArgs e)
    {
        _timerService.Stop();

        DeviceDisplay.KeepScreenOn = false;

        SummaryViewModel summaryView = new SummaryViewModel(_timerService.TimingSession);

        var navigationParameters = new Dictionary<string, object>
        {
            {"SummaryView", summaryView}
        };

        Shell.Current.GoToAsync("//SummaryPage", navigationParameters);

        SplitText = AppResources.Start;
        viewModel.Splittext= SplitText;
        SplitColor = Color.FromArgb(Colors.White.ToArgbHex());
        viewModel.Splitcolor = SplitColor;
    }
}