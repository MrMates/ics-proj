﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.Models;
using Project.DAL;

namespace Project.BL.Mappers
{

    public interface IProjectModelMapper : IModelMapper<Project, ProjectListModel, ProjectDetailModel>
    {
    }
}
