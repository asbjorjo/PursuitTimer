using PursuitTimer.Model;
using System.Globalization;

namespace PursuitTimer.Services
{
    public class TimerService
    {
        private bool running = false;
        private TimingSession timingSession = new();
        
        public TimingSession TimingSession
        {
            get => timingSession;
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

        public void SetTarget(TimeSpan target)
        {
            TimingSession.Target = target;
        }

        public bool SetTarget(string target)
        {
            double targetseconds;

            bool validTarget = double.TryParse(target?.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out targetseconds);

            if (validTarget)
            {
                timingSession.Target = TimeSpan.FromSeconds(targetseconds);
            }

            return validTarget;
        }

        public bool SetTolerance(string tolerance)
        {
            double toleranceseconds;

            bool validTolerance = double.TryParse(tolerance?.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out toleranceseconds);

            if (validTolerance)
            {
                timingSession.Tolerance = TimeSpan.FromSeconds(toleranceseconds);
            }

            return validTolerance;
        }

        public bool SetTolerancePositive(string tolerance)
        {
            double toleranceseconds;

            bool validTolerance = double.TryParse(tolerance?.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out toleranceseconds);

            if (validTolerance)
            {
                timingSession.TolerancePositive = TimeSpan.FromSeconds(toleranceseconds);
            }

            return validTolerance;
        }

        public bool IsRunning()
        {
            return running;
        }
    }
}
