using PursuitTimer.ViewModels;

namespace PursuitTimer.Pages;

public partial class SummaryPage : ContentPage
{
    SummaryViewModel vm => BindingContext as SummaryViewModel;

    public SummaryPage(SummaryViewModel vm)
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