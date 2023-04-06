using Microsoft.EntityFrameworkCore;

namespace Project.DAL.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<DatabaseContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
}