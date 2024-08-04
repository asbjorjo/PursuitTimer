using PursuitTimer.Model;
using System.Text.Json;

namespace PursuitTimer.Services
{
    public class TimingSessionService : ITimingSessionService
    {
        private string _fileName = FileSystem.AppDataDirectory + "timingsession.json";

        public TimingSession LoadTimingSession()
        {
            TimingSession session = new TimingSession();
            if (File.Exists(_fileName))
            {
                var sessionData = File.ReadAllText(_fileName);
                try
                {
                    session = JsonSerializer.Deserialize<TimingSession>(sessionData);
                }
                catch (JsonException)
                {
                    File.Delete(_fileName);
                }
            }
            
            return session;
        }

        public void SaveTimingSession(TimingSession timingSession)
        {
            var serializedSession = JsonSerializer.Serialize(timingSession);
            File.WriteAllText(_fileName, serializedSession);
        }
    }
}
