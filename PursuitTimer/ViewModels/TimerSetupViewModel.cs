using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PursuitTimer.Pages;
using PursuitTimer.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PursuitTimer.ViewModels
{
    public partial class TimerSetupViewModel : ViewModelBase
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
            Targetsplit = _timerService.TimingSession.Target > TimeSpan.Zero ? _timerService.TimingSession.Target.ToString("ss'.'ff") : _settingsService.Get(nameof(Targetsplit), Targetsplit);
            Targettolerance = _timerService.TimingSession.Tolerance > TimeSpan.Zero ? _timerService.TimingSession.Tolerance.ToString("s'.'ff") : _settingsService.Get(nameof(Targettolerance), Targettolerance);
            Targettolerancepositive = _timerService.TimingSession.TolerancePositive > TimeSpan.Zero ? _timerService.TimingSession.TolerancePositive.ToString("s'.'ff") : _settingsService.Get(nameof(Targettolerancepositive), Targettolerancepositive);
            Monochrome = _settingsService.Get(nameof(Monochrome), Monochrome);
        }

        private void UpdateTarget()
        {
            if (_timerService.SetTarget(Targetsplit))
            {
                _settingsService.Save(nameof(Targetsplit), Targetsplit);
            }

            if (_timerService.SetTolerance(Targettolerance))
            {
                _settingsService.Save(nameof(Targettolerance), Targettolerance);
            }

            if (_timerService.SetTolerancePositive(Targettolerancepositive))
            {
                _settingsService.Save(nameof(Targettolerancepositive), Targettolerancepositive);
            }
        }

        [RelayCommand]
        async Task Reset()
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
            _timerService.Reset();
            UpdateTarget();
            _settingsService.Save(nameof(Monochrome), Monochrome);

            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }
}
