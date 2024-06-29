using PursuitTimer.Model;

namespace PursuitTimer.Services
{
    public class TimerService
    {
        private bool running = false;
        private TimingSession timingSession = new();
        private ISettingsService _settingsService;

        public TimingSession TimingSession
        {
            get => timingSession;
        }

        public TimerService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public void Start()
        {
            timingSession.Reset();
            running = true;
        }

        public void Stop()
        {
            running = false;
        }

        public void MarkTime()
        {
            timingSession.Target = TimeSpan.FromSeconds(_settingsService.Get<double>("Targetsplit", 0));
            timingSession.Tolerance = TimeSpan.FromSeconds(_settingsService.Get<double>("Targettolerance", 0));
            timingSession.TolerancePositive = TimeSpan.FromSeconds(_settingsService.Get<double>("Targettolerancepositive", 0));

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
            running = false;
            timingSession.Reset();
        }

        public bool IsRunning()
        {
            return running;
        }
    }
}
