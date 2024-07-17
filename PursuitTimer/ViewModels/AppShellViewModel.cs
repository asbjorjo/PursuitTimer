namespace PursuitTimer.ViewModels
{
    public partial class AppShellViewModel
    {
        public TimerViewModel TimerView { get; }

        public AppShellViewModel(TimerViewModel timerView)
        {
            TimerView = timerView;
        }
    }
}
