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
    private readonly IUserFacade _userFacade;
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;
    private bool _isTimeSpentSortedAscending = true;
    private bool _isActivityNameSortedAscending = true;

    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;
    public UserDetailModel ActivityUser { get; set; } = null!;


    //TODO:
    //Aby fungovalo na proklik (načtení ProjectId z Shell.Current.Resources

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
        if (Shell.Current.Resources.TryGetValue("projectId", out object projectId) && projectId is Guid id)
        {
            Project = await _projectFacade.GetAsync(id);
            Activities = (await _projectFacade.GetActivitiesInProject(id)).ToList();

            foreach (ActivityListModel activity in Activities)
            {
                if (activity.UserId != Guid.Empty)
                {
                    ActivityUser = await _userFacade.GetAsync(activity.UserId);
                    activity.UserName = ActivityUser.UserFirstName + " " + ActivityUser.UserLastName;
                }
                else
                {
                    activity.UserName = "Unknown user";
                }
                activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
            }
        }
    }

    [RelayCommand]
    private async Task DeleteActivity(Guid activityId)
    {
        bool confirmed = await Application.Current.MainPage.DisplayAlert("Delete Activity", "Are you sure you want to delete this activity?", "Yes", "No");

        if (confirmed)
        {
            await _activityFacade.DeleteAsync(activityId);
            await LoadDataAsync();
        }
    }

    [RelayCommand]
    private async Task ActivityDescription(Guid activityId)
    {
        ActivityDetailModel activity = await _activityFacade.GetAsync(activityId);
        await Application.Current.MainPage.DisplayAlert("Description of " + activity.ActivityName, activity.ActivityDescription, "Thanks!");
    }


    [RelayCommand]
    private void SortByProjectName()
    {
        if (_isActivityNameSortedAscending)
        {
            Activities = Activities.OrderBy(x => x.ActivityName);
        }
        else
        {
            Activities = Activities.OrderByDescending(x => x.ActivityName);
        }

        _isActivityNameSortedAscending = !_isActivityNameSortedAscending;
    }

    [RelayCommand]
    private void SortByTimeSpent()
    {
        if (_isTimeSpentSortedAscending)
        {
            Activities = Activities.OrderBy(x => x.TimeSpent);
        }
        else
        {
            Activities = Activities.OrderByDescending(x => x.ActivityName);
        }

        _isTimeSpentSortedAscending = !_isTimeSpentSortedAscending;
    }

}

