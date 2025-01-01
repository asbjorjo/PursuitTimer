using CommunityToolkit.Mvvm.Messaging.Messages;

namespace PursuitTimer.Messages;

public class TimingStateChangedMessage : ValueChangedMessage<TimingState>
{
    public TimingStateChangedMessage(TimingState state) : base(state)
    {
    }
}
