namespace PursuitTimer;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }

    protected override void OnNavigating(ShellNavigatingEventArgs args)
    {
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
