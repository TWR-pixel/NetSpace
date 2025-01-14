using NetSpace.Community.Tests.Unit.Initializer;

namespace NetSpace.Community.Tests.Unit.Infrastructure.Community;

public class CommunityRepositoryTests
{
    [Fact]
    public async Task SaveChangesAsync_ShouldReturn3()
    {
        //Arrange
        var uof = await TestInitializer.CreateUnitOfWorkWithTestDataAsync();
        var testUsers = TestInitializer.Create3Users();
        var entities = TestInitializer.Create3Communities(testUsers);
        await uof.Communities.AddRangeAsync(entities);

        //Act
        var result = await uof.SaveChangesAsync();

        //Assert
        Assert.NotEqual(0, result);
        Assert.Equal(6, result);
    }

    [Fact]
    public async Task SaveChanges_ShouldNotThrowException()
    {
        //Arrange
        var repo = await TestInitializer.CreateUnitOfWorkWithTestDataAsync();

        //Act, Assert
        AssertHelper.DoesntThrow(async () => await repo.SaveChangesAsync());
    }

    [Fact]
    public async Task CountAsync_ShouldReturn6Communities()
    {
        var uof = await TestInitializer.CreateUnitOfWorkWithTestDataAsync();
        var testUsers = TestInitializer.Create3Users();
        var entities = TestInitializer.Create3Communities(testUsers);
        await uof.Communities.AddRangeAsync(entities);
        await uof.SaveChangesAsync();

        var result = await uof.Communities.CountAsync();

        Assert.NotEqual(0, result);
        Assert.Equal(6, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveFirstCommunity()
    {
        //Arrange
        var uof = await TestInitializer.CreateUnitOfWorkWithTestDataAsync();
        var testUsers = TestInitializer.Create3Users();
        var entities = TestInitializer.Create3Communities(testUsers);
        await uof.Communities.AddRangeAsync(entities);
        await uof.SaveChangesAsync();

        //Act
        await uof.Communities.DeleteAsync(entities.First());
        await uof.SaveChangesAsync();

        var countAfterDeleting = await uof.Communities.CountAsync();

        //Assert
        Assert.Equal(5, countAfterDeleting);
        Assert.NotEqual(6, countAfterDeleting);
    }

    [Fact]
    public async Task DeleteRangeAsync_ShouldRemoveAllCommunities()
    {
        //Arrange
        var repo = await TestInitializer.CreateUnitOfWorkWithTestDataAsync();
        var testUsers = TestInitializer.Create3Users();
        var entities = TestInitializer.Create3Communities(testUsers);
        await repo.Communities.AddRangeAsync(entities);
        await repo.SaveChangesAsync();

        //Act
        await repo.Communities.DeleteRangeAsync(entities);
        await repo.SaveChangesAsync();

        var countAfterDeleting = await repo.Communities.CountAsync();

        //Assert
        Assert.Equal(3, countAfterDeleting);
        Assert.NotEqual(6, countAfterDeleting);
    }

    [Fact]
    public async Task FindByIdAsync_ShouldReturnCommunityWithExistingId()
    {
        //Arrange
        var repo = await TestInitializer.CreateUnitOfWorkWithTestDataAsync();
        var testUsers = TestInitializer.Create3Users();
        var entities = TestInitializer.Create3Communities(testUsers);
        await repo.Communities.AddRangeAsync(entities);
        await repo.SaveChangesAsync();
        var firstEntityId = entities.First().Id;

        //Act
        var result = await repo.Communities.FindByIdAsync(entities.First().Id);

        Assert.NotNull(result);
        Assert.Equal(firstEntityId, result.Id);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllAddedCommunities()
    {
        //Arrange
        var repo = await TestInitializer.CreateUnitOfWorkWithTestDataAsync();
        var testUsers = TestInitializer.Create3Users();
        var entities = TestInitializer.Create3Communities(testUsers);
        await repo.Communities.AddRangeAsync(entities);
        await repo.SaveChangesAsync();

        //Act
        var result = await repo.Communities.GetAllAsync();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(6, result.Count());
    }

    [Fact]
    public async Task UpdateAsync_ShouldSetNewCommunityName()
    {
        //Arrange
        var repo = await TestInitializer.CreateUnitOfWorkWithTestDataAsync();
        var testUsers = TestInitializer.Create3Users();
        var entities = TestInitializer.Create3Communities(testUsers);
        await repo.Communities.AddRangeAsync(entities);
        await repo.SaveChangesAsync();
        var userForUpdate = await repo.Communities.FindByIdAsync(entities.First().Id);
        userForUpdate!.Name = "newName";

        //Act
        await repo.Communities.UpdateAsync(userForUpdate);
        await repo.SaveChangesAsync();

        var updateUserFromDb = await repo.Communities.FindByIdAsync(userForUpdate.Id);

        Assert.Equal(updateUserFromDb!.Name, userForUpdate.Name);
    }

    [Fact]
    public async Task UpdateRangeAsync_ShouldSetNewCommunityNames()
    {
        //Arrange
        var repo = await TestInitializer.CreateUnitOfWorkWithTestDataAsync();
        var testUsers = TestInitializer.Create3Users();
        var entities = TestInitializer.Create3Communities(testUsers);
        await repo.Communities.AddRangeAsync(entities);
        await repo.SaveChangesAsync();
        var userForUpdate1 = await repo.Communities.FindByIdAsync(entities.First().Id);
        var userForUpdate2 = await repo.Communities.FindByIdAsync(entities.Skip(1).First().Id);
        userForUpdate1!.Name = "newName1";
        userForUpdate2!.Name = "newName2";

        //Act
        await repo.Communities.UpdateRangeAsync([userForUpdate1, userForUpdate2]);
        await repo.SaveChangesAsync();

        var updatedUserFromDb1 = await repo.Communities.FindByIdAsync(userForUpdate1.Id);
        var updatedUserFromDb2 = await repo.Communities.FindByIdAsync(userForUpdate2.Id);

        Assert.Equal(updatedUserFromDb1!.Name, userForUpdate1.Name);
        Assert.Equal(updatedUserFromDb2!.Name, userForUpdate2.Name);
    }
}
