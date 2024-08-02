using CommunityToolkit.Maui.Views;
using PursuitTimer.Popups;
using PursuitTimer.Resources.Strings;
using PursuitTimer.ViewModels;

namespace PursuitTimer.Pages;

public partial class TimerPage : ContentPage
{
    private const double MinRatio = 3.5;

    TimerViewModel vm => BindingContext as TimerViewModel;

    public TimerPage(TimerViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
        vm.IsActive = true;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (vm.ShowChanges)
        {
            this.ShowPopup(new ChangesPopup());
            vm.ShowChanges = false;
        }

        vm.UpdateView();

        DeviceDisplay.KeepScreenOn = true;

        Application.Current.RequestedThemeChanged += ThemeChangedEventHandler;
    }

    void ThemeChangedEventHandler(object senbder, AppThemeChangedEventArgs a)
    {
        vm.UpdateView();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        UpdateFontSize();
    }

    private void UpdateFontSize()
    {
        double ratio = TimerLayout.Width / LastSplitLabel.Height;
        double fontSize = ratio < MinRatio ? LastSplitLabel.Height / MinRatio * ratio : LastSplitLabel.Height;

        vm.Fontsize = fontSize;
    }

    private void OnSplitClicked(object sender, EventArgs e)
    {
        vm.Split();

        UpdateFontSize();
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

        if (location.OriginalString.EndsWith("/Reset")) {
            vm.Reset = true;
        }

        base.OnNavigatedTo(args);
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);

        vm.Label = AppResources.Timing;
    }
}