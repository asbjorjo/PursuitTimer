using CommunityToolkit.Mvvm.ComponentModel;

namespace PursuitTimer.ViewModels
{
    public partial class ChangesViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial string Version { get; set; }

        [ObservableProperty]
        public partial string Heading { get; set; }

        [ObservableProperty]
        public partial List<string> Changes { get; set; }

        public ChangesViewModel() {
            Version = AppInfo.VersionString;
            Heading = "This version represents a major overhaul of the UI. The basic functionality is the same as before, but there are significant changes to the navigation:";
            Changes = new List<string>
            {
                "Navigation is now by icons at bottom of screen.",
                "Reset of timer is done by pressing \"Reset\" when timer view is active.",
                "Summary can only be shown after first split.",
                "Settings and About will deactive during timing.",
                "The timing session is saved so that if the app crashes it is loaded on restart."
            };
        }
    }
}
