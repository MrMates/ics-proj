using Project.BL.Models;
using Project.App.Views.User;
using CommunityToolkit.Mvvm.Input;
using Project.BL.Facades;
using Project.App.Services;

namespace Project.App.ViewModels.User;

public partial class UserListViewModel : ViewModelBase
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<UserListModel> Users { get; set; } = null!;

    public UserListViewModel(
        IUserFacade userFacade, 
        IMessengerService messengerService,
        INavigationService navigationService)
        : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task GoToCreateUser()
    {
        await _navigationService.GoToAsync("/create");
    }
    [RelayCommand]
    private async Task GoToUserProfile()
    {
        await _navigationService.GoToAsync("//users");
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Users = await _userFacade.GetAsync();
    }
}
