using CommunityToolkit.Mvvm.Messaging;
using PursuitTimer.Messages;

namespace PursuitTimer;

public partial class AppShelliOS : Shell
{
    public AppShelliOS()
    {
        InitializeComponent();
    }

    protected override void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);
        Console.WriteLine($"{args.Source}: {args.Current} -> {args.Target}");
        if (args.Source == ShellNavigationSource.ShellSectionChanged
            && args is { Current: not null, Target: not null }
            && args.Current.Location == args.Target.Location)
        {
            Console.WriteLine($"{args.Current.Location} -> {args.Target.Location}");
            WeakReferenceMessenger.Default.Send(new TabReselectedMessage(args.Target.Location.OriginalString));
        }
    }
}
