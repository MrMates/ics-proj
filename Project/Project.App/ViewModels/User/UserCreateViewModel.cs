using Project.App.Services;
using Project.BL.Facades;
using CommunityToolkit.Mvvm.Input;
using Project.DAL;
using Project.App.Messages;
using Microsoft.Maui.Controls;

namespace Project.App.ViewModels.User;


public partial class UserCreateViewModel : ViewModelBase
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;


    public string FirstName { get; set; }
    public string SurName { get; set; }
    public Color FrameBackgroundColor { get; set; } = Color.FromRgba(217, 217, 217, 255);
    public string ImageFileString { get; set; }
    private ImageSource _profilePicSource;
    public ImageSource ProfilePicSource
    {
        get => _profilePicSource;
        set => SetProperty(ref _profilePicSource, value);
    }


    public UserCreateViewModel(
        IUserFacade userFacade,
        IMessengerService messengerService,
        INavigationService navigationService)
        : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task PickPhoto()
    {
        var result = await MediaPicker.PickPhotoAsync();
        if (result != null)
        {
            ProfilePicSource = ImageSource.FromFile(result.FullPath);
            ImageFileString = ProfilePicSource.ToString();
            ImageFileString = ImageFileString.Replace("File: ", "");
        }
    }
    [RelayCommand]
    private async Task Create_User_Handler()
    {
        if (FirstName != null && SurName != null)   
        {
            await _userFacade.SaveAsync(new BL.Models.UserDetailModel { UserFirstName = FirstName, 
                                                                        UserLastName = SurName, 
                                                                        UserPhotoUrl = ImageFileString });
            await _navigationService.GoToAsync("//users");
            MessengerService.Send<UserCreatedMessage>(new UserCreatedMessage(Guid.Empty));

        }
    }
    


}
