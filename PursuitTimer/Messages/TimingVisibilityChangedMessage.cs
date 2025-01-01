using CommunityToolkit.Mvvm.Messaging.Messages;

namespace PursuitTimer.Messages;

public class TimingVisibilityChangedMessage : ValueChangedMessage<bool>
{
    public TimingVisibilityChangedMessage(bool timingVisible) : base(timingVisible)
    {
    }
}
