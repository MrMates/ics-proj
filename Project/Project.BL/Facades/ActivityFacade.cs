using Project.DAL.UnitOfWork;
using Project.DAL;
using Project.BL.Models;
using Project.DAL.Mappers;
using Project.BL.Mappers;

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
}