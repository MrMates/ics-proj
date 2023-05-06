using Project.BL.Models;
using Project.App.Views.Project;
using CommunityToolkit.Mvvm.Input;
using Project.BL.Facades;
using Project.App.Services;
using Project.DAL;
using Project.DAL.Seeds;

namespace Project.App.ViewModels.Project;

public partial class ProjectDetailViewModel : ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;
    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

    public Guid Id = Guid.Parse("188BFF5B-FCC8-452E-A20E-AF0DEB0DDD1B");
    public ProjectDetailModel Project { get; set; }

    public ProjectDetailViewModel(
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
        Project = await _projectFacade.GetAsync(Id);
        Activities = null;
        //Activities = await _projectFacade.GetActivitiesInProject(Id);
    }
}

