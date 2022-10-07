using CommunityToolkit.Mvvm.ComponentModel;
using PursuitTimer.Model;
using PursuitTimer.Resources.Strings;

namespace PursuitTimer.ViewModels;

public partial class TimerViewModel : ObservableObject
{
    [ObservableProperty]
    private Color splitcolor = Colors.Transparent;
    [ObservableProperty]
    private SplitTime splittime;
    [ObservableProperty]
    private string splittext = AppResources.Start;
    [ObservableProperty]
    private double fontsize = 32;
    [ObservableProperty]
    private string targetsplit = "00.000";

    public TimerViewModel() { }

    public TimerViewModel(TimingSession session)
    {
        UpdateModel(session);
    }

    public void UpdateModel(TimingSession timingSession)
    {
        if (timingSession.Target > TimeSpan.Zero)
        {
            targetsplit = timingSession.Target.ToString("ss\\.fff");
        }

        if (timingSession.SplitTimes.Count > 0)
        {
            var splitTime = timingSession.SplitTimes[timingSession.SplitTimes.Count - 1];

            Splittext = splitTime.Split.ToString("ss'.'ff");

            if (timingSession.Target > TimeSpan.Zero)
            {
                Splitcolor = splitTime.DeltaTarget > TimeSpan.Zero ? Colors.Coral : Colors.LightGreen;
            }
            else
            {
                Splitcolor = splitTime.DeltaPrevious > TimeSpan.Zero ? Colors.Coral : Colors.LightGreen;
            }
        } else
        {
            Splittext = AppResources.Start;
            Splitcolor = Colors.Transparent;
        }
    }
}
