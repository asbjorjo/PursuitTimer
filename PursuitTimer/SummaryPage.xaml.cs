using PursuitTimer.Shared.Extensions;
using PursuitTimer.Shared.Services;

namespace PursuitTimer;

public partial class SummaryPage : ContentPage
{
    readonly TimerService _timerService;
    List<TimeSpan> splits = new();

    public SummaryPage(TimerService timerService)
	{
        InitializeComponent();

        _timerService = timerService;
        
        splits = _timerService.GetTimes();

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
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        splits = _timerService.GetTimes();

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
    }

    private async void OnStartClickedAsync(object sender, EventArgs e)
    {
        _timerService.Start();

        DeviceDisplay.KeepScreenOn = true;

        await Shell.Current.GoToAsync("//TimerPage");
    }
}