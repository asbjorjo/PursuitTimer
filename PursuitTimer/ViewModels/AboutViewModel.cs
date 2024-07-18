using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PursuitTimer.ViewModels
{
    public partial class AboutViewModel : ObservableObject
    {
        [ObservableProperty]
        private string version;

        public AboutViewModel()
        {
            version = AppInfo.Current.VersionString;
        }

        [RelayCommand]
        async Task Link(string link)
        {
            await Launcher.OpenAsync(link);
        }
    }
}
