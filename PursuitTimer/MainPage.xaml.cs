using PursuitTimer.Shared.Services;

namespace PursuitTimer;

public partial class MainPage : ContentPage
{
	readonly TimerService _timerService;
	readonly TapGestureRecognizer _splitTap;
	List<TimeSpan> splits = new();
	TimeDisplayDrawable timeDisplay;

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
		TimeDisplay.GestureRecognizers.Add(_splitTap);
		
		_timerService.Start();

		TimeDisplay.HeightRequest = TimerView.Height - StartBtn.Height;

		StartBtn.IsVisible = false;
		TimeDisplay.IsVisible = true;
		StopBtn.IsVisible = true;

		TimerLayout.Layout(Bounds);
    }

	private void OnSplitClicked(object sender, EventArgs e)
    {
		object _timedisplay;
		_timerService.MarkTime();

		splits = _timerService.GetTimes();

		Resources.TryGetValue("timedisplay", out _timedisplay);
		timeDisplay = (TimeDisplayDrawable)_timedisplay;
		timeDisplay.UpdateTime(splits.Last());
        TimeDisplay.Invalidate();
        TimerLayout.Layout(Bounds);
    }

	private void OnStopClicked(object sender, EventArgs e)
	{
		TimerView.GestureRecognizers.Remove(_splitTap);
		TimeDisplay.GestureRecognizers.Remove(_splitTap);

		_timerService.Reset();

		StopBtn.IsVisible = false;
		TimeDisplay.IsVisible = false;
		StartBtn.IsVisible = true;
	}
}

