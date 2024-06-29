namespace PursuitTimer.Services
{
    public sealed class SettingsService : ISettingsService
    {
        private IPreferences AppSettings = Preferences.Default;

        public T Get<T>(string key)
        {
            T defaultValue = default;

            return Get(key, defaultValue);
        }

        public T Get<T>(string key, T defaultValue)
        {
            var value = AppSettings.Get(key, defaultValue);

            return value;
        }

        public void Save<T>(string key, T value)
        {
            AppSettings.Set(key, value);
        }
    }
}
