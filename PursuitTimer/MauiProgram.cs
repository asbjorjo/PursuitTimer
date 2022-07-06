using CommunityToolkit.Maui;
using PursuitTimer.Shared.Services;

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
			});

		builder.Services.AddSingleton<TimerService>();
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<TimerPage>();
		builder.Services.AddTransient<SummaryPage>();

		return builder.Build();
	}
}
