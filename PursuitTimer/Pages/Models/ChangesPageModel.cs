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
        Heading = "A minor, but maybe useful feature is back:";
        Changes = new List<ChangeItem>
        {
            new("Request device keep screen on while timing is running or timing screen is active.")
        };
    }
}
