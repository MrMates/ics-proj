using Project.BL.Models;
using DAL = Project.DAL;

namespace Project.BL.Facades;

public interface IProjectFacade : IFacade<DAL::Project, ProjectListModel, ProjectDetailModel>
{
    Task<ProjectCreationDetailModel> SaveAsync(ProjectCreationDetailModel model);
    Task<ProjectReportListModel> SaveAsync(ProjectReportListModel model);
}