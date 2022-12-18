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
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        vm.UpdateModel();

        LastSplitLabel.TextColor = vm.Splittextcolor;
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

        LastSplitLabel.TextColor = vm.Splittextcolor;
    }
}