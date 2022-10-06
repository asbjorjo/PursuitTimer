using PursuitTimer.Services;
using PursuitTimer.ViewModels;

namespace PursuitTimer.Pages;

public partial class MainPage : ContentPage
{
    public static readonly BindableProperty ShowSummaryProperty =
            BindableProperty.Create("ShowSummary", typeof(bool), typeof(MainPage), false);
    public bool ShowSummary
    {
        get => (bool)GetValue(ShowSummaryProperty);
        set => SetValue(ShowSummaryProperty, value);
    }

    private readonly TimerService _timerService;

    public MainPage(TimerService timerService)
	{
        InitializeComponent();

        _timerService = timerService;
        BindingContext = this;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        ShowSummary = _timerService.TimingSession.SplitTimes.Count > 0;
    }
    private async void OnSetupClickedAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TimerSetupPage");
    }

    private async void OnSumaryClickedAsync(object sender, EventArgs e)
    {
        SummaryViewModel summaryView = new(_timerService.TimingSession);

        var navigationParameters = new Dictionary<string, object>
        {
            {"SummaryView", summaryView}
        };

        await Shell.Current.GoToAsync("//SummaryPage", navigationParameters);
    }

    private async void OnStartClickedAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TimerPage");
    }
}
