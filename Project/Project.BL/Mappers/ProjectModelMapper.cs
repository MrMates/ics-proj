using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.Models;
using Project.DAL;

namespace Project.BL.Mappers
{
    public class ProjectModelMapper : ModelMapperBase<DAL.Project, ProjectListModel, ProjectDetailModel>
    {
        public override ProjectListModel MapToListModel(DAL.Project? entity)
            => entity is null
            ? ProjectListModel.empty
            : new ProjectListModel { Id= entity.Id, ProjectName = entity.Name};

        public override ProjectDetailModel MapToDetailModel(DAL.Project? entity)
            => entity is null
            ? ProjectDetailModel.Empty
            : new ProjectDetailModel { };

        public override DAL.Project MapToEntity(ProjectDetailModel model)
            => new() { /*TODO*/};
    }
}
