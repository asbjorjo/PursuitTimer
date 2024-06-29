using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PursuitTimer.Pages;
using PursuitTimer.Services;

namespace PursuitTimer.ViewModels
{
    public partial class TimerSetupViewModel : ObservableObject
    {
        [ObservableProperty]
        private string targetsplit = "00.00";
        [ObservableProperty]
        private string targettolerance = "0.00";
        [ObservableProperty]
        private string targettolerancepositive = "0.00";
        [ObservableProperty]
        private bool monochrome = false;
        private readonly TimerService _timerService;
        private readonly ISettingsService _settingsService;

        public TimerSetupViewModel(TimerService timerService, ISettingsService settingsService)
        {
            _timerService = timerService;
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
        }

        [RelayCommand]
        void Reset()
        {
            Targetsplit = "00.00";
            Targettolerance = "0.00";
            Targettolerancepositive = "0.00";
            Monochrome = false;
        }

        [RelayCommand]
        async Task Cancel()
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        [RelayCommand]
        async Task Save()
        {
            SaveTargets();
            _settingsService.Save(nameof(Monochrome), Monochrome);

            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }
}
