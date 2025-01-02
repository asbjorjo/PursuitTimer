using CommunityToolkit.Mvvm.Messaging.Messages;

namespace PursuitTimer.Messages;

public class SettingsChangedMessage : ValueChangedMessage<TimingSettings>
{
    public SettingsChangedMessage(TimingSettings settings) : base(settings)
    {
    }
}
