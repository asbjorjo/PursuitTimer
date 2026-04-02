using CommunityToolkit.Maui;
using MauiIcons.Fluent;
using MauiIcons.Fluent.Filled;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;

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
                fonts.AddFont("B612Mono-Regular.ttf", "B612Mono");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.AddPageModels();

        builder.ConfigureMauiHandlers(handlers =>
        {
#if ANDROID
            handlers.AddHandler(typeof(Shell), typeof(Platforms.Android.AndroidShellRenderer));
#endif
        });

        // TODO: Remove this when this makes it to release: https://github.com/dotnet/maui/pull/34427
#if ANDROID
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("DisableEmojiCompat", (handler, view) =>
        {
            handler.PlatformView.EmojiCompatEnabled = false;
        });

        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("DisableEmojiCompat", (handler, view) =>
        {
            handler.PlatformView.EmojiCompatEnabled = false;
        });
#endif

        return builder.Build();
    }
}

public static class PageModelsExtension
{
    public static MauiAppBuilder AddPageModels(this MauiAppBuilder builder) 
    {
        builder.Services.AddSingleton<TimingPageModel>();

        return builder;
    }
}