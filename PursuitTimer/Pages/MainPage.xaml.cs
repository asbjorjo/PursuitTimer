namespace PursuitTimer.Pages;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
        InitializeComponent();

        StartBtn.Clicked += OnStartClickedAsync;
    }

    private async void OnStartClickedAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TimerPage");
    }
}

