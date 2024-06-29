namespace PursuitTimer.Services
{
    public static class ServicesExtensions
    {
        public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<ISettingsService, SettingsService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            return builder;
        }
    }
}
