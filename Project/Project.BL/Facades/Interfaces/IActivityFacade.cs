using Project.BL.Models;
using Project.DAL;

namespace Project.BL.Facades;

public interface IActivityFacade : IFacade<Activity, ActivityListModel, ActivityDetailModel>
{
    public abstract Task<IEnumerable<ActivityListModel>> GetUserActivities(Guid userId);
    public abstract Task<TimeSpan> ActivityTimeSpent(Guid activityId);
    public abstract Task<IEnumerable<ActivityListModel>> GetPastWeek(Guid userId);
    public abstract Task<IEnumerable<ActivityListModel>> GetPastMonth(Guid userId);
    public abstract Task<IEnumerable<ActivityListModel>> GetPreviousMonth(Guid userId);
    public abstract Task<IEnumerable<ActivityListModel>> GetPastYear(Guid userId);
    public abstract Task<IEnumerable<ActivityListModel>> GetBeginningBefore(DateTime date);
    public abstract Task<IEnumerable<ActivityListModel>> GetBeginningAfter(DateTime date);
    public abstract Task<IEnumerable<ActivityListModel>> GetEndingBefore(DateTime date);
    public abstract Task<IEnumerable<ActivityListModel>> GetEndingAfter(DateTime date);

}
