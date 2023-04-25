using Project.BL.Models;
using Project.DAL;

namespace Project.BL.Facades;

public interface IActivityFacade : IFacade<Activity, ActivityListModel, ActivityDetailModel>
{
    public abstract Task<IEnumerable<ActivityListModel>> GetPastWeek();
    public abstract Task<IEnumerable<ActivityListModel>> GetPastMonth();
    public abstract Task<IEnumerable<ActivityListModel>> GetPreviousMonth();
    public abstract Task<IEnumerable<ActivityListModel>> GetPastYear();
}
