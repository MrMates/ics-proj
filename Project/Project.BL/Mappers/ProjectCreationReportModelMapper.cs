using Project.BL.Mappers.Interfaces;
using Project.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = Project.DAL;

namespace Project.BL.Mappers
{
    public class ProjectCreationReportModelMapper : ModelMapperBase<DAL.Project, ProjectReportListModel, ProjectCreationDetailModel>, IProjectCreationReportModelMapper
    {
        public override ProjectReportListModel MapToListModel(DAL.Project? entity)
            => entity is null
            ? ProjectReportListModel.Empty
            : new ProjectReportListModel { Id = entity.Id, ProjectId = entity.Id, ProjectName = entity.Name };

        public override ProjectCreationDetailModel MapToDetailModel(DAL.Project entity)
            => entity is null
            ? ProjectCreationDetailModel.Empty
            : new ProjectCreationDetailModel { Id = entity.Id, ProjectName = entity.Name };

        public override DAL.Project MapToEntity(ProjectCreationDetailModel model)
            => new() { Id = model.Id, Name = model.ProjectName };
    }
}
