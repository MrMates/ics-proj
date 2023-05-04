using CommunityToolkit.Mvvm.Input;
using Project.App.ViewModels.User;
using Project.App.ViewModels.Project;
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
        private async Task GoToProjectsAsync()
            => await _navigationService.GoToAsync<ProjectListViewModel>();
        
        [RelayCommand]
        private async Task GoToProjectReportsAsync()
            => await _navigationService.GoToAsync<ProjectReportListViewModel>();
        
        [RelayCommand]
        private async Task GoToProjectDetailAsync()
            => await  _navigationService.GoToAsync<ProjectDetailViewModel>();

    }
}