using PursuitTimer.Shared.Services;

namespace PursuitTimer;

public partial class MainPage : ContentPage
{
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
		LastSplitLabel.IsVisible = true;
		StopBtn.IsVisible = true;

		TimerLayout.Layout(Bounds);
    }

	private void OnSplitClicked(object sender, EventArgs e)
    {
		_timerService.MarkTime();

		splits = _timerService.GetTimes();
		LastSplitLabel.Text = splits.Last().ToString("ss'.'fff");

        TimerLayout.Layout(Bounds);
    }

	private void OnStopClicked(object sender, EventArgs e)
	{
		TimerView.GestureRecognizers.Remove(_splitTap);

		_timerService.Reset();

		LastSplitLabel.IsVisible = false;
		LastSplitLabel.Text = "";
		StopBtn.IsVisible = false;
		StartBtn.IsVisible = true;
	}
}

