using PursuitTimer.Shared.Services;

namespace PursuitTimer;

public partial class MainPage : ContentPage
{
    private const double MinRatio = 4.0;

    readonly TimerService _timerService;
	readonly TapGestureRecognizer _splitTap;
	List<TimeSpan> splits = new();

	public MainPage(TimerService timerService)
	{
		_timerService = timerService;
		TapGestureRecognizer tapGestureRecognizer = new();
		tapGestureRecognizer.Tapped += OnSplitClicked;
		_splitTap = tapGestureRecognizer;
        InitializeComponent();
	}

	private void OnStartClicked(object sender, EventArgs e)
    {
		TimerView.GestureRecognizers.Add(_splitTap);
		
		_timerService.Start();

		StartBtn.IsVisible = false;
		StopBtn.IsVisible = true;

		LastSplitLabel.HeightRequest = TimerView.Height - StartBtn.Height;
		LastSplitLabel.IsVisible = true;

        TimerLayout.Layout(Bounds);
		DeviceDisplay.KeepScreenOn = true;
		DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
    }

	private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
	{
		if (LastSplitLabel.IsVisible)
		{
            LastSplitLabel.HeightRequest = TimerView.Width - StopBtn.Height;

            double ratio = TimerView.Height / LastSplitLabel.HeightRequest;
            double fontSize = ratio < MinRatio ? LastSplitLabel.HeightRequest / MinRatio * ratio : LastSplitLabel.HeightRequest;

            LastSplitLabel.FontSize = fontSize;
        }
    }

	private void OnSplitClicked(object sender, EventArgs e)
    {
		_timerService.MarkTime();

		splits = _timerService.GetTimes();

        LastSplitLabel.HeightRequest = TimerView.Height - StopBtn.Height;

        double ratio = TimerView.Width / LastSplitLabel.HeightRequest;
        double fontSize = ratio < MinRatio ? LastSplitLabel.HeightRequest / MinRatio * ratio : LastSplitLabel.HeightRequest;

		LastSplitLabel.FontSize = fontSize;
        LastSplitLabel.Text = splits.Last().ToString("ss'.'fff");

        TimerLayout.Layout(Bounds);
    }

    private void OnStopClicked(object sender, EventArgs e)
	{
		TimerView.GestureRecognizers.Remove(_splitTap);

		_timerService.Reset();

		StopBtn.IsVisible = false;
		StartBtn.IsVisible = true;

		LastSplitLabel.Text = "";
		LastSplitLabel.IsVisible = false;

		DeviceDisplay.KeepScreenOn = false;
    }
}

