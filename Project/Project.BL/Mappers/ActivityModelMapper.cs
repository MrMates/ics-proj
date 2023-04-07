using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DAL;
using Project.BL.Models;

namespace Project.BL.Mappers
{
    public class ActivityModelMapper : ModelMapperBase<Activity, ActivityListModel, ActivityDetailModel>, IActivityModelMapper
    {
        public override ActivityListModel MapToListModel(Activity? entity)
            => entity is null
            ? ActivityListModel.Empty
            : new ActivityListModel { Id= entity.Id,ProjectId = entity.ProjectId,ActivityName = entity.Name};

        public override ActivityDetailModel MapToDetailModel(Activity? entity)
            => entity is null
            ? ActivityDetailModel.Empty
            : new ActivityDetailModel { Id= entity.Id, UserId=entity.UserId, ProjectId = entity.ProjectId, ActivityName = entity.Name,TimeBegin = entity.TimeBegin, TimeEnd = entity.TimeEnd, ActivityType = entity.ActivityType, ActivityDescription = entity.ActivityDescription};

        public override Activity MapToEntity(ActivityDetailModel model)
            => new() { Id = model.Id, ProjectId = model.ProjectId, Name = model.ActivityName, ActivityType = model.ActivityType, UserId = model.UserId, TimeBegin = model.TimeBegin, TimeEnd = model.TimeEnd, ActivityDescription = model.ActivityDescription};
    }
}
