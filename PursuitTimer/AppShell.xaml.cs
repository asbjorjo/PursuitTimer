using CommunityToolkit.Mvvm.Messaging;
using PursuitTimer.Messages;
using PursuitTimer.Services;
using PursuitTimer.ViewModels;

namespace PursuitTimer;

public partial class AppShell : Shell
{
    private bool _isPopToRootInProgress;
    private INavigationService _navigationService;
    AppShellViewModel vm => BindingContext as AppShellViewModel;

    public AppShell(INavigationService navigationService, AppShellViewModel navigationViewModel)
    {
        _navigationService = navigationService;

        InitializeComponent();

        BindingContext = navigationViewModel;
    }

    protected override async void OnParentSet()
    {   
        base.OnParentSet();

        if (Parent is not null)
        {
            await _navigationService.InitializeAsync();
        }
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
