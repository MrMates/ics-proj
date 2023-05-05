using Microsoft.Maui.Controls;
using Project.App.Services;
using Project.BL.Facades;
using Project.App.Messages;
using CommunityToolkit.Mvvm.Input;
using Project.BL.Models;

namespace Project.App.ViewModels.Project;


public partial class ProjectCreateViewModel : ViewModelBase
{
    //private readonly IUserFacade _userFacade;
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;
    public string Name { get; set; }

    public ProjectCreateViewModel(
        IProjectFacade projectFacade,
        IMessengerService messengerService,
        INavigationService navigationService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
    }




    [RelayCommand]
    private async Task Create_Project_Handler()
    {
        if (Name != null)
        {
            await _projectFacade.SaveAsync(new BL.Models.ProjectDetailModel { ProjectName = Name});
            await _navigationService.GoToAsync("//projects");

            MessengerService.Send<ProjectCreatedMessage>(new ProjectCreatedMessage(Guid.Empty));
        }
    }

}
