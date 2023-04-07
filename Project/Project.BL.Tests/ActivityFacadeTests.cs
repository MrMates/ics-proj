using Project.BL.Facades;
using Project.BL.Models;
using Project.Common.Tests;
using Project.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System.Diagnostics;

namespace Project.BL.Tests;

public sealed class ActivityFacadeTests : FacadeTestsBase
{
    private readonly IActivityFacade _facadeSUT;

    public ActivityFacadeTests(ITestOutputHelper output) : base(output)
    {
        _facadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
    }

    [Fact]
    public async Task Create_WithFKsFromSeed_DoesNotThrow()
    {
        var model = new ActivityDetailModel()
        { 
            ActivityName = "Name",
            ActivityType = "Type",
            UserId = UserSeeds.DefaultUser.Id,
            ProjectId = ProjectSeeds.AgencyProject.Id
        };
        var _ = await _facadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task GetById_FromSeed_EqualsSeeded()
    {
        var detailModel = ActivityModelMapper.MapToDetailModel(ActivitySeeds.WorkActivity);

        var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);

        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task DeleteById_FromSeed_DoesNotThrow()
    {
        await _facadeSUT.DeleteAsync(ActivitySeeds.WorkActivity.Id);
    }

    [Fact]
    public async Task Update_FromSeed_Changed_EqualsUpdated()
    {
        var model = ActivityModelMapper.MapToDetailModel(ActivitySeeds.WorkActivity);
        model.ActivityName = "Změněný";

        var returned = await _facadeSUT.SaveAsync(model);
        Assert.Equal("Změněný", returned.ActivityName);
    }

    [Fact]
    public async Task GetAll_FromSeed_EqualsSeeded()
    {
        var listModel = ActivityModelMapper.MapToListModel(ActivitySeeds.WorkActivity);

        var returnedModel = await _facadeSUT.GetAsync();

        Assert.Contains(listModel, returnedModel);

    }
}
