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

        BindingContext = new TimerViewModel();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        viewModel.UpdateModel(_timerService.TimingSession);
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
    private async void OnResetClickedAsync(object sender, EventArgs e)
    {
        viewModel.Targetsplit = "00.000";
    }

    private async void OnCancelClickedAsync(object sender, EventArgs e)
    {
        BindingContext = new TimerViewModel();
        viewModel.UpdateModel(_timerService.TimingSession);
        await Shell.Current.GoToAsync("//MainPage");
    }
    private async void OnSaveClickedAsync(object sender, EventArgs e)
    {
        _timerService.Reset();
        UpdateTarget();
        BindingContext = new TimerViewModel();
        viewModel.UpdateModel(_timerService.TimingSession);
        await Shell.Current.GoToAsync("//MainPage");
    }
}