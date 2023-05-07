using Project.BL.Models;
using Project.App.Views.Project;
using CommunityToolkit.Mvvm.Input;
using Project.BL.Facades;
using Project.App.Services;
using Project.DAL;
using Project.DAL.Seeds;
using System.Windows.Input;
using System.Globalization;

namespace Project.App.ViewModels.Project;

public partial class ProjectReportListViewModel : ViewModelBase
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;
    private double _whole = 0.0;
    private TimeSpan _totalTime = TimeSpan.Zero;
    private bool _isTimeSpentSortedAscending = true;
    private bool _isActivityNameSortedAscending = true;
    private DateTime _dateBefore;
    private DateTime _dateAfter;
    private IEnumerable<ActivityListModel> _beforeReports;
    private IEnumerable<ActivityListModel> _afterReports;
    public ICommand LoadDataCommand => new Command(async () => await LoadDataAsync());
    public IEnumerable<ActivityListModel> Reports { get; set; } = null!;
    public TimeSpan TotalTime { get; set; }
    public DateTime DateBefore { get; set; } = DateTime.Now;
    public DateTime DateAfter { get; set; } 
    

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
        
            _whole = 0.0;
            _totalTime = TimeSpan.Zero;
            foreach (ActivityListModel activity in Reports)
            {
                activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
                _whole += activity.TimeSpent.TotalSeconds;
                _totalTime += activity.TimeSpent;
            }
            foreach (ActivityListModel activity in Reports)
            {
                activity.Percentage = activity.TimeSpent.TotalSeconds / _whole * 100;
            }
            TotalTime = _totalTime;
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
            _whole = 0.0;
            _totalTime = TimeSpan.Zero;
            foreach (ActivityListModel activity in Reports)
            {
                activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
                _whole += activity.TimeSpent.TotalSeconds;
                _totalTime += activity.TimeSpent;
            }
            foreach (ActivityListModel activity in Reports)
            {
                activity.Percentage = activity.TimeSpent.TotalSeconds / _whole * 100;
            }
            TotalTime = _totalTime;
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
            _whole = 0.0;
            _totalTime = TimeSpan.Zero;
            foreach (ActivityListModel activity in Reports)
            {
                activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
                _whole += activity.TimeSpent.TotalSeconds;
                _totalTime += activity.TimeSpent;
            }
            foreach (ActivityListModel activity in Reports)
            {
                activity.Percentage = activity.TimeSpent.TotalSeconds / _whole * 100;
            }
            TotalTime = _totalTime;
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
            _whole = 0.0;
            _totalTime = TimeSpan.Zero;
            foreach (ActivityListModel activity in Reports)
            {
                activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
                _whole += activity.TimeSpent.TotalSeconds;
                _totalTime += activity.TimeSpent;
            }
            foreach (ActivityListModel activity in Reports)
            {
                activity.Percentage = activity.TimeSpent.TotalSeconds / _whole * 100;
            }
            TotalTime = _totalTime;
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
            _whole = 0.0;
            _totalTime = TimeSpan.Zero;
            foreach (ActivityListModel activity in Reports)
            {
                activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
                _whole += activity.TimeSpent.TotalSeconds;
                _totalTime += activity.TimeSpent;
            }
            foreach (ActivityListModel activity in Reports)
            {
                activity.Percentage = activity.TimeSpent.TotalSeconds / _whole * 100;
            }
            TotalTime = _totalTime;
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


    [RelayCommand]
    private async Task LoadStartTime()
    {
        if (Shell.Current.Resources.TryGetValue("userId", out object userIdObj) && userIdObj is Guid userId)
        {
            _beforeReports = (await _activityFacade.GetBeginningBefore(DateBefore));
            _afterReports = (await _activityFacade.GetBeginningAfter(DateAfter));

           Reports = _beforeReports.Intersect(_afterReports).ToList();
            _whole = 0.0;
            _totalTime = TimeSpan.Zero;
            foreach (ActivityListModel activity in Reports)
            {
                activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
                _whole += activity.TimeSpent.TotalSeconds;
                _totalTime += activity.TimeSpent;
            }
            foreach (ActivityListModel activity in Reports)
            {
                activity.Percentage = activity.TimeSpent.TotalSeconds / _whole * 100;
            }
            TotalTime = _totalTime;
        }
        else
        {
            Reports = null;
            TotalTime = TimeSpan.Zero;
        }

    }

    [RelayCommand]
    private async Task LoadEndTime()
    {
        if (Shell.Current.Resources.TryGetValue("userId", out object userIdObj) && userIdObj is Guid userId)
        {
            _beforeReports = (await _activityFacade.GetEndingBefore(DateBefore));
            _afterReports = (await _activityFacade.GetEndingAfter(DateAfter));

            Reports = _beforeReports.Intersect(_afterReports).ToList();
            _whole = 0.0;
            _totalTime = TimeSpan.Zero;
            foreach (ActivityListModel activity in Reports)
            {
                activity.TimeSpent = await _activityFacade.ActivityTimeSpent(activity.Id);
                _whole += activity.TimeSpent.TotalSeconds;
                _totalTime += activity.TimeSpent;
            }
            foreach (ActivityListModel activity in Reports)
            {
                activity.Percentage = activity.TimeSpent.TotalSeconds / _whole * 100;
            }
            TotalTime = _totalTime;
        }
        else
        {
            Reports = null;
            TotalTime = TimeSpan.Zero;
        }

    }

}

