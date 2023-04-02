using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.Models;
using Project.DAL;

namespace Project.BL.Mappers
{
    public class UserModelMapper : ModelMapperBase<User, UserListModel, UserDetailModel>
    {
        public override UserListModel MapToListModel(User? entity)
            => entity is null
            ? UserListModel.Empty
            : new UserListModel { Id = entity.Id, UserFirstName = entity.FirstName , UserLastName = entity.LastName, UserPhotoUrl = entity.PhotoUrl};

        public override UserDetailModel MapToDetailModel(User? entity)
            => entity is null
            ? UserDetailModel.Empty
            : new UserDetailModel { Id = entity.Id, UserFirstName = entity.FirstName, UserLastName = entity.LastName, UserPhotoUrl= entity.PhotoUrl};

        public override User MapToEntity(UserDetailModel model)
            => new() { Id = model.Id, FirstName = model.UserFirstName, LastName = model.UserLastName, PhotoUrl = model.UserPhotoUrl };
    }
}
