using Microsoft.Maui.Controls;
using Project.App.Services;
using Project.BL.Facades;
using CommunityToolkit.Mvvm.Input;

namespace Project.App.ViewModels.User;


public partial class UserProfileViewModel : ViewModelBase
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;


    public string FirstName { get; set; }
    public string SurName { get; set; }
    public Color FrameBackgroundColor { get; set; } = Color.FromRgba(217, 217, 217, 255);
    public string ImageFileString { get; set; }
    private ImageSource _profilePicSource = (string)Shell.Current.Resources["userPic"];

    public ImageSource ProfilePicSource
    {
        get => _profilePicSource;
        set 
        { 
            if (_profilePicSource != null)
            {
                SetProperty(ref _profilePicSource, value);

            }
        }
    }
    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }


    public UserProfileViewModel(
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
        IsLoading = true;
        var result = await MediaPicker.PickPhotoAsync();
        if (result != null)
        {
            ProfilePicSource = ImageSource.FromFile(result.FullPath);
            if (ProfilePicSource != null) 
            {
                ImageFileString = ProfilePicSource.ToString();
                ImageFileString = ImageFileString.Replace("File: ", "");
            }
            
        }
        IsLoading = false;
    }
    [RelayCommand]
    private async Task UpdateUserProfile()
    {
        if (FirstName != null && SurName != null)
        {
            Guid currentUserId = (Guid)Shell.Current.Resources["userId"];
            await _userFacade.SaveAsync(new BL.Models.UserDetailModel { Id = currentUserId,
                UserFirstName = FirstName,
                UserLastName = SurName,
                UserPhotoUrl = ImageFileString });
            Shell.Current.Resources["userPic"] = ImageFileString;
        }
    }
}
