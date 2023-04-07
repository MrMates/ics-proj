using Project.BL.Models;
using DAL = Project.DAL;

namespace Project.BL.Facades;

public interface IProjectCreationReportFacade : IFacade<DAL::Project, ProjectReportListModel, ProjectCreationDetailModel>
{
}