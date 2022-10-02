namespace PursuitTimer.Pages;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
        InitializeComponent();
    }

    private async void OnSetupClickedAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TimerSetupPage");
    }

    private async void OnStartClickedAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TimerPage");
    }
}

