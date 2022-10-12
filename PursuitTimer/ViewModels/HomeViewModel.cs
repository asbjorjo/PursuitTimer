using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PursuitTimer.Pages;
using PursuitTimer.Services;

namespace PursuitTimer.ViewModels
{
    public partial class HomeViewModel : ViewModelBase
    {
        private readonly TimerService _timerService;
        [ObservableProperty]
        private bool hasSummary;

        public HomeViewModel(TimerService timerService)
        {
            _timerService = timerService;
        }

        public void Initialize()
        {
            HasSummary = _timerService.TimingSession.SplitTimes.Count > 0;
        }

        [RelayCommand]
        async Task Setup()
        {
            await Shell.Current.GoToAsync($"//{nameof(TimerSetupPage)}");
        }

        [RelayCommand]
        async Task Summary()
        {
            await Shell.Current.GoToAsync($"//{nameof(SummaryPage)}");
        }

        [RelayCommand]
        async Task Start()
        {
            _timerService.Reset();

            await Shell.Current.GoToAsync($"//{nameof(TimerPage)}");
        }
    }
}
