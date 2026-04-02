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
        Heading = "Fix Android settings crash:";
        Changes = new List<ChangeItem>
        {
            new("Temporary fix for crash on Android when editing target and tolerances.")
        };
    }
}
