using PursuitTimer.Shared.Extensions;
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
        DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
        InitializeComponent();
    }

    private void OnStartClicked(object sender, EventArgs e)
    {
		if (SummaryView.IsVisible)
		{
			SummaryView.IsVisible = false;
			LastSplitLabel.IsVisible = true;
			LastSplitLabel.Layout(SummaryView.Bounds);
		}

		TimerLayout.GestureRecognizers.Add(_splitTap);
		
		_timerService.Start();

        StartBtn.IsVisible = false;
		StopBtn.IsVisible = true;
        StopBtn.Layout(StartBtn.Bounds);

		DeviceDisplay.KeepScreenOn = true;
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

		splits = _timerService.GetTimes();

        double ratio = TimerLayout.Width / LastSplitLabel.Height;
        double fontSize = ratio < MinRatio ? LastSplitLabel.Height / MinRatio * ratio : LastSplitLabel.Height;

		LastSplitLabel.FontSize = fontSize;
        LastSplitLabel.Text = splits.Last().ToString("ss'.'fff");
    }

    private void OnStopClicked(object sender, EventArgs e)
	{
		TimerLayout.GestureRecognizers.Remove(_splitTap);

		var children = Summary.Children.ToList();
		foreach (var child in children)
		{
			Summary.Remove(child);
		}

        foreach (TimeSpan intermediate in splits)
        {
            Label interLabel = new Label();
            interLabel.Text = intermediate.ToString("ss'.'fff");
			interLabel.FontSize = 32;
            Summary.Add(interLabel);
        }

		Summary.Add(new Label
		{
			Text = splits.Sum().ToString("mm':'ss'.'fff"),
			FontSize = 32
		});

        LastSplitLabel.IsVisible = false;
        SummaryView.IsVisible = true;
        SummaryView.Layout(LastSplitLabel.Bounds);

        _timerService.Reset();

        StopBtn.IsVisible = false;
		StartBtn.IsVisible = true;
        StartBtn.Layout(StopBtn.Bounds);

        LastSplitLabel.Text = "";
		
		DeviceDisplay.KeepScreenOn = false;
    }
}

