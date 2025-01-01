namespace PursuitTimer.Helpers;

public static class TimingTargetHelper
{
    private const string SharedName = "me.veloti.pursuittimer";

    private const string KeyTarget = "TimingTarget";
    private const string KeyToleranceOver = "TimingToleranceOver";
    private const string KeyToleranceUnder = "TimingToleranceUnder";

    public static TimingTarget GetTimingTarget()
    {
        IPreferences _preferences = Preferences.Default;
        double target = _preferences.Get<double>(KeyTarget, default, SharedName);
        double over = _preferences.Get<double>(KeyToleranceOver, default, SharedName);
        double under = _preferences.Get<double>(KeyToleranceUnder, default, SharedName);

        return new TimingTarget
        {
            Target = TimeSpan.FromSeconds(target),
            ToleranceOver = TimeSpan.FromSeconds(over),
            ToleranceUnder = TimeSpan.FromSeconds(under)
        };
    }

    public static void SaveTimingTarget(TimingTarget value)
    {
        IPreferences _preferences = Preferences.Default;
        _preferences.Set(KeyTarget, value.Target.TotalSeconds, SharedName);
        _preferences.Set(KeyToleranceOver, value.ToleranceOver.TotalSeconds, SharedName);
        _preferences.Set(KeyToleranceUnder, value.ToleranceUnder.TotalSeconds, SharedName);
    }
}
