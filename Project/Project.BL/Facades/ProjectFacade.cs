using Project.DAL.UnitOfWork;
using DAL = Project.DAL;
using Project.BL.Models;
using Project.DAL.Mappers;
using Project.BL.Mappers;
using Project.DAL.Repositories;
using Project.DAL;

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
}