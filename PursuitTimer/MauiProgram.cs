using CommunityToolkit.Maui;
using MauiIcons.Fluent;
using MauiIcons.Fluent.Filled;
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
			.UseFluentMauiIcons()
			.UseFluentFilledMauiIcons()
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

        builder.ConfigureMauiHandlers(handlers =>
        {
#if ANDROID
		    handlers.AddHandler(typeof(Shell), typeof(Your.Namespace.AndroidShellRenderer));
#endif
        });

            return builder.Build();
	}
}
