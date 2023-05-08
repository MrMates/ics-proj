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
            Type = Project.Common.Enums.ActivityType.Work,
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
    public async Task Get_UserActivities_FromSeed_Works()
    {
        var activities = await _facadeSUT.GetUserActivities(UserSeeds.DefaultUser.Id);

        Assert.Equal(2, activities.Count());
    }

    [Fact]
    public async Task Get_ActivityTimeSpent_Works()
    {
        var timeSpent = await _facadeSUT.ActivityTimeSpent(ActivitySeeds.WorkActivity.Id);

        Assert.Equal(ActivitySeeds.WorkActivity.TimeEnd - ActivitySeeds.WorkActivity.TimeBegin, timeSpent);
    }

    [Fact]
    public async Task GetAll_Filter_FromPastWeek()
    {
        // None of the seeded activities have start date from past month
        var fromSeeds = await _facadeSUT.GetPastWeek(UserSeeds.DefaultUser.Id);
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

        var fromAdded = await _facadeSUT.GetPastWeek(UserSeeds.DefaultUser.Id);

        // Output of GetPastWeek is not empty
        Assert.NotEmpty(fromAdded);

        // Output has exactly one activity
        Assert.True(fromAdded.Count() == 1);

        var returnedModel = fromAdded.First();
        FixIds(listModel, returnedModel);

        // It should be the new one
        Assert.Equal(listModel.Id, returnedModel.Id);
    }

    [Fact]
    public async Task GetAll_Filter_FromPastMonth()
    {
        // None of the seeded activities have start date from past month
        var fromSeeds = await _facadeSUT.GetPastMonth(UserSeeds.DefaultUser.Id);
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

        var fromAdded = await _facadeSUT.GetPastMonth(UserSeeds.DefaultUser.Id);

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
        var fromSeeds = await _facadeSUT.GetPreviousMonth(UserSeeds.DefaultUser.Id);
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

        var fromAdded = await _facadeSUT.GetPreviousMonth(UserSeeds.DefaultUser.Id);

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
        var fromSeeds = await _facadeSUT.GetPastYear(UserSeeds.DefaultUser.Id);
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

        var fromAdded = await _facadeSUT.GetPastYear(UserSeeds.DefaultUser.Id);

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
    public async Task GetAll_CustomFilter_BeginningBefore()
    {
        // There will already be some seeded activities beginning before the day a month ago
        var fromSeeds = await _facadeSUT.GetBeginningBefore(DateTime.Today.AddMonths(-1), UserSeeds.DefaultUser.Id);
        var seededCount = fromSeeds.Count();

        // We add a new activity from past week - the output shouldn't change
        var newModel = ActivityModelMapper.MapToDetailModel(ActivitySeeds.NotSeededPastWeek);
        await _facadeSUT.SaveAsync(newModel);

        var afterNewAdd = await _facadeSUT.GetBeginningBefore(DateTime.Today.AddMonths(-1), UserSeeds.DefaultUser.Id);
        var newCount = afterNewAdd.Count();
        Assert.Equal(seededCount, newCount);

        // We add an activity from previous year - it should show up in the output
        var oldModel = ActivityModelMapper.MapToDetailModel(ActivitySeeds.NotSeededPreviousYear);
        await _facadeSUT.SaveAsync(oldModel);

        var afterOldAdd = await _facadeSUT.GetBeginningBefore(DateTime.Today.AddMonths(-1), UserSeeds.DefaultUser.Id);
        var oldCount = afterOldAdd.Count();
        Assert.NotEqual(seededCount, oldCount);
    }

    [Fact]
    public async Task GetAll_CustomFilter_BeginningAfter()
    {
        // There are no activities beginning after the day a month ago
        var fromSeeds = await _facadeSUT.GetBeginningAfter(DateTime.Today.AddMonths(-1), UserSeeds.DefaultUser.Id);
        Assert.Empty(fromSeeds);

        // We add a new activity from previous year - the output shouldn't change
        var oldModel = ActivityModelMapper.MapToDetailModel(ActivitySeeds.NotSeededPreviousYear);
        await _facadeSUT.SaveAsync(oldModel);

        var afterOldAdd = await _facadeSUT.GetBeginningAfter(DateTime.Today.AddMonths(-1), UserSeeds.DefaultUser.Id);
        Assert.Empty(afterOldAdd);

        // We add an activity from past week - it should show up in the output
        var newModel = ActivityModelMapper.MapToDetailModel(ActivitySeeds.NotSeededPastWeek);
        await _facadeSUT.SaveAsync(newModel);

        var afterNewAdd = await _facadeSUT.GetBeginningAfter(DateTime.Today.AddMonths(-1), UserSeeds.DefaultUser.Id);
        Assert.NotEmpty(afterNewAdd);
    }

    private static void FixIds(ActivityListModel expectedModel, ActivityListModel returnedModel)
    {
        returnedModel.Id = expectedModel.Id;
    }
}
