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
public partial class TimerViewModel : ObservableRecipient, IRecipient<TargetsChangedMessage>, IRecipient<TimingSessionRequestMessage>, IRecipient<TabReselectedMessage>
{
    private static readonly string SplitFormat = "ss'.'ff";
    private static readonly Color SplitPositive = Colors.Red;
    private static readonly Color SplitNegative = Colors.Yellow;
    private static readonly Color SplitNeutral = Colors.Lime;

    private readonly TimingSession _timingSession = new();
    private readonly INavigationService _navigationService;
    private readonly ISettingsService _settingsService;
    private readonly ITimingSessionService _sessionService;
    private bool _running => _timingSession.IsRunning;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private Color splitcolor = Colors.Transparent;
    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private SplitTime splittime;
    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private string splittext = AppResources.Start;
    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private double fontsize = 32;
    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    public Color splittextcolor = Application.Current?.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;
    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private string label = AppResources.Timing;
    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private bool running = false;
    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private bool hasIntermediate = false;
    
    public bool ShowChanges {
        get => _settingsService.ShowChanges();
        set { if (!value) _settingsService.ChangesShown(); }
    }

    public bool Reset { get; set; }

    public TimerViewModel(INavigationService navigationService, ISettingsService settingsService, ITimingSessionService sessionService) : base()
    {
        _navigationService = navigationService;
        _settingsService = settingsService;
        _sessionService = sessionService;
        
        _timingSession = _sessionService.LoadTimingSession();

        _timingSession.Targets = _settingsService.GetTargets();

        IsActive = true;
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

    public void UpdateView()
    {
        if (Reset)
        {
            _timingSession.Reset();
            Reset = false;
        }

        if (_running)
        {
            Label = AppResources.Reset;

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
                HasIntermediate = true;
            }
            else
            {
                Splittext = AppResources.Split;
            }
        }
        else
        {
            HasIntermediate = false;
            Label = AppResources.Timing;

            Splittext = AppResources.Start;
            Splitcolor = Application.Current.RequestedTheme == AppTheme.Light ? Colors.White : Colors.Black;

            DeviceDisplay.KeepScreenOn = false;
        }

        Running = _running;
        Splittextcolor = Textcolor();
        _sessionService.SaveTimingSession(_timingSession);
    }

    [RelayCommand]
    public void Split()
    {
        _timingSession.AddSplit();

        UpdateView();
    }

    [RelayCommand]
    async Task Summary()
    {
        DeviceDisplay.KeepScreenOn = false;

        await _navigationService.NavgigateToAsync("Summary");
    }

    public void Receive(TargetsChangedMessage message)
    {
        _timingSession.Targets = message.Value;
    }

    public void Receive(TimingSessionRequestMessage message)
    {
        message.Reply(_timingSession);
    }

    public void Receive(TabReselectedMessage message)
    {
        string target = message.Value;
        if (target.EndsWith("Timer")) {
            Reset = true;
            UpdateView();
        }
    }
}
