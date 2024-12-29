using CommunityToolkit.Mvvm.Messaging;
using PursuitTimer.Messages;
using PursuitTimer.ViewModels;

namespace PursuitTimer;

public partial class AppShell : Shell
{
    public AppShell(AppShellViewModel model)
    {
        InitializeComponent();

        BindingContext = model;
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
