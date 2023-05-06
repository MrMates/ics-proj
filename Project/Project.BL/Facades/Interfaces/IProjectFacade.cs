using Project.BL.Models;
using DAL = Project.DAL;

namespace Project.BL.Facades;

public interface IProjectFacade : IFacade<DAL::Project, ProjectListModel, ProjectDetailModel>
{
    public abstract Task AddActivityToProject(Guid activityID, Guid projectID);

    public abstract Task AddUserToProject(Guid userID, Guid projectID);

    public abstract Task<IEnumerable<UserListModel>> GetUsersInProject(Guid projectID);

    public abstract Task<IEnumerable<ActivityListModel>> GetActivitiesInProject(Guid projectID);

    public abstract Task<TimeSpan> TotalTimeSpent(Guid projectID);
}