using PursuitTimer.Model;

namespace PursuitTimer.Services
{
    public class TimerService
    {
        private bool running = false;
        private TimingSession timingSession;
        
        public TimingSession TimingSession
        {
            get => timingSession;
        }

        public void Start()
        {
            Reset();
            timingSession = new TimingSession(DateTime.UtcNow);
            running = true;
        }

        public void Stop()
        {
            running = false;
        }

        public void MarkTime()
        {
            if (!running)
            {
                Start();
            }
            else
            {
                timingSession.AddSplit();
            }
        }

        public void Reset()
        {
            Stop();
            timingSession = null;
        }

        public bool IsRunning()
        {
            return running;
        }
    }
}
