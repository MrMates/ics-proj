using Project.DAL.UnitOfWork;
using DAL = Project.DAL;
using Project.BL.Models;
using Project.DAL.Mappers;
using Project.BL.Mappers;
using Project.DAL.Repositories;
using Project.DAL;
using Microsoft.EntityFrameworkCore;

namespace Project.BL.Facades;

public class ProjectFacade :
    FacadeBase<DAL::Project, ProjectListModel, ProjectDetailModel, ProjectEntityMapper>, IProjectFacade
{
    public ProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IProjectModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }

    public async Task AddActivityToProject(Guid activityID, Guid projectID)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        var activityRep = uow.GetRepository<Activity, ActivityEntityMapper>();
        var projectRep = uow.GetRepository<DAL.Project, ProjectEntityMapper>();

        Activity activity = await activityRep
            .Get()
            .SingleAsync(activity => activity.Id == activityID);

        DAL.Project project = await projectRep
            .Get()
            .SingleAsync(project => project.Id == projectID);

        if (activity != null && project != null)
        {
            activity.ProjectId = projectID;
            await activityRep.UpdateAsync(activity);
            await uow.CommitAsync();
        }
    }

    public async Task AddUserToProject(Guid userID, Guid projectID)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        var userRep = uow.GetRepository<User, UserEntityMapper>();
        var userProjectRep = uow.GetRepository<UserProject, UserProjectEntityMapper>();
        var projectRep = uow.GetRepository<DAL.Project, ProjectEntityMapper>();

        User user = await userRep
            .Get()
            .SingleAsync(user => user.Id == userID);

        DAL.Project project = await projectRep
            .Get()
            .SingleAsync(project => project.Id == projectID);

        if (user != null && project != null)
        {
            UserProject userProject = new UserProject() 
            { 
                Id = Guid.NewGuid(),
                ProjectId = project.Id,
                UserId = user.Id,
                Project = project,
                User = user
            };

            await userProjectRep.InsertAsync(userProject);
            await uow.CommitAsync();
        }
    }

    public async Task<IEnumerable<ActivityListModel>> GetActivitiesInProject(Guid projectID)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        DAL.Project project = await uow
            .GetRepository<DAL.Project, ProjectEntityMapper>()
            .Get()
            .Include(i => i.Activities)
            .Where(i => i.Id == projectID)
            .SingleAsync();

        ActivityModelMapper mapper = new ActivityModelMapper();
        return mapper.MapToListModel(project.Activities);
    }

    public async Task<IEnumerable<UserListModel>> GetUsersInProject(Guid projectID)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IEnumerable<User> users = await uow
            .GetRepository<User, UserEntityMapper>()
            .Get()
            .Include(i => i.UserProjects)
            .Where(i => i.UserProjects.Any(i => i.ProjectId == projectID))
            .ToListAsync();

        UserModelMapper mapper = new UserModelMapper();
        return mapper.MapToListModel(users);
    }

    public async Task<TimeSpan> TotalTimeSpent(Guid projectID)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        DAL.Project project = await uow
            .GetRepository<DAL.Project, ProjectEntityMapper>()
            .Get()
            .Include(i => i.Activities)
            .SingleAsync(project => project.Id == projectID);

        TimeSpan sum = new 
            TimeSpan(project.Activities
            .Where(activity => activity.TimeEnd != null)
            .Sum(activity => (activity.TimeEnd! - activity.TimeBegin).Value.Ticks));

        return sum;
    }

    public async Task<IEnumerable<ProjectListModel>> GetAsyncWithUsers()
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<DAL.Project> entities = await uow
            .GetRepository<DAL.Project, ProjectEntityMapper>()
            .Get()
            .Include(i => i.UserProjects)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }
}