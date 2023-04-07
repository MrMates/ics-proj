namespace Project.DAL.Mappers
{
    public class UserProjectEntityMapper : IEntityMapper<UserProject>
    {
        public void MapToExistingEntity(UserProject existingEntity, UserProject newEntity)
        {
            existingEntity.User = newEntity.User;
            existingEntity.Project = newEntity.Project;
        }
    }
}
