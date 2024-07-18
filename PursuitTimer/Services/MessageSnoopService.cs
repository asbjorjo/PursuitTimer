using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace PursuitTimer.Services
{
    public partial class MessageSnoopService : IRecipient<PropertyChangedMessage<string>>, IRecipient<PropertyChangedMessage<bool>>, IRecipient<PropertyChangedMessage<object>>
    {
        private readonly IMessenger _messenger;

        public MessageSnoopService(IMessenger messenger) {
            _messenger = messenger;

            messenger.RegisterAll(this);
        }

        public void Receive(PropertyChangedMessage<bool> message)
        {
            Console.WriteLine($"{message.Sender}: {message.PropertyName} - {message.OldValue} -> {message.NewValue}");
        }

        public void Receive(PropertyChangedMessage<string> message)
        {
            Console.WriteLine($"{message.Sender}: {message.PropertyName} - {message.OldValue} -> {message.NewValue}");
        }

        public void Receive(PropertyChangedMessage<object> message)
        {
            Console.WriteLine($"{message.Sender}: {message.PropertyName} - {message.OldValue} -> {message.NewValue}");
        }
    }
}
