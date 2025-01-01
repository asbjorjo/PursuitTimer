namespace PursuitTimer.Pages.Models;

public partial class AboutPageModel : ObservableObject
{
    [ObservableProperty]
    public partial string Version { get; set; } = AppInfo.Current.VersionString;

    //public AboutPageModel()
    //{
    //    Version = AppInfo.Current.VersionString;
    //}

    [RelayCommand]
    async Task Link(string link)
    {
        await Launcher.OpenAsync(link);
    }
}
