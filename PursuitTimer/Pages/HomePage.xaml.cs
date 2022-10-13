using PursuitTimer.Services;
using PursuitTimer.ViewModels;

namespace PursuitTimer.Pages;

public partial class HomePage : ContentPage
{
    private HomeViewModel vm => BindingContext as HomeViewModel;

    public HomePage(HomeViewModel vm)
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
