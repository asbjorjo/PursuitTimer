namespace PursuitTimer.Pages;

public partial class SettingsPage : ContentPage
{
    SettingsPageModel model => (SettingsPageModel)BindingContext;

	public SettingsPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        model.InitSettings();

        base.OnAppearing();
    }
}