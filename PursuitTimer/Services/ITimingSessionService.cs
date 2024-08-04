using PursuitTimer.Model;

namespace PursuitTimer.Services
{
    public interface ITimingSessionService
    {
        void SaveTimingSession(TimingSession timingSession);
        TimingSession LoadTimingSession();
    }
}
