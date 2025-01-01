using CommunityToolkit.Mvvm.Messaging.Messages;
using PursuitTimer.Models;

namespace PursuitTimer.Messages;

public class TimingTargetChangedMessage : ValueChangedMessage<TimingTarget>
{
    public TimingTargetChangedMessage(TimingTarget timingTarget) : base(timingTarget)
    {
    }
}
