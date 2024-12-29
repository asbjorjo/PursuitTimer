using PursuitTimer.ViewModels;

namespace PursuitTimer;

public partial class App : Application
{
	AppShellViewModel _appShellViewModel;

	public App(IServiceProvider serviceProvider)
	{
		InitializeComponent();

		_appShellViewModel = serviceProvider.GetService<AppShellViewModel>();
	}

    protected override Window CreateWindow(IActivationState activationState)
    {
		//return new Window(DeviceInfo.Platform == DevicePlatform.iOS ? new AppShelliOS() : new AppShell());
		return new Window(new AppShell(_appShellViewModel));
    }
}
