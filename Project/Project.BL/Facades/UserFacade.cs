using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.BL.Mappers;
using Project.BL.Models;
using Project.DAL;
using Project.DAL.Mappers;
using Project.DAL.Repositories;
using Project.DAL.UnitOfWork;

namespace Project.BL.Facades;

public class UserFacade :
    FacadeBase<User, UserListModel, UserDetailModel,
        UserEntityMapper>, IUserFacade
{
    private readonly IUserModelMapper _UserModelMapper;

    public UserFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserModelMapper UserModelMapper)
        : base(unitOfWorkFactory, UserModelMapper) =>
        _UserModelMapper = UserModelMapper;

    protected override string IncludesNavigationPathDetail =>
        $"{nameof(User.UserProjects)}.{nameof(UserProject.Project)}";

    public async Task<IEnumerable<ProjectListModel>> GetUsersProjects(Guid userID)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IEnumerable<DAL.Project> projects = await uow
            .GetRepository<DAL.Project, ProjectEntityMapper>()
            .Get()
            .Include(i => i.UserProjects)
            .Where(i => i.UserProjects.Any(i => i.UserId == userID))
            .ToListAsync();

        ProjectModelMapper mapper = new ProjectModelMapper();
        return mapper.MapToListModel(projects);
    }
}