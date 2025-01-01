namespace PursuitTimer.Pages;

public partial class SummaryPage : ContentPage
{
    SummaryPageModel vm => (SummaryPageModel)BindingContext;

    public SummaryPage()
	{
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        vm.Initialize();
    }
}