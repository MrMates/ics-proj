using Project.DAL.UnitOfWork;
using Project.DAL;
using Project.BL.Models;
using Project.DAL.Mappers;
using Project.BL.Mappers;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project.BL.Facades;

public class ActivityFacade :
    FacadeBase<Activity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>, IActivityFacade
{
    public ActivityFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IActivityModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }

    /// <summary>
    /// Returns a collection of activities that began (!) in the past week.
    /// </summary>
    /// <remarks>Past week is considered from the last Monday, not last 7 days.</remarks>
    /// <returns>Collection of ActivityListModels that began in the past week.</returns>
    public async Task<IEnumerable<ActivityListModel>> GetPastWeek()
    {
        DateTime dt = DateTime.Now;

        int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
        DateTime lastMonday = dt.AddDays(-1 * diff).Date;

        // Just to check we don't have any "future" activities
        DateTime endOfWeek = lastMonday.AddDays(7).AddSeconds(-1);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<Activity> entities = await uow
            .GetRepository<Activity, ActivityEntityMapper>()
            .Get()
            .Where(activity => activity.TimeBegin >= lastMonday && activity.TimeBegin <= endOfWeek)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    /// <summary>
    /// Returns a collection of activities that began (!) in the past month.
    /// </summary>
    /// <remarks>Past month is considered from the first day, not last 30/31 days.</remarks>
    /// <returns>Collection of ActivityListModels that began in the past month.</returns>
    public async Task<IEnumerable<ActivityListModel>> GetPastMonth()
    {
        DateTime dt = DateTime.Now;

        var firstDayOfMonth = new DateTime(dt.Year, dt.Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<Activity> entities = await uow
            .GetRepository<Activity, ActivityEntityMapper>()
            .Get()
            .Where(activity => activity.TimeBegin >= firstDayOfMonth && activity.TimeBegin <= lastDayOfMonth)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    /// <summary>
    /// Returns a collection of activities that began (!) in the previous month.
    /// </summary>
    /// <remarks>Previous month is considered from the first day.</remarks>
    /// <returns>Collection of ActivityListModels that began in the previous month.</returns>
    public async Task<IEnumerable<ActivityListModel>> GetPreviousMonth()
    {
        DateTime dt = DateTime.Now.AddMonths(-1);

        var firstDayOfMonth = new DateTime(dt.Year, dt.Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<Activity> entities = await uow
            .GetRepository<Activity, ActivityEntityMapper>()
            .Get()
            .Where(activity => activity.TimeBegin >= firstDayOfMonth && activity.TimeBegin <= lastDayOfMonth)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    /// <summary>
    /// Returns a collection of activities that began (!) in the past year.
    /// </summary>
    /// <remarks>Past year is considered from the first of January, not last 365 days.</remarks>
    /// <returns>Collection of ActivityListModels that began in the past year.</returns>
    public async Task<IEnumerable<ActivityListModel>> GetPastYear()
    {
        DateTime dt = DateTime.Now;

        var firstDayOfYear = new DateTime(dt.Year, 1, 1);
        var lastDayOfYear = firstDayOfYear.AddYears(1).AddSeconds(-1);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<Activity> entities = await uow
            .GetRepository<Activity, ActivityEntityMapper>()
            .Get()
            .Where(activity => activity.TimeBegin >= firstDayOfYear && activity.TimeBegin <= lastDayOfYear)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }
}