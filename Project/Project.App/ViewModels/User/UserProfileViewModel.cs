using Microsoft.Maui.Controls;
using Project.App.Services;
using Project.BL.Facades;
using CommunityToolkit.Mvvm.Input;
using Project.DAL;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;

namespace Project.App.ViewModels.User;


public partial class UserProfileViewModel : ViewModelBase, IRecipient<UserPickedMessage>
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;


    public string FirstName { get; set; } = (string) Shell.Current.Resources["firstName"];
    public string SurName { get; set; } = (string)Shell.Current.Resources["surName"];
    public Color FrameBackgroundColor { get; set; } = Color.FromRgba(217, 217, 217, 255);
    public string ImageFileString { get; set; }
    private ImageSource _profilePicSource = (string)Shell.Current.Resources["userPic"];
    public bool ImgPicked = false;

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

    public UserProfileViewModel(
        IUserFacade userFacade,
        IMessengerService messengerService,
        INavigationService navigationService)
        : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        FirstName = (string)Shell.Current.Resources["firstName"];
        SurName = (string)Shell.Current.Resources["surName"];
        ProfilePicSource = (string)Shell.Current.Resources["userPic"];
    }

    [RelayCommand]
    private async Task PickPhoto()
    {
        var result = await MediaPicker.PickPhotoAsync();
        if (result != null)
        {
            ProfilePicSource = ImageSource.FromFile(result.FullPath);
            if (ProfilePicSource != null) 
            {
                ImageFileString = ProfilePicSource.ToString();
                ImageFileString = ImageFileString.Replace("File: ", "");
                ImgPicked = true;
            }
        }
    }
    [RelayCommand]
    private async Task UpdateUserProfile()
    {
        if (FirstName != null && SurName != null)
        {
            Guid currentUserId = (Guid)Shell.Current.Resources["userId"];
            if (ImageFileString != null) 
            {
                await _userFacade.SaveAsync(new BL.Models.UserDetailModel
                {
                    Id = currentUserId,
                    UserFirstName = FirstName,
                    UserLastName = SurName,
                    UserPhotoUrl = ImageFileString
                });
                Shell.Current.Resources["userPic"] = ImageFileString;
            } else
            {
                await _userFacade.SaveAsync(new BL.Models.UserDetailModel
                {
                    Id = currentUserId,
                    UserFirstName = FirstName,
                    UserLastName = SurName,
                    UserPhotoUrl = (string)Shell.Current.Resources["userPic"]
                });
            }
        }
    }

    public async void Receive(UserPickedMessage message)
    {
        await LoadDataAsync();
    }
}
