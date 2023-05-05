using Project.BL.Models;
using Project.App.Views.Project;
using CommunityToolkit.Mvvm.Input;
using Project.BL.Facades;
using Project.App.Services;
using Project.DAL;
using Project.DAL.Seeds;
using Project.DAL.Mappers;
using Project.DAL.UnitOfWork;

namespace Project.App.ViewModels.Project;

public partial class ProjectListViewModel : ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;
    public IEnumerable<ProjectListModel> Projects { get; set; } = null!;



    public ProjectListViewModel(
        IProjectFacade projectFacade,
        IMessengerService messengerService,
        INavigationService navigationService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
    }


    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Projects = await _projectFacade.GetAsync();


        foreach (ProjectListModel project in Projects)
        {
            project.TimeSpent = await _projectFacade.TotalTimeSpent(project.Id);
        }
    }

    [RelayCommand]
    private async Task User_Join_Project(Guid projectId)
    {
        if (Shell.Current.Resources.TryGetValue("userId", out object userIdObj) && userIdObj is Guid userId)
        {
            ProjectDetailModel project = await _projectFacade.GetAsync(projectId);
            await _projectFacade.AddUserToProject(userId,projectId);
            await _projectFacade.SaveAsync(project);
        }
    }


    [RelayCommand]
    private async Task GoToCreateProject()
    {
        await _navigationService.GoToAsync("/create");
    }
}