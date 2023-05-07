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

    protected override string IncludesNavigationPathDetail =>
        $"{nameof(User.Activities)}";

}