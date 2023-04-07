using Project.BL.Models;
using DAL = Project.DAL;

namespace Project.BL.Facades;

public interface IProjectFacade : IFacade<DAL::Project, ProjectListModel, ProjectDetailModel>
{
}