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
    public Color FrameBackgroundColor { get; set; }
    public string ImageFileString { get; set; }


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
        FrameBackgroundColor = Color.FromRgba(0, 255, 0, 0);
        //Debug.WriteLine(FrameBackgroundColor.ToString());
        var result = await MediaPicker.PickPhotoAsync();
        if (result != null)
        {
            var imageSource = ImageSource.FromFile(result.FullPath);
            ImageFileString = imageSource.ToString();
            ImageFileString = ImageFileString.Replace("File: ", "");
            //profilePic.Source = imageSource;
            //_frame.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
        }
    }
    [RelayCommand]
    private async Task Create_User_Handler()
    {
        if (FirstName != null && SurName != null)
        {
            Guid currentUserId = (Guid) Shell.Current.Resources["userId"];
            await _userFacade.SaveAsync(new BL.Models.UserDetailModel { Id = currentUserId,
                                                                        UserFirstName = FirstName, 
                                                                        UserLastName = SurName, 
                                                                        UserPhotoUrl = ImageFileString });
        }
    }



}
