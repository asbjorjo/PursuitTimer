using PursuitTimer.Services;
using PursuitTimer.ViewModels;

namespace PursuitTimer.Pages;

[QueryProperty(nameof(SummaryView), "SummaryView")]
public partial class SummaryPage : ContentPage
{
    private TimerService _timerService;
    public static readonly BindableProperty SummaryViewProperty =
        BindableProperty.Create("SummaryView", typeof(SummaryViewModel), typeof(SummaryPage), new SummaryViewModel());

    public SummaryViewModel SummaryView
    {
        get =>  (SummaryViewModel)GetValue(SummaryViewProperty);
        set 
        {
            SetValue(SummaryViewProperty, value);
        }
    }

    public SummaryPage(TimerService timerService)
	{
        _timerService = timerService;
        InitializeComponent();
        BindingContext = this;
    }

    private async void OnResumeClickedAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TimerPage");
    }

    private async void OnRestartClickedAsync(object sender, EventArgs e)
    {
        _timerService.Reset();

        await Shell.Current.GoToAsync("//TimerPage");
    }

    private async void OnMainClickedAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}