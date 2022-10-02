using PursuitTimer.Resources.Strings;
using PursuitTimer.Services;
using PursuitTimer.ViewModels;
using System.Globalization;

namespace PursuitTimer.Pages;

public partial class TimerSetupPage : ContentPage
{
    readonly TimerService _timerService;
    TimerViewModel viewModel => BindingContext as TimerViewModel;

    public TimerSetupPage(TimerService timerService)
    {
        _timerService = timerService;

        InitializeComponent();

        BindingContext = new TimerViewModel(timerService.TimingSession);
    }

    private void UpdateTarget()
    {
        double targetseconds;

        if (double.TryParse(viewModel.Targetsplit?.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out targetseconds))
        {
            _timerService.SetTarget(TimeSpan.FromSeconds(targetseconds));
        }
        else
        {
            _timerService.SetTarget(TimeSpan.Zero);
        }
    }

    private async void OnTimeClickedAsync(object sender, EventArgs e)
    {
        _timerService.Reset();
        UpdateTarget();
        await Shell.Current.GoToAsync("//TimerPage");
    }
}