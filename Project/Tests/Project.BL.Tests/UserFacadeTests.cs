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

namespace Project.BL.Tests;

public sealed class UserFacadeTests : FacadeTestsBase
{
    private readonly IUserFacade _facadeSUT;

    public UserFacadeTests(ITestOutputHelper output) : base(output)
    {
        _facadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);
    }

    [Fact]
    public async Task Create_NewUser_EqualsCreated()
    {
        var model = new UserDetailModel()
        {
            UserFirstName = "Lukáš",
            UserLastName = "Etzler",
        };

        var returnedModel = await _facadeSUT.SaveAsync(model);

        FixIds(model, returnedModel);
        DeepAssert.Equal(model, returnedModel);
    }

    [Fact]
    public async Task Update_FromSeed_Changed()
    {
        var model = UserModelMapper.MapToDetailModel(UserSeeds.DefaultUser);
        model.UserLastName = "Změněný";

        var returned = await _facadeSUT.SaveAsync(model);
        Assert.Equal("Změněný", returned.UserLastName);
    }

    [Fact]
    public async Task GetById_FromSeed_EqualsSeeded()
    {
        var detailModel = UserModelMapper.MapToDetailModel(UserSeeds.DefaultUser);

        var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);

        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task GetAll_FromSeed_EqualsSeeded()
    {
        var listModel = UserModelMapper.MapToListModel(UserSeeds.DefaultUser);

        var returnedModel = await _facadeSUT.GetAsync();

        Assert.Contains(listModel, returnedModel);
    }

    [Fact]
    public async Task DeleteById_FromSeed_DoesNotThrow()
    {
        await _facadeSUT.DeleteAsync(UserSeeds.DefaultUser.Id);
    }

    private static void FixIds(UserDetailModel expectedModel, UserDetailModel returnedModel)
    {
        returnedModel.Id = expectedModel.Id;
    }
}
