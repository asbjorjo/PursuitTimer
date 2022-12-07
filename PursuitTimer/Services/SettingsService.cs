namespace PursuitTimer.Services
{
    public class SettingsService : ISettingsService
    {
        public T Get<T>(string key)
        {
            T defaultValue = default;

            return Get<T>(key, defaultValue);
        }

        public T Get<T>(string key, T defaultValue)
        {
            var value = Preferences.Default.Get<T>(key, defaultValue);

            return value;
        }

        public void Save<T>(string key, T value)
        {
            Preferences.Default.Set<T>(key, value);
        }
    }
}
