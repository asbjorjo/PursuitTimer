using CommunityToolkit.Maui;
using PursuitTimer.Pages;
using PursuitTimer.Services;
using PursuitTimer.ViewModels;

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

		builder.ConfigurePages();
        builder.ConfigureServices();
        builder.ConfigureViewModels();
        
		return builder.Build();
	}
}
