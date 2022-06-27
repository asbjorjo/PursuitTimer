using PursuitTimer.Shared.Services;

namespace PursuitTimer;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("RobotoMono-VariableFont_wght.ttf", "RobotoMono");
			});

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<TimerService>();

		return builder.Build();
	}
}
