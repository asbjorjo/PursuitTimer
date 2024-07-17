using PursuitTimer.Services;
using PursuitTimer.ViewModels;

namespace PursuitTimer;

public partial class App : Application
{
	public App(INavigationService navigationService, AppShellViewModel navigationViewModel)
	{
		InitializeComponent();

		MainPage = new AppShell(navigationService, navigationViewModel);
	}
}
