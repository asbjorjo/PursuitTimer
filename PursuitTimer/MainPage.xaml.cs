using PursuitTimer.Shared.Services;

namespace PursuitTimer;

public partial class MainPage : ContentPage
{
    readonly TimerService _timerService;

	public MainPage(TimerService timerService)
	{
		_timerService = timerService;

        InitializeComponent();

        StartBtn.Clicked += OnStartClickedAsync;
    }

    private async void OnStartClickedAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TimerPage");
    }
}

