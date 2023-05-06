using Project.BL.Models;
using Project.App.Views.Project;
using CommunityToolkit.Mvvm.Input;
using Project.BL.Facades;
using Project.App.Services;
using Project.DAL;
using Project.DAL.Seeds;
using AdSupport;

namespace Project.App.ViewModels.Project;

public partial class ProjectDetailViewModel : ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly IUserFacade _userFacade;
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;
    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;
    public UserDetailModel ActivityUser { get; set; } = null!;


    //TODO:
    //Aby fungovalo na proklik (načtení ProjectId z Shell.Current.Resources
    //Filtery dle času / Sorty dle názvu
    //Pokud půjde, tak tooltip nebo nějakej popup při hoveru nad danou aktivitou s jejím descriptionem :) 
    //Případně asi mazání aktivity stejně jako u ProjectListu

    public Guid Id = Guid.Parse("188BFF5B-FCC8-452E-A20E-AF0DEB0DDD1B");
    public ProjectDetailModel Project { get; set; }

    public string UserName = string.Empty;

    public ProjectDetailViewModel(
        IProjectFacade projectFacade,
        IUserFacade userFacade,
        IActivityFacade activityFacade,
        IMessengerService messengerService,
        INavigationService navigationService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _activityFacade = activityFacade;   
        _userFacade = userFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Project = await _projectFacade.GetAsync(Id);
        Activities = (await _projectFacade.GetActivitiesInProject(Id)).ToList();

        foreach (ActivityListModel activity in Activities)
        {
            if (activity.UserId != Guid.Empty)
            {
                ActivityUser = await _userFacade.GetAsync(activity.UserId);
                activity.UserName = ActivityUser.UserFirstName + " " + ActivityUser.UserLastName;
            }
            else
            {
                activity.UserName =  "Unknown user";
            }
            activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
        }
    }
}

