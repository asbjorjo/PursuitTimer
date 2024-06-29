namespace PursuitTimer.ViewModels
{
    public static class ViewModelExtensions
    {
        public static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<SummaryViewModel>();
            builder.Services.AddTransient<TimerSetupViewModel>();
            builder.Services.AddSingleton<TimerViewModel>();

            return builder;
        }
    }
}
