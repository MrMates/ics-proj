using System;
using System.Threading.Tasks;
using Project.BL.Mappers;
using Project.BL.Models;
using Project.DAL;
using Project.DAL.Mappers;
using Project.DAL.Repositories;
using Project.DAL.UnitOfWork;

namespace Project.BL.Facades;

public class UserFacade :
    FacadeBase<User, UserListModel, UserDetailModel,
        UserEntityMapper>, IUserFacade
{
    private readonly IUserModelMapper _UserModelMapper;

    public UserFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserModelMapper UserModelMapper)
        : base(unitOfWorkFactory, UserModelMapper) =>
        _UserModelMapper = UserModelMapper;

    public async Task<UserDetailModel> SaveAsync(UserDetailModel model, Guid Id)
    {
        User entity = _UserModelMapper.MapToEntity(model);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<User> repository =
            uow.GetRepository<User, UserEntityMapper>();

        await repository.InsertAsync(entity);
        await uow.CommitAsync();

        return ModelMapper.MapToDetailModel(entity);
    }
}