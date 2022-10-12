using PursuitTimer.Pages;

namespace PursuitTimer;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

		Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
		Routing.RegisterRoute(nameof(SummaryPage), typeof(SummaryPage));
		Routing.RegisterRoute(nameof(TimerPage), typeof(TimerPage));
		Routing.RegisterRoute(nameof(TimerSetupPage), typeof(TimerSetupPage));
	}
}
