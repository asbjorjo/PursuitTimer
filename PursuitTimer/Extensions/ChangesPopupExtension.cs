using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using PursuitTimer.Pages.Popups;

namespace PursuitTimer.Extensions;

public static class ChangesPopupExtension
{
    private static bool _show = true;
    private const string KeyChanges = "LastVersion";

    public static void ShowChanges(this ContentPage page)
    {
        if (_show)
        {
            bool alreadyshown = Preferences.Default.Get(KeyChanges, string.Empty) == AppInfo.VersionString;

            if (!alreadyshown)
            {
                page.ShowPopup(new ChangesPopup(), new PopupOptions
                {
                    CanBeDismissedByTappingOutsideOfPopup = true,
                });
                Preferences.Default.Set(KeyChanges, AppInfo.VersionString);
                _show = false;
            }
        }
    }
}
