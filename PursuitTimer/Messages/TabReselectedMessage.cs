using CommunityToolkit.Mvvm.Messaging.Messages;

namespace PursuitTimer.Messages
{
    public class TabReselectedMessage : ValueChangedMessage<string>
    {
        public TabReselectedMessage(string value) : base(value)
        {
        }
    }
}