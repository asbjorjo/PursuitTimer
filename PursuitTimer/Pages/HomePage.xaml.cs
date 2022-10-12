using PursuitTimer.Services;
using PursuitTimer.ViewModels;

namespace PursuitTimer.Pages;

public partial class HomePage : ContentPage
{
    private readonly TimerService _timerService;

    public HomePage(TimerService timerService)
	{
        InitializeComponent();

        _timerService = timerService;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        SummaryBtn.IsEnabled = _timerService.TimingSession.SplitTimes.Count > 0;
        
        base.OnNavigatedTo(args);
    }
    private async void OnSetupClickedAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TimerSetupPage");
    }

    private async void OnSumaryClickedAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(SummaryPage)}");
    }

    private async void OnStartClickedAsync(object sender, EventArgs e)
    {
        _timerService.Reset();

        await Shell.Current.GoToAsync("//TimerPage");
    }
}
