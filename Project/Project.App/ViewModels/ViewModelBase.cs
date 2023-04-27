using CommunityToolkit.Mvvm.ComponentModel;
using Project.App.Services;
using System.ComponentModel;

namespace Project.App.ViewModels;

public abstract class ViewModelBase : ObservableRecipient, IViewModel
{
    protected readonly IMessengerService MessengerService;

    private bool _needsRefresh = true;

    protected ViewModelBase(IMessengerService messengerService)
        : base(messengerService.Messenger)
    {
        MessengerService = messengerService;
        IsActive = true;
    }

    public async Task OnAppearingAsync()
    {
        if (_needsRefresh)
        {
            await LoadDataAsync();
            _needsRefresh = false;
        }
    }

    protected virtual Task LoadDataAsync() => Task.CompletedTask;
}
