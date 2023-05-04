using Project.BL.Models;
using Project.App.Views.Project;
using CommunityToolkit.Mvvm.Input;
using Project.BL.Facades;
using Project.App.Services;
using Project.DAL;
using Project.DAL.Seeds;

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
    }

    [RelayCommand]
    private async Task GoToCreateProject()
    {
        await _navigationService.GoToAsync("/create");
    }
}