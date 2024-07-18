using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.Messaging;

namespace PursuitTimer.Services
{
    public static class ServicesExtensions
    {
        public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddTransient<ISettingsService, SettingsService>();
            builder.Services.AddSingleton<ITimingSessionService, TimingSessionService>();
            builder.Services.AddSingleton(x => new MessageSnoopService(WeakReferenceMessenger.Default));

            return builder;
        }
    }
}
