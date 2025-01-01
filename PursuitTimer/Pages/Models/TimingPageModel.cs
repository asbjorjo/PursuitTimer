using PursuitTimer.Helpers;

namespace PursuitTimer.Pages.Models;

public partial class TimingPageModel : ObservableRecipient, IRecipient<TimingTargetChangedMessage>, IRecipient<TabReselectedMessage>, IRecipient<TimingSessionRequestMessage>
{
    private const string TimingSplitFormat = "ss'.'ff";
    private static readonly Color SplitPositive = Colors.Red;
    private static readonly Color SplitNegative = Colors.Yellow;
    private static readonly Color SplitNeutral = Colors.Lime;
    private const double NominalWidth = 860;
    private const double NominalFontSize = 260;
    
    private TimingSession _session = new();
    private bool _monochrome = false;

    [ObservableProperty]
    public partial Color TextColor {  get; set; } = Application.Current?.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;

    [ObservableProperty]
    public partial double TextSize {  get; set; }

    [ObservableProperty]
    public partial Color TimingColor { get; set; } = Colors.Transparent;

    [ObservableProperty]
    public partial string TimingText { get; set; } = AppResources.Start;

    public TimingPageModel()
    {
        IsActive = true;
    }

    public void InitSession()
    {
        IPreferences preferences = Preferences.Default;

        _session = TimingSessionHelper.GetTimingSession();

        UpdateState();
    }

    private Color Textcolor()
    {
        var splittextcolor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;

        if (_session.IsRunning && _session.Splits.Count > 0 && !_monochrome)
        {
            splittextcolor = TimingColor.GetLuminosity() < Math.Sqrt(1.05 * 0.05) - 0.05 ? Colors.White : Colors.Black;
        }

        return splittextcolor;
    }

    private Color Timingcolor()
    {
        if (!_monochrome && _session.IsRunning && _session.HasSplits)
        {
            TimingSplit lastsplit = _session.Splits.Last();

            if (_session.HasTarget)
            {
                if (lastsplit.DeltaTarget > _session.Target.ToleranceOver)
                {
                    return SplitPositive;
                }
                else if (_session.Target.ToleranceUnder == TimeSpan.Zero || lastsplit.DeltaTarget > TimeSpan.Zero - _session.Target.ToleranceUnder)
                {
                    return SplitNeutral;
                }
                else
                {
                    return SplitNegative;
                }
            }
            else
            {
                return lastsplit.DeltaPrevious > TimeSpan.Zero ? SplitPositive : SplitNeutral;
            }
        }
        else
        {
            return Colors.Transparent;
        }
    }

    private void UpdateState()
    {
        if (!_session.IsRunning)
        {
            TimingText = AppResources.Start;
        }
        else
        {
            TimingText = _session.HasSplits ? _session.Splits.Last().Split.ToString(TimingSplitFormat) : AppResources.Split;
        }

        TimingColor = Timingcolor();
        TextColor = Textcolor();

        TimingState state = new TimingState(_session.IsRunning);

        Messenger.Send(new TimingStateChangedMessage(state));
    }

    internal void Size(double width, double height)
    {
        var FontScaleFactor = width / NominalWidth;

        var newfontsize = NominalFontSize * FontScaleFactor;

        if (DeviceInfo.Platform == DevicePlatform.iOS && DeviceDisplay.Current.MainDisplayInfo.Orientation == DisplayOrientation.Landscape)
        {
            newfontsize = newfontsize * 0.9;
        }

        TextSize = newfontsize;
    }

    [RelayCommand]
    public void Split()
    {
        _session.AddSplit();

        UpdateState();

        TimingSessionHelper.SaveTimingSession(_session);
    }

    public void Receive(TimingTargetChangedMessage message)
    {
        _session = TimingSessionHelper.ResetTimingSession();

        UpdateState();
    }

    internal void Appearing()
    {
        Messenger.Send(new TimingVisibilityChangedMessage(true));
    }

    internal void Disappearing()
    {
        Messenger.Send(new TimingVisibilityChangedMessage(false));
    }

    public void Receive(TabReselectedMessage message)
    {
        string target = message.Value;
        if (target == "//PursuitTimer/Timing")
        {
            _session = TimingSessionHelper.ResetTimingSession();
            UpdateState();
        }
    }

    public void Receive(TimingSessionRequestMessage message)
    {
        message.Reply(_session);
    }
}
