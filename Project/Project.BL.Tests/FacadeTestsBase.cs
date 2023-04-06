using Project.BL.Mappers;
using Project.Common.Tests;
using Project.DAL;
using Project.DAL.Mappers;
using Project.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Project.BL.Tests;

public class FacadeTestsBase : IAsyncLifetime
{
    protected FacadeTestsBase(ITestOutputHelper output)
    {
        DbContextFactory = new DbContextTestingFactory(GetType().FullName!, seedTestingData: true);

        ActivityEntityMapper = new ActivityEntityMapper();
        UserEntityMapper = new UserEntityMapper();
        ProjectEntityMapper = new ProjectEntityMapper();
        UserProjectEntityMapper = new UserProjectEntityMapper();

        ActivityModelMapper = new ActivityModelMapper();
        ProjectModelMapper = new ProjectModelMapper();
        ProjectCreationReportModelMapper = new ProjectCreationReportModelMapper();
        UserModelMapper = new UserModelMapper();

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<DatabaseContext> DbContextFactory { get; }

    protected ActivityEntityMapper ActivityEntityMapper { get; }
    protected UserEntityMapper UserEntityMapper { get; }
    protected ProjectEntityMapper ProjectEntityMapper { get; }
    protected UserProjectEntityMapper UserProjectEntityMapper { get; }


    protected ActivityModelMapper ActivityModelMapper { get; }
    protected ProjectModelMapper ProjectModelMapper { get; }
    protected ProjectCreationReportModelMapper ProjectCreationReportModelMapper { get; }
    protected UserModelMapper UserModelMapper { get; }

    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}
