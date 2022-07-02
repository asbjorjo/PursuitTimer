using PursuitTimer.Shared.Services;

namespace PursuitTimer;

public partial class MainPage : ContentPage
{
    private const double MinRatio = 3.5;

    readonly TimerService _timerService;
	readonly TapGestureRecognizer _splitTap;
	List<TimeSpan> splits = new();

	public MainPage(TimerService timerService)
	{
		_timerService = timerService;
		TapGestureRecognizer tapGestureRecognizer = new();
		tapGestureRecognizer.Tapped += OnSplitClicked;
		_splitTap = tapGestureRecognizer;
        DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
        InitializeComponent();
    }

    private void OnStartClicked(object sender, EventArgs e)
    {
		TimerLayout.GestureRecognizers.Add(_splitTap);
		
		_timerService.Start();

        StartBtn.IsVisible = false;
		StopBtn.IsVisible = true;
        StopBtn.Layout(StartBtn.Bounds);

		DeviceDisplay.KeepScreenOn = true;
    }

	private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
	{
		double ratio = LastSplitLabel.Height / LastSplitLabel.Height;
        double fontSize = ratio < MinRatio ? LastSplitLabel.Height / MinRatio * ratio : LastSplitLabel.Height;

        LastSplitLabel.FontSize = fontSize;
    }

	private void OnSplitClicked(object sender, EventArgs e)
    {
		_timerService.MarkTime();

		splits = _timerService.GetTimes();

        double ratio = TimerLayout.Width / LastSplitLabel.Height;
        double fontSize = ratio < MinRatio ? LastSplitLabel.Height / MinRatio * ratio : LastSplitLabel.Height;

		LastSplitLabel.FontSize = fontSize;
        LastSplitLabel.Text = splits.Last().ToString("ss'.'fff");
    }

    private void OnStopClicked(object sender, EventArgs e)
	{
		TimerLayout.GestureRecognizers.Remove(_splitTap);

		_timerService.Reset();

        StopBtn.IsVisible = false;
		StartBtn.IsVisible = true;
        StartBtn.Layout(StopBtn.Bounds);

        LastSplitLabel.Text = "";
		
		DeviceDisplay.KeepScreenOn = false;
    }
}

