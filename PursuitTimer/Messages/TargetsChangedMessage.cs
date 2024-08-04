using CommunityToolkit.Mvvm.Messaging.Messages;
using PursuitTimer.Model;

namespace PursuitTimer.Messages
{
    public class TargetsChangedMessage : ValueChangedMessage<Targets>
    {
        public TargetsChangedMessage(Targets value) : base(value)
        {
        }
    }
}