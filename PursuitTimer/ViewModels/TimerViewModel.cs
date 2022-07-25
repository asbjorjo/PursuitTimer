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
}
