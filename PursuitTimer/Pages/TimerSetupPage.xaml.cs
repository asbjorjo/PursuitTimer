using PursuitTimer.Services;
using PursuitTimer.ViewModels;
using System.Globalization;

namespace PursuitTimer.Pages;

public partial class TimerSetupPage : ContentPage
{
    TimerSetupViewModel vm => BindingContext as TimerSetupViewModel;

    public TimerSetupPage(TimerSetupViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        vm.Initialize();
    }
}