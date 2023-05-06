using CommunityToolkit.Mvvm.Input;
using Project.App.ViewModels.User;
using Project.App.ViewModels.Timer;
using Project.App.Services;

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
        private void BackAsync() => _navigationService.SendBackButtonPressed();

        [RelayCommand]
        private async Task GoToUsersAsync()
            => await _navigationService.GoToAsync<UserListViewModel>();

        [RelayCommand]
        private async Task GoToTimersAsync()
            => await _navigationService.GoToAsync<ActivityListViewModel>();

    }
}