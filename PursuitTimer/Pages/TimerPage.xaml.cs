using PursuitTimer.Resources.Strings;
using PursuitTimer.ViewModels;

namespace PursuitTimer.Pages;

public partial class TimerPage : ContentPage
{
    TimerViewModel vm => BindingContext as TimerViewModel;

    public TimerPage(TimerViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }


    void ThemeChangedEventHandler(object senbder, AppThemeChangedEventArgs a)
    {
        vm.UpdateView();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        vm.UpdateFontSize(width, height);
    }

    protected override void OnDisappearing()
    {
        vm.UpdateView();

        Application.Current.RequestedThemeChanged -= ThemeChangedEventHandler;

        base.OnDisappearing();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        var location = Shell.Current.CurrentState.Location;

        if (location.OriginalString.EndsWith("/Reset"))
        {
            vm.Reset = true;
        }

        vm.UpdateView();

        base.OnNavigatedTo(args);
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);

        vm.Label = AppResources.Timing;
    }
}