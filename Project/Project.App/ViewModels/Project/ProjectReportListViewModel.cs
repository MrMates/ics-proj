using Project.BL.Models;
using Project.App.Views.Project;
using CommunityToolkit.Mvvm.Input;
using Project.BL.Facades;
using Project.App.Services;
using Project.DAL;
using Project.DAL.Seeds;
using System.Windows.Input;

namespace Project.App.ViewModels.Project;

public partial class ProjectReportListViewModel : ViewModelBase
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;
    private double whole = 0.0;
    private TimeSpan totalTime = TimeSpan.Zero;
    private bool _isTimeSpentSortedAscending = true;
    private bool _isActivityNameSortedAscending = true;

    public ICommand LoadDataCommand => new Command(async () => await LoadDataAsync());
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
     
        

        if (Shell.Current.Resources.TryGetValue("userId", out object userIdObj) && userIdObj is Guid userId)
        { 
            Reports = (await _activityFacade.GetUserActivities(userId)).ToList(); 
        
            whole = 0.0;
            totalTime = TimeSpan.Zero;
            foreach (ActivityListModel activity in Reports)
            {
                activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
                whole += activity.TimeSpent.TotalSeconds;
                totalTime += activity.TimeSpent;
            }
            foreach (ActivityListModel activity in Reports)
            {
                activity.Percentage = activity.TimeSpent.TotalSeconds / whole * 100;
            }
            TotalTime = totalTime;
        }
        else
        {
            Reports = null;
            TotalTime = TimeSpan.Zero;
        }
    }

    [RelayCommand]
    private async Task LoadPreviousMonth()
    {
        if (Shell.Current.Resources.TryGetValue("userId", out object userIdObj) && userIdObj is Guid userId)
        {
            Reports = (await _activityFacade.GetPreviousMonth(userId)).ToList();
            whole = 0.0;
            totalTime = TimeSpan.Zero;
            foreach (ActivityListModel activity in Reports)
            {
                activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
                whole += activity.TimeSpent.TotalSeconds;
                totalTime += activity.TimeSpent;
            }
            foreach (ActivityListModel activity in Reports)
            {
                activity.Percentage = activity.TimeSpent.TotalSeconds / whole * 100;
            }
            TotalTime = totalTime;
        }
        else
        {
            Reports = null;
            TotalTime = TimeSpan.Zero;
        }
    }

    [RelayCommand]
    private async Task LoadPastWeek()
    {
        if (Shell.Current.Resources.TryGetValue("userId", out object userIdObj) && userIdObj is Guid userId)
        {
            Reports = (await _activityFacade.GetPastWeek(userId)).ToList();
            whole = 0.0;
            totalTime = TimeSpan.Zero;
            foreach (ActivityListModel activity in Reports)
            {
                activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
                whole += activity.TimeSpent.TotalSeconds;
                totalTime += activity.TimeSpent;
            }
            foreach (ActivityListModel activity in Reports)
            {
                activity.Percentage = activity.TimeSpent.TotalSeconds / whole * 100;
            }
            TotalTime = totalTime;
        }
        else
        {
            Reports = null;
            TotalTime = TimeSpan.Zero;
        }

    }

    [RelayCommand]
    private async Task LoadPastMonth()
    {
        if (Shell.Current.Resources.TryGetValue("userId", out object userIdObj) && userIdObj is Guid userId)
        {
            Reports = (await _activityFacade.GetPastMonth(userId)).ToList();
            whole = 0.0;
            totalTime = TimeSpan.Zero;
            foreach (ActivityListModel activity in Reports)
            {
                activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
                whole += activity.TimeSpent.TotalSeconds;
                totalTime += activity.TimeSpent;
            }
            foreach (ActivityListModel activity in Reports)
            {
                activity.Percentage = activity.TimeSpent.TotalSeconds / whole * 100;
            }
            TotalTime = totalTime;
        }
        else
        {
            Reports = null;
            TotalTime = TimeSpan.Zero;
        }

    }

    [RelayCommand]
    private async Task LoadPastYear()
    {
        if (Shell.Current.Resources.TryGetValue("userId", out object userIdObj) && userIdObj is Guid userId)
        {
            Reports = (await _activityFacade.GetPastYear(userId)).ToList();
            whole = 0.0;
            totalTime = TimeSpan.Zero;
            foreach (ActivityListModel activity in Reports)
            {
                activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
                whole += activity.TimeSpent.TotalSeconds;
                totalTime += activity.TimeSpent;
            }
            foreach (ActivityListModel activity in Reports)
            {
                activity.Percentage = activity.TimeSpent.TotalSeconds / whole * 100;
            }
            TotalTime = totalTime;
        }
        else
        {
            Reports = null;
            TotalTime = TimeSpan.Zero;
        }

    }




    [RelayCommand]
    private void SortByActivityName()
    {
        if (_isActivityNameSortedAscending)
        {
            Reports = Reports.OrderBy(x => x.ActivityName);
        }
        else
        {
            Reports = Reports.OrderByDescending(x => x.ActivityName);
        }

        _isActivityNameSortedAscending = !_isActivityNameSortedAscending;
    }

}

