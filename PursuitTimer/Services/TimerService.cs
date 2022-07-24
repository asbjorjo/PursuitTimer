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
            timingSession = new TimingSession();
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

        public bool IsRunning()
        {
            return running;
        }
    }
}
