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
            .FirstAsync(activity => activity.Id == activityID);

        DAL.Project project = await projectRep
            .Get()
            .Include(project => project.Activities)
            .FirstAsync(project => project.Id == projectID);

        if (activity != null && project != null)
        {
            activity.ProjectId = projectID;
            project.Activities.Add(activity);
            await activityRep.UpdateAsync(activity);
            await projectRep.UpdateAsync(project);
            await uow.CommitAsync();
        }
    }

    public async Task<TimeSpan> TotalTimeSpent(Guid projectID)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        DAL.Project project = await uow
            .GetRepository<DAL.Project, ProjectEntityMapper>()
            .Get()
            .Include(i => i.Activities)
            .FirstAsync(project => project.Id == projectID);

        TimeSpan sum = new 
            TimeSpan(project.Activities
            .Where(activity => activity.TimeEnd != null)
            .Sum(activity => (activity.TimeEnd! - activity.TimeBegin).Value.Ticks));

        return sum;
    }
}