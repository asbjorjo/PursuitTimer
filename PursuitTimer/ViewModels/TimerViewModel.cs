using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PursuitTimer.Extensions;
using PursuitTimer.Messages;
using PursuitTimer.Model;
using PursuitTimer.Resources.Strings;
using PursuitTimer.Services;

namespace PursuitTimer.ViewModels;

[QueryProperty(nameof(Reset), "Reset")]
public partial class TimerViewModel : ObservableObject, IRecipient<TargetsChangedMessage>, IRecipient<TimingSessionRequestMessage>
{
    private static readonly string SplitFormat = "ss'.'ff";
    private static readonly Color SplitPositive = Colors.Red;
    private static readonly Color SplitNegative = Colors.Yellow;
    private static readonly Color SplitNeutral = Colors.Lime;

    private readonly TimingSession _timingSession = new();
    private readonly INavigationService _navigationService;
    private readonly ISettingsService _settingsService;

    private bool _running = false;

    [ObservableProperty]
    private Color splitcolor = Colors.Transparent;
    [ObservableProperty]
    private SplitTime splittime;
    [ObservableProperty]
    private string splittext = AppResources.Start;
    [ObservableProperty]
    private double fontsize = 32;
    [ObservableProperty]
    public Color splittextcolor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;

    public bool Reset { get; set; }

    public TimerViewModel(INavigationService navigationService, ISettingsService settingsService)
    {
        _navigationService = navigationService;
        _settingsService = settingsService;
        
        StrongReferenceMessenger.Default.RegisterAll(this);

        _timingSession.Targets = _settingsService.GetTargets();
    }

    public Color Textcolor()
    {
        var splittextcolor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;

        if (!Reset && _timingSession.SplitTimes.Count > 0 && !_settingsService.Get<bool>("Monochrome"))
        {
            splittextcolor = Splitcolor.GetLuminosity() < (Math.Sqrt(1.05 * 0.05)) - 0.05 ? Colors.White : Colors.Black;
        }

        return splittextcolor;
    }

    public void UpdateModel()
    {
        if (_running && ! Reset)
        {
            if (_timingSession.SplitTimes.Count > 0)
            {
                var splitTime = _timingSession.SplitTimes[_timingSession.SplitTimes.Count - 1];

                Splittext = splitTime.Split.ToString(SplitFormat);

                if (!_settingsService.Get<bool>("Monochrome"))
                {
                    if (_timingSession.Target > TimeSpan.Zero)
                    {
                        if (splitTime.DeltaTarget > _timingSession.TolerancePositive)
                        {
                            Splitcolor = SplitPositive;
                        }
                        else if (_timingSession.Tolerance == TimeSpan.Zero || splitTime.DeltaTarget > TimeSpan.Zero - _timingSession.Tolerance)
                        {
                            Splitcolor = SplitNeutral;
                        }
                        else
                        {
                            Splitcolor = SplitNegative;
                        }
                    }
                    else
                    {
                        Splitcolor = splitTime.DeltaPrevious > TimeSpan.Zero ? SplitPositive : SplitNeutral;
                    }
                }
            }
            else
            {
                Splittext = AppResources.Split;
            }
        }
        else
        {
            Splittext = AppResources.Start;
            Splitcolor = Colors.Transparent;
        }

        Splittextcolor = Textcolor();
    }

    [RelayCommand]
    public void Split()
    {
        DeviceDisplay.KeepScreenOn = true;

        if (Reset)
        {
            _running = false;
            Reset = false;
        }

        if (_running)
        {
            _timingSession.AddSplit();
        }
        else
        {
            _timingSession.Reset();
            _running = true;
        }

        UpdateModel();
    }

    [RelayCommand]
    async Task Summary()
    {
        DeviceDisplay.KeepScreenOn = false;

        await _navigationService.NavgigateToAsync("//Summary");
    }

    public void Receive(TargetsChangedMessage message)
    {
        _timingSession.Targets = message.Value;
    }

    public void Receive(TimingSessionRequestMessage message)
    {
        message.Reply(_timingSession);
    }
}
