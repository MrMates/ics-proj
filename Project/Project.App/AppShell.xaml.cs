using CommunityToolkit.Mvvm.Input;
using Project.App.ViewModels.User;
using Project.App.ViewModels.Project;
using Project.App.ViewModels.Timer;
using Project.App.Services;
using Project.App.Views.User;
using Project.DAL;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;
using Project.BL.Facades;

namespace Project.App
{
    public partial class AppShell
    {
        private readonly INavigationService _navigationService;
        protected readonly IMessengerService MessengerService;

        public AppShell(INavigationService navigationService,
            IMessengerService messengerService)
        {
            MessengerService = messengerService;
            _navigationService = navigationService;

            InitializeComponent();
        }

        [RelayCommand]
        private async Task GoToUserProfilesAsync()
        {
            await _navigationService.GoToAsync<UserProfileViewModel>();
        }
        [RelayCommand]
        private async Task GoToUsersAsync()
        {
            Shell.Current.Resources.Remove("userId");
            Shell.Current.Resources.Remove("userPic");
            Shell.Current.Resources.Remove("firstName");
            Shell.Current.Resources.Remove("surName");
            MessengerService.Send(new UserLoggedOutMessage());
            await _navigationService.GoToAsync<UserListViewModel>();
        }
            


        [RelayCommand]
        private async Task GoToProjectsAsync()
            => await _navigationService.GoToAsync<ProjectListViewModel>();
        
        [RelayCommand]
        private async Task GoToProjectReportsAsync()
            => await _navigationService.GoToAsync<ProjectReportListViewModel>();
        
        [RelayCommand]
        private async Task GoToProjectDetailAsync()
            => await  _navigationService.GoToAsync<ProjectDetailViewModel>();

        [RelayCommand]
        private async Task GoToTimersAsync()
            => await _navigationService.GoToAsync<TimerViewModel>();

    }
}