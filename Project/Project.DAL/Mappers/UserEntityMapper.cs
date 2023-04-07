namespace Project.DAL.Mappers
{
    public class UserEntityMapper : IEntityMapper<User>
    {
        public void MapToExistingEntity(User existingEntity, User newEntity)
        {
            existingEntity.FirstName = newEntity.FirstName;
            existingEntity.LastName = newEntity.LastName;
            existingEntity.PhotoUrl = newEntity.PhotoUrl;
        }
    }
}
