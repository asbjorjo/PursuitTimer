using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PursuitTimer.ViewModels
{
    public partial class AboutViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial string Version { get; set; }

        public AboutViewModel()
        {
            Version = AppInfo.Current.VersionString;
        }

        [RelayCommand]
        async Task Link(string link)
        {
            await Launcher.OpenAsync(link);
        }
    }
}
