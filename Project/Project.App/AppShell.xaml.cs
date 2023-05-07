using CommunityToolkit.Mvvm.Input;
using Project.App.ViewModels.User;
using Project.App.ViewModels.Project;
using Project.App.ViewModels.Timer;
using Project.App.Services;
using Project.App.Views.User;

namespace Project.App
{
    public partial class AppShell
    {
        private readonly INavigationService _navigationService;

        public AppShell(INavigationService navigationService)
        {
            _navigationService = navigationService;

            InitializeComponent();
        }

        [RelayCommand]
        private async Task GoToUserProfilesAsync()
            => await _navigationService.GoToAsync<UserProfileViewModel>();

        [RelayCommand]
        private async Task GoToUsersAsync()
        {
            Shell.Current.Resources.Remove("userId");
            Shell.Current.Resources.Remove("userPic");
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