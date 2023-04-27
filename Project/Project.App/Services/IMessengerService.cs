using CommunityToolkit.Mvvm.Messaging;

namespace Project.App.Services;

public interface IMessengerService
{
    IMessenger Messenger { get; }

    void Send<T>(T message)
        where T : class;
}
