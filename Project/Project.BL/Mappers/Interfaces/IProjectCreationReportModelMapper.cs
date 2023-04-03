using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.Models;
using DAL = Project.DAL;

namespace Project.BL.Mappers.Interfaces
{
    public interface IProjectCreationReportModelMapper : IModelMapper<DAL.Project, ProjectReportListModel, ProjectCreationDetailModel>
    {
    }
}
