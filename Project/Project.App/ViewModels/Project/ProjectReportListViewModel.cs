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
    private readonly IActivityFacade _activityFacade ;
    private readonly INavigationService _navigationService;
    public IEnumerable<ActivityListModel> Reports { get; set; } = null!;
    public TimeSpan TotalTime { get; set; }

    public ProjectReportListViewModel(
        IActivityFacade activityFacade,
        IMessengerService messengerService,
        INavigationService navigationService)
        : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        //TODO - userId brát jako aktivního uživatele

        if (Shell.Current.Resources.TryGetValue("userId", out object userIdObj) && userIdObj is Guid userId)
        { 
            Reports = (await _activityFacade.GetUserActivities(userId)).ToList(); 
        }
        else { 
            Reports = null;
            return;
        }
        

        double whole = 0.0;
        TimeSpan totalTime = TimeSpan.Zero;
        foreach (ActivityListModel activity in Reports)
        {
            activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
            whole += activity.TimeSpent.TotalSeconds;
            totalTime += activity.TimeSpent;
        }
        foreach (ActivityListModel activity in Reports)
        {
            activity.Percentage = activity.TimeSpent.TotalSeconds / whole*100;
        }
        TotalTime = totalTime;
    }


}

