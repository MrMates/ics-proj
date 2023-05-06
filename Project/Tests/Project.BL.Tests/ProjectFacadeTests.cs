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
using System.Collections.ObjectModel;
using Project.DAL;

namespace Project.BL.Tests;

public sealed class ProjectFacadeTests : FacadeTestsBase
{
    private readonly IProjectFacade _facadeSUT;
    private readonly IProjectCreationReportFacade _detailedFacadeSUT;

    public ProjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _facadeSUT = new ProjectFacade(UnitOfWorkFactory, ProjectModelMapper);
        _detailedFacadeSUT = new ProjectCreationReportFacade(UnitOfWorkFactory, ProjectCreationReportModelMapper);
    }

    [Fact]
    public async Task Create_Empty_EqualsCreated()
    {
        var model = new ProjectDetailModel()
        {
            Activities = new ObservableCollection<ActivityListModel>(),
            Users = new ObservableCollection<UserListModel>(),
            ProjectName = "Nový projekt"
        };

        var returnedModel = await _facadeSUT.SaveAsync(model);

        FixIds(model, returnedModel);
        DeepAssert.Equal(model, returnedModel);
    }

    [Fact]
    public async Task Add_Activity_Added()
    {
        await _facadeSUT.AddActivityToProject(ActivitySeeds.WorkActivity.Id, ProjectSeeds.AgencyProject.Id);


        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var project = await dbxAssert.Projects
            .Include(i => i.Activities)
            .SingleAsync(i => i.Id == ProjectSeeds.AgencyProject.Id);
        Assert.NotEmpty(project!.Activities);
    }

    [Fact]
    public async Task Add_Users_Added()
    {
        await _facadeSUT.AddUserToProject(UserSeeds.DefaultUser.Id, ProjectSeeds.AgencyProject.Id);


        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var project = await dbxAssert.Projects
            .Include(i => i.UserProjects)
            .SingleAsync(i => i.Id == ProjectSeeds.AgencyProject.Id);
        var user = await dbxAssert.Users
            .Include(i => i.UserProjects)
            .SingleAsync(i => i.Id == UserSeeds.DefaultUser.Id);

        Assert.NotEmpty(project!.UserProjects);
        Assert.NotEmpty(user!.UserProjects);
        Assert.True(dbxAssert.UserProjects.Any());
    }

    [Fact]
    public async Task Get_UsersInProject_Works()
    {
        var emptyUsers = await _facadeSUT.GetUsersInProject(ProjectSeeds.AgencyProject.Id);

        await _facadeSUT.AddUserToProject(UserSeeds.DefaultUser.Id, ProjectSeeds.AgencyProject.Id);

        var users = await _facadeSUT.GetUsersInProject(ProjectSeeds.AgencyProject.Id);

        Assert.Empty(emptyUsers);
        Assert.NotEmpty(users);
    }

    [Fact]
    public async Task Get_ActivitiesInProject_Works()
    {
        var seededActivities = await _facadeSUT.GetActivitiesInProject(ProjectSeeds.AgencyProject.Id);

        await _facadeSUT.AddActivityToProject(ActivitySeeds.ActivityToDelete.Id, ProjectSeeds.AgencyProject.Id);

        var updatedActivities = await _facadeSUT.GetActivitiesInProject(ProjectSeeds.AgencyProject.Id);

        Assert.Single(seededActivities);
        Assert.Equal(2, updatedActivities.Count());
    }

    [Fact]
    public async Task Create_CreationModel_DoesNotThrow()
    {
        var model = new ProjectCreationDetailModel()
        {
            ProjectName = "Nový projekt"
        };

        var _ = await _detailedFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task Create_WithSeeded_Throws()
    {
        var model = new ProjectDetailModel()
        {
            ProjectName = ProjectSeeds.AgencyProject.Name,
            Activities = new ObservableCollection<ActivityListModel>()
            {
                new()
                {
                    ActivityName = ActivitySeeds.WorkActivity.Name,
                    Id = ActivitySeeds.WorkActivity.Id
                }
            },
            Users = new ObservableCollection<UserListModel>()
            {
                new()
                {
                    UserFirstName = UserSeeds.DefaultUser.FirstName,
                    UserLastName = UserSeeds.DefaultUser.LastName
                }
            }
        };

        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _facadeSUT.SaveAsync(model));
    }

    private static void FixIds(ProjectDetailModel expectedModel, ProjectDetailModel returnedModel)
    {
        returnedModel.Id = expectedModel.Id;
    }
}
