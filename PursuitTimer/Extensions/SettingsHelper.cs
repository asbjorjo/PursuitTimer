using PursuitTimer.Model;
using PursuitTimer.Services;

namespace PursuitTimer.Extensions
{
    public static class SettingsHelper
    {
        public static Targets GetTargets(this ISettingsService settingsService)
        {
            return new Targets
            {
                Target = TimeSpan.FromSeconds(settingsService.Get<double>("Targetsplit")),
                Negative = TimeSpan.FromSeconds(settingsService.Get<double>("Targettolerance")),
                Positive = TimeSpan.FromSeconds(settingsService.Get<double>("Targettolerancepositive"))
            };
        }

        public static void SaveTargets(this ISettingsService settingsService, Targets targets)
        {
            settingsService.Save("Targetsplit", targets.Target.TotalSeconds);
            settingsService.Save("Targettolerance", targets.Negative.TotalSeconds);
            settingsService.Save("Targettolerancepositive", targets.Positive.TotalSeconds);
        }

        public static bool ShowChanges(this ISettingsService settingsService) {
            return settingsService.Get("Lastversion", string.Empty) != AppInfo.VersionString;
        }

        public static void ChangesShown(this ISettingsService settingsService)
        {
            settingsService.Save("Lastversion", AppInfo.VersionString); 
        }
    }
}
