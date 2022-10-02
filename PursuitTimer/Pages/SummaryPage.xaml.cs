using PursuitTimer.ViewModels;

namespace PursuitTimer.Pages;

[QueryProperty(nameof(SummaryView), "SummaryView")]
public partial class SummaryPage : ContentPage
{
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

    public SummaryPage()
	{
        InitializeComponent();
        BindingContext = this;
    }

    private async void OnStartClickedAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TimerPage");
    }

    private async void OnMainClickedAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}