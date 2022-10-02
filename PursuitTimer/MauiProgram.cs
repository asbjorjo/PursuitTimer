using CommunityToolkit.Maui;
using PursuitTimer.Pages;
using PursuitTimer.Services;

namespace PursuitTimer;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("RobotoMono-Regular.ttf", "RobotoMonoRegular");
				fonts.AddFont("B612Mono-Regular.ttf", "B612Mono");
			});

		builder.Services.AddSingleton<TimerService>();
        builder.Services.AddTransient<TimerSetupPage>();
        builder.Services.AddTransient<TimerPage>();

		return builder.Build();
	}
}
