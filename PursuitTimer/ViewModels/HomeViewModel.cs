using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PursuitTimer.Messages;
using PursuitTimer.Model;
using PursuitTimer.Pages;
using PursuitTimer.Services;

namespace PursuitTimer.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SummaryCommand))]
        private bool hasSummary;
        private INavigationService _navigationService;

        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void Initialize()
        {
            var timingSessionResponse = StrongReferenceMessenger.Default.Send<TimingSessionRequestMessage>();

            HasSummary = timingSessionResponse.HasReceivedResponse && timingSessionResponse.Response.SplitTimes.Count > 0;
        }

        [RelayCommand]
        async Task Setup()
        {
            await _navigationService.NavgigateToAsync("//TimerSetup");
        }

        [RelayCommand(CanExecute = nameof(CanShowSummary))]
        async Task Summary()
        {
            await _navigationService.NavgigateToAsync("//Summary");
        }

        [RelayCommand]
        async Task Timing()
        {
            await _navigationService.NavgigateToAsync("//Timing");
        }

        private bool CanShowSummary() => HasSummary;
    }
}
