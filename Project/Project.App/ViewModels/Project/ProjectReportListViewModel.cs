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

        await _activityFacade.AddActivityToProject(ActivitySeeds.DefaultActivity.Id, ProjectSeeds.DefaultProject.Id);

        Reports = (await _activityFacade.GetAsync()).ToList();
        double whole = 0.0;
        TimeSpan totalTime = TimeSpan.Zero;
        foreach (ActivityListModel activity in Reports)
        {
            //activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
            if (!activity.TimeEnd.HasValue)
            {
               activity.TimeSpent =  TimeSpan.FromSeconds(2545);
            }
            else
            {
                activity.TimeSpent = DateTime.Now - activity.TimeBegin;
            }
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

