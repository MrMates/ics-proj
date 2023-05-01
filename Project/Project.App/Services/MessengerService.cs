using CommunityToolkit.Mvvm.Messaging;

namespace Project.App.Services;

class MessengerService : IMessengerService
{
    public IMessenger Messenger { get; }

    public MessengerService(IMessenger messenger)
    {
        Messenger = messenger;
    }

    public void Send<T>(T message) 
        where T : class
    {
        Messenger.Send(message);
    }
}
