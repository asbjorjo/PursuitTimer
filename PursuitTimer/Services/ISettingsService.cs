namespace PursuitTimer.Services
{
    public interface ISettingsService
    {
        T Get<T>(string key);
        T Get<T>(string key, T defaultValue);
        void Save<T>(string key, T value);
    }
}
