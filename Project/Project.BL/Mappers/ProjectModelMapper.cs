﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.Models;
using Project.DAL;

namespace Project.BL.Mappers
{
    public class ProjectModelMapper : ModelMapperBase<DAL.Project, ProjectListModel, ProjectDetailModel>, IProjectModelMapper
    {
        public override ProjectListModel MapToListModel(DAL.Project? entity)
        {
            if (entity is null) return ProjectListModel.empty;

            var users = entity.UserProjects.Select(i => i.User);

            UserModelMapper mapper = new UserModelMapper();

            return entity is null
            ? ProjectListModel.empty
            : new ProjectListModel { Id = entity.Id, ProjectName = entity.Name, Users = mapper.MapToListModel(users).ToList() };
        }

        public override ProjectDetailModel MapToDetailModel(DAL.Project? entity)
            => entity is null
            ? ProjectDetailModel.Empty
            : new ProjectDetailModel {Id = entity.Id, ProjectName = entity.Name };

        public override DAL.Project MapToEntity(ProjectDetailModel model) 
            => new() { Id = model.Id, Name = model.ProjectName};
    }
}
