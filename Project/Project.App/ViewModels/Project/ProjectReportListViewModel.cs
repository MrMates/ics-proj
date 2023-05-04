using Project.BL.Models;
using Project.App.Views.Project;
using CommunityToolkit.Mvvm.Input;
using Project.BL.Facades;
using Project.App.Services;
using Project.DAL;
using Project.DAL.Seeds;

namespace Project.App.ViewModels.Project;

public partial class ProjectReportListViewModel : ViewModelBase
{
    private readonly IProjectCreationReportFacade _projectCreationReportFacade ;
    private readonly INavigationService _navigationService;
    public IEnumerable<ProjectReportListModel> Reports { get; set; } = null!;


    public ProjectReportListViewModel(
        IProjectCreationReportFacade projectReportFacade,
        IMessengerService messengerService,
        INavigationService navigationService)
        : base(messengerService)
    {
        _projectCreationReportFacade = projectReportFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Reports = await _projectCreationReportFacade.GetAsync();
    }
}

