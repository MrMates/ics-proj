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

    [Fact]
    public async Task GetAll_Filter_FromPastWeek()
    {
        // None of the seeded activities have start date from past month
        var fromSeeds = await _facadeSUT.GetPastWeek();
        Assert.Empty(fromSeeds);

        // We add a new activity that took place today
        var newEntity = ActivitySeeds.NotSeededPastWeek;

        // We add a new activity that took place on the first day of previous month
        var oldEntity = ActivitySeeds.NotSeededPreviousMonth;

        // GetPastWeek returns list model, so we'll need both
        var listModel = ActivityModelMapper.MapToListModel(newEntity);
        var detailModel = ActivityModelMapper.MapToDetailModel(newEntity);

        var oldDetailModel = ActivityModelMapper.MapToDetailModel(oldEntity);

        await _facadeSUT.SaveAsync(detailModel);
        await _facadeSUT.SaveAsync(oldDetailModel);

        var fromAdded = await _facadeSUT.GetPastWeek();

        // Output of GetPastWeek is not empty
        Assert.NotEmpty(fromAdded);

        // Output has exactly one activity
        Assert.True(fromAdded.Count() == 1);

        var returnedModel = fromAdded.First();
        FixIds(listModel, returnedModel);

        // It should be the new one
        DeepAssert.Equal(listModel, returnedModel);
    }

    [Fact]
    public async Task GetAll_Filter_FromPastMonth()
    {
        // None of the seeded activities have start date from past month
        var fromSeeds = await _facadeSUT.GetPastMonth();
        Assert.Empty(fromSeeds);

        // We add a new activity that took place on the first day of this month
        var newEntity = ActivitySeeds.NotSeededPastMonth;

        // We add a new activity that took place on the first day of previous month
        var oldEntity = ActivitySeeds.NotSeededPreviousMonth;

        // GetPastWeek returns list model, so we'll need both
        var listModel = ActivityModelMapper.MapToListModel(newEntity);
        var detailModel = ActivityModelMapper.MapToDetailModel(newEntity);

        var oldDetailModel = ActivityModelMapper.MapToDetailModel(oldEntity);

        await _facadeSUT.SaveAsync(detailModel);
        await _facadeSUT.SaveAsync(oldDetailModel);

        var fromAdded = await _facadeSUT.GetPastMonth();

        // Output of GetPastWeek is not empty
        Assert.NotEmpty(fromAdded);

        // Output has exactly one activity
        Assert.True(fromAdded.Count() == 1);

        var returnedModel = fromAdded.First();
        FixIds(listModel, returnedModel);

        // It should be the new one
        DeepAssert.Equal(listModel, returnedModel);
    }

    [Fact]
    public async Task GetAll_Filter_FromPreviousMonth()
    {
        // None of the seeded activities have start date from previous month
        var fromSeeds = await _facadeSUT.GetPreviousMonth();
        Assert.Empty(fromSeeds);

        // We add a new activity that took place on the first day of previous month
        var newEntity = ActivitySeeds.NotSeededPreviousMonth;

        // We add a new activity that took place on the first day of previous year
        var oldEntity = ActivitySeeds.NotSeededPreviousYear;

        // GetPastWeek returns list model, so we'll need both
        var listModel = ActivityModelMapper.MapToListModel(newEntity);
        var detailModel = ActivityModelMapper.MapToDetailModel(newEntity);

        var oldDetailModel = ActivityModelMapper.MapToDetailModel(oldEntity);

        await _facadeSUT.SaveAsync(detailModel);
        await _facadeSUT.SaveAsync(oldDetailModel);

        var fromAdded = await _facadeSUT.GetPreviousMonth();

        // Output of GetPastWeek is not empty
        Assert.NotEmpty(fromAdded);

        // Output has exactly one activity
        Assert.True(fromAdded.Count() == 1);

        var returnedModel = fromAdded.First();
        FixIds(listModel, returnedModel);

        // It should be the new one
        DeepAssert.Equal(listModel, returnedModel);
    }

    [Fact]
    public async Task GetAll_Filter_FromPastYear()
    {
        // None of the seeded activities have start date from past year
        var fromSeeds = await _facadeSUT.GetPastYear();
        Assert.Empty(fromSeeds);

        // We add a new activity that took place on the first day of past year
        var newEntity = ActivitySeeds.NotSeededPastYear;

        // We add a new activity that took place on the first day of previous year
        var oldEntity = ActivitySeeds.NotSeededPreviousYear;

        // GetPastWeek returns list model, so we'll need both
        var listModel = ActivityModelMapper.MapToListModel(newEntity);
        var detailModel = ActivityModelMapper.MapToDetailModel(newEntity);

        var oldDetailModel = ActivityModelMapper.MapToDetailModel(oldEntity);

        await _facadeSUT.SaveAsync(detailModel);
        await _facadeSUT.SaveAsync(oldDetailModel);

        var fromAdded = await _facadeSUT.GetPastYear();

        // Output of GetPastWeek is not empty
        Assert.NotEmpty(fromAdded);

        // Output has exactly one activity
        Assert.True(fromAdded.Count() == 1);

        var returnedModel = fromAdded.First();
        FixIds(listModel, returnedModel);

        // It should be the new one
        DeepAssert.Equal(listModel, returnedModel);
    }

    private static void FixIds(ActivityListModel expectedModel, ActivityListModel returnedModel)
    {
        returnedModel.Id = expectedModel.Id;
    }
}
