using DAL = Project.DAL;

namespace Project.DAL.Mappers
{
    public class ProjectEntityMapper : IEntityMapper<DAL::Project>
    {
        public void MapToExistingEntity(DAL::Project existingEntity, DAL::Project newEntity)
        {
            existingEntity.Name = newEntity.Name;

        }
    }
}