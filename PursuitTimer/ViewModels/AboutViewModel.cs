using CommunityToolkit.Mvvm.ComponentModel;

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
    }
}
