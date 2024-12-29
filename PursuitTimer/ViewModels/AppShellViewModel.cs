using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using PursuitTimer.Resources.Strings;

namespace PursuitTimer.ViewModels;

public partial class AppShellViewModel : ObservableRecipient, IRecipient<PropertyChangedMessage<string>>, IRecipient<PropertyChangedMessage<bool>>
{
    [ObservableProperty]
    public partial string TimerLabel { get; set; } = AppResources.Timing;

    [ObservableProperty]
    public partial bool TimerRunning { get; set; } = false;

    [ObservableProperty]
    public partial bool HasIntermediate { get; set; } = false;

    public AppShellViewModel()
    {
        IsActive = true;
    }

    public void Receive(PropertyChangedMessage<string> message)
    {
        if (message.Sender.GetType() == typeof(TimerViewModel))
        {
            switch (message.PropertyName)
            {
                case "Label":
                    TimerLabel = message.NewValue;
                    break;
                default:
                    break;
            }
        }
    }

    public void Receive(PropertyChangedMessage<bool> message)
    {
        if (message.Sender.GetType() == typeof(TimerViewModel))
        {
            switch (message.PropertyName)
            {
                case "Running":
                    TimerRunning = message.NewValue;
                    break;
                case "HasIntermediate":
                    HasIntermediate = message.NewValue;
                    break;
                default:
                    break;
            }
        }
    }
}
