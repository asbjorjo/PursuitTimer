using Android.Content;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;

namespace Your.Namespace;

/// <summary>
/// Shell renderer to fix tab reselection event issue on Android
/// https://github.com/dotnet/maui/issues/15301
/// </summary>
public class AndroidShellRenderer : ShellRenderer
{
    public AndroidShellRenderer(Context context) : base(context)
    {
    }

    protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
    {
        return new CustomShellBottomNavViewAppearanceTracker(this, shellItem);
    }
}

public class CustomShellBottomNavViewAppearanceTracker : ShellBottomNavViewAppearanceTracker
{
    private readonly IShellContext _shellRenderer;
    private readonly ShellItem _shellItem;
    private bool _subscribedItemReselected;

    public CustomShellBottomNavViewAppearanceTracker(IShellContext shellRenderer, ShellItem shellItem)
        : base(shellRenderer, shellItem)
    {
        _shellRenderer = shellRenderer;
        _shellItem = shellItem;
    }

    public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
    {
        base.SetAppearance(bottomView, appearance);
        if (_subscribedItemReselected) return;
        bottomView.ItemReselected += ItemReselected;
        _subscribedItemReselected = true;
    }

    private void ItemReselected(object? sender, EventArgs e)
    {
        (_shellItem as IShellItemController).ProposeSection(_shellItem.CurrentItem);
    }
}