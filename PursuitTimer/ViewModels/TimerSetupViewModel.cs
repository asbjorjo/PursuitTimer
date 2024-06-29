using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PursuitTimer.Messages;
using PursuitTimer.Pages;
using PursuitTimer.Services;

namespace PursuitTimer.ViewModels
{
    public partial class TimerSetupViewModel : ObservableObject
    {
        [ObservableProperty]
        private double targetsplit = default;
        [ObservableProperty]
        private double targettolerance = default;
        [ObservableProperty]
        private double targettolerancepositive = default;
        [ObservableProperty]
        private bool monochrome = false;
        private readonly INavigationService _navigationService;
        private readonly ISettingsService _settingsService;

        public TimerSetupViewModel(INavigationService navigationService, ISettingsService settingsService)
        {
            _navigationService = navigationService;
            _settingsService = settingsService;
        }

        internal void Initialize()
        {
            Targetsplit = _settingsService.Get(nameof(Targetsplit), Targetsplit);
            Targettolerance = _settingsService.Get(nameof(Targettolerance), Targettolerance);
            Targettolerancepositive = _settingsService.Get(nameof(Targettolerancepositive), Targettolerancepositive);
            Monochrome = _settingsService.Get(nameof(Monochrome), Monochrome);
        }

        private void SaveTargets()
        {
            _settingsService.Save(nameof(Targetsplit), Targetsplit);
            _settingsService.Save(nameof(Targettolerance), Targettolerance);
            _settingsService.Save(nameof(Targettolerancepositive), Targettolerancepositive);

            StrongReferenceMessenger.Default.Send(new TargetsChangedMessage(new Model.Targets
            {
                Target = TimeSpan.FromSeconds(Targetsplit),
                Negative = TimeSpan.FromSeconds(Targettolerance),
                Positive = TimeSpan.FromSeconds(Targettolerancepositive)
            }));
        }

        [RelayCommand]
        void Reset()
        {
            Targetsplit = default;
            Targettolerance = default;
            Targettolerancepositive = default;
            Monochrome = false;
        }

        [RelayCommand]
        async Task Cancel()
        {
            await _navigationService.NavgigateToAsync("//Home");
        }

        [RelayCommand]
        async Task Save()
        {
            SaveTargets();
            _settingsService.Save(nameof(Monochrome), Monochrome);

            await _navigationService.NavgigateToAsync("//Home");
        }
    }
}
