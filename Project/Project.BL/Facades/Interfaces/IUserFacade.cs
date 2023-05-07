using Project.BL.Models;
using Project.DAL;

namespace Project.BL.Facades;

public interface IUserFacade : IFacade<User, UserListModel, UserDetailModel>
{
    public abstract Task<IEnumerable<ProjectListModel>> GetUsersProjects(Guid userID);
}
