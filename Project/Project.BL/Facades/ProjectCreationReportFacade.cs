using Project.DAL.UnitOfWork;
using DAL = Project.DAL;
using Project.BL.Models;
using Project.DAL.Mappers;
using Project.BL.Mappers.Interfaces;

namespace Project.BL.Facades;

public class ProjectCreationReportFacade :
    FacadeBase<DAL::Project, ProjectReportListModel, ProjectCreationDetailModel, ProjectEntityMapper>, IProjectCreationReportFacade
{
    public ProjectCreationReportFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IProjectCreationReportModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }
}