using PursuitTimer.Pages;
using PursuitTimer.Services;

namespace PursuitTimer;

public partial class App : Application
{
	public App(INavigationService navigationService)
	{
		InitializeComponent();

		MainPage = new AppShell(navigationService);
	}
}
