using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PursuitTimer.Extensions;
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
            var targets = _settingsService.GetTargets();

            Targetsplit = targets.Target.TotalSeconds;
            Targettolerance = targets.Negative.TotalSeconds;
            Targettolerancepositive = targets.Positive.TotalSeconds;
            Monochrome = _settingsService.Get("Monochrome", Monochrome);
        }

        private void SaveTargets()
        {
            var targets = new Model.Targets
            {
                Target = TimeSpan.FromSeconds(Targetsplit),
                Negative = TimeSpan.FromSeconds(Targettolerance),
                Positive = TimeSpan.FromSeconds(Targettolerancepositive)
            };

            _settingsService.SaveTargets(targets);

            StrongReferenceMessenger.Default.Send(new TargetsChangedMessage(targets));
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
            await _navigationService.NavgigateToAsync("//Timing/Timer");
        }

        [RelayCommand]
        async Task Save()
        {
            SaveTargets();
            _settingsService.Save("Monochrome", Monochrome);

            await _navigationService.NavgigateToAsync("//Timing/Timer");
        }
    }
}
