using Microsoft.Maui.Controls;
using Project.App.Services;
using Project.BL.Facades;
using CommunityToolkit.Mvvm.Input;

namespace Project.App.ViewModels.User;


public partial class UserCreateViewModel : ViewModelBase
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;


    public string FirstName { get; set; }
    public string SurName { get; set; }


    public UserCreateViewModel(
        IUserFacade userFacade,
        IMessengerService messengerService,
        INavigationService navigationService)
        : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
        _navigationService.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task PickPhoto()
    {
        var result = await MediaPicker.PickPhotoAsync();
        if (result != null)
        {
            var imageSource = ImageSource.FromFile(result.FullPath);
            //profilePic.Source = imageSource;
            //frame.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
        }
    }
    [RelayCommand]
    private async Task Create_User_Handler()
    {
        if (FirstName != null && SurName != null)   
        {
            await _userFacade.SaveAsync(new BL.Models.UserDetailModel { UserFirstName = FirstName, UserLastName = SurName });
        }
    }

}
