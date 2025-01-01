namespace PursuitTimer.Pages.Models;

public partial class ChangesPageModel : ObservableObject
{
    [ObservableProperty]
    public partial string Version { get; set; }

    [ObservableProperty]
    public partial string Heading { get; set; }

    [ObservableProperty]
    public partial List<ChangeItem> Changes { get; set; }

    public ChangesPageModel()
    {
        Version = AppInfo.VersionString;
        Heading = "This version represents a major overhaul of the UI. The basic functionality is the same as before, but there are significant changes to the navigation:";
        Changes = new List<ChangeItem>
        {
            new("Navigation is now by icons at bottom of screen."),
            new("Reset of timer is done by pressing \"Reset\" when timer view is active."),
            new("Summary can only be shown after first split."),
            new("Settings and About will deactive during timing."),
            new("The timing session is saved so that if the app crashes it is loaded on restart.")
        };
    }
}
