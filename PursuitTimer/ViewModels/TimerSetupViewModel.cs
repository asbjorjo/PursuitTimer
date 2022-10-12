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
        private string targetsplit = "00.000";
        private readonly TimerService _timerService;

        public TimerSetupViewModel(TimerService timerService)
        {
            _timerService = timerService;
        }

        internal void Initialize()
        {
            Targetsplit = _timerService.TimingSession.Target > TimeSpan.Zero ? _timerService.TimingSession.Target.ToString("ss'.'fff") : "00.000";
        }

        private void UpdateTarget()
        {
            double targetseconds;

            if (double.TryParse(Targetsplit?.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out targetseconds))
            {
                _timerService.SetTarget(TimeSpan.FromSeconds(targetseconds));
            }
            else
            {
                _timerService.SetTarget(TimeSpan.Zero);
            }
        }

        [RelayCommand]
        async Task Reset()
        {
            Targetsplit = "00.000";
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

            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }
}
