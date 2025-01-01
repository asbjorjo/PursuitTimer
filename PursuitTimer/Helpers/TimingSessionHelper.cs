using System.IO;
using System.Text.Json;

namespace PursuitTimer.Helpers;

public static class TimingSessionHelper
{
    private static readonly string _fileName = FilePathHelper.GetFilePath("timingsession.json");

    public static TimingSession GetTimingSession()
    {
        if (File.Exists(_fileName))
        {
            var sessionData = File.ReadAllText(_fileName);
            try
            {
#nullable enable
                TimingSession? _session = JsonSerializer.Deserialize<TimingSession>(sessionData);
#nullable disable
                if (_session != null) return _session;
            }
            catch (JsonException)
            {
                File.Delete(_fileName);
            }
        }

        return new() { Target = TimingTargetHelper.GetTimingTarget() };
    }

    public static void SaveTimingSession(TimingSession value)
    {
        var serializedSession = JsonSerializer.Serialize(value);
        File.WriteAllText(_fileName, serializedSession);
    }

    public static TimingSession ResetTimingSession()
    {
        TimingSession session = new() { Target = TimingTargetHelper.GetTimingTarget() };

        SaveTimingSession(session);

        return session;
    }
}
