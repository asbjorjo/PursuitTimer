namespace PursuitTimer.Helpers;

public static class SettingsHelper
{
    private static readonly IPreferences _preferences = Preferences.Default;
    private const string SharedName = "me.veloti.pursuittimer";

    private const string KeyMonochrome = "Monochrome";

    public static bool GetMonochrome()
    {
        return _preferences.Get<bool>(KeyMonochrome, false);
    }

    public static void SaveMonochrome(bool value)
    {
        _preferences.Set(KeyMonochrome, value);
    }
}
