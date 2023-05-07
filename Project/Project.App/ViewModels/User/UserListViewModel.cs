using Project.BL.Models;
using Project.App.Views.User;
using CommunityToolkit.Mvvm.Input;
using Project.BL.Facades;
using Project.App.Services;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;


namespace Project.App.ViewModels.User;

public partial class UserListViewModel : ViewModelBase, IRecipient<UserCreatedMessage>
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
    private async Task GoToUserProfile(Guid UserId)
    {
        string UserImage = Users.Where(i => i.Id == UserId).Single().UserPhotoUrl;
        string UserFirstName = Users.Where(i => i.Id == UserId).Single().UserFirstName;
        string UserLastName = Users.Where(i => i.Id == UserId).Single().UserLastName;
        Shell.Current.Resources.Add("userId", UserId);
        Shell.Current.Resources["userPic"] = UserImage;
        Shell.Current.Resources["firstName"] = UserFirstName;
        Shell.Current.Resources["surName"] = UserLastName;

        await _navigationService.GoToAsync("//user-profile");
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Users = await _userFacade.GetAsync();
    }
    public async void Receive(UserCreatedMessage message)
    {
        await LoadDataAsync();
    }

}
