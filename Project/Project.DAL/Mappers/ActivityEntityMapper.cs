namespace Project.DAL.Mappers;
public class ActivityEntityMapper : IEntityMapper<Activity>
    {
        public void MapToExistingEntity(Activity existingEntity, Activity newEntity)
        {
            existingEntity.Name = newEntity.Name;
            existingEntity.TimeBegin = newEntity.TimeBegin;
            existingEntity.TimeEnd = newEntity.TimeEnd;
            existingEntity.Type = newEntity.Type;
            existingEntity.ActivityDescription = newEntity.ActivityDescription;

        }
    }
