using NetSpace.Tests.Unit.Initializer;

namespace NetSpace.Tests.Unit.Infrastructure;

public class UserRepositoryTests
{
    [Fact]
    public async Task SaveChangesAsync_ShouldReturn3()
    {
        //Arrange
        var repo = TestInitializer.CreateInMemoryUserRepo();
        var entities = TestInitializer.Create3Users();
        await repo.AddRangeAsync(entities);

        //Act
        var result = await repo.SaveChangesAsync(CancellationToken.None);

        //Assert
        Assert.NotEqual(0, result);
        Assert.Equal(3, result);
    }

    [Fact]
    public async Task SaveChanges_ShouldNotThrowException()
    {
        //Arrange
        var repo = await TestInitializer.AddRangeAndSaveChangesToRepo3Users();

        //Act, Assert
        AssertHelper.DoesntThrow(async () => await repo.SaveChangesAsync());
    }

    [Fact]
    public async Task CountAsync_ShouldReturn3Users()
    {
        var repo = await TestInitializer.AddRangeAndSaveChangesToRepo3Users();

        var result = await repo.CountAsync();

        Assert.NotEqual(0, result);
        Assert.Equal(3, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveFirstUser()
    {
        //Arrange
        var repo = TestInitializer.CreateInMemoryUserRepo();
        var entities = TestInitializer.Create3Users();
        await repo.AddRangeAsync(entities);
        await repo.SaveChangesAsync();

        //Act
        await repo.DeleteAsync(entities.First());
        await repo.SaveChangesAsync();

        var countAfterDeleting = await repo.CountAsync();

        //Assert
        Assert.Equal(2, countAfterDeleting);
        Assert.NotEqual(3, countAfterDeleting);
    }

    [Fact]
    public async Task DeleteRangeAsync_ShouldRemoveAllUsers()
    {
        //Arrange
        var repo = TestInitializer.CreateInMemoryUserRepo();
        var entities = TestInitializer.Create3Users();
        await repo.AddRangeAsync(entities);
        await repo.SaveChangesAsync();

        //Act
        await repo.DeleteRangeAsync(entities);
        await repo.SaveChangesAsync();

        var countAfterDeleting = await repo.CountAsync();

        //Assert
        Assert.Equal(0, countAfterDeleting);
        Assert.NotEqual(3, countAfterDeleting);
    }

    [Fact]
    public async Task FindByIdAsync_ShouldReturnUserWithExistingId()
    {
        //Arrange
        var repo = TestInitializer.CreateInMemoryUserRepo();
        var entities = TestInitializer.Create3Users();
        await repo.AddRangeAsync(entities);
        await repo.SaveChangesAsync();
        var firstEntityId = entities.First().Id;

        //Act
        var result = await repo.FindByIdAsync(entities.First().Id);

        Assert.NotNull(result);
        Assert.Equal(firstEntityId, result.Id);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllAddedUsers()
    {
        //Arrange
        var repo = TestInitializer.CreateInMemoryUserRepo();
        var entities = TestInitializer.Create3Users();
        await repo.AddRangeAsync(entities);
        await repo.SaveChangesAsync();

        //Act
        var result = await repo.GetAllAsync();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(entities.Count, result.Count());
    }

    [Fact]
    public async Task UpdateAsync_ShouldSetNewUserName()
    {
        //Arrange
        var repo = TestInitializer.CreateInMemoryUserRepo();
        var entities = TestInitializer.Create3Users();
        await repo.AddRangeAsync(entities);
        await repo.SaveChangesAsync();
        var userForUpdate = await repo.FindByIdAsync(entities.First().Id);
        userForUpdate!.Name = "newName";

        //Act
        await repo.UpdateAsync(userForUpdate);
        await repo.SaveChangesAsync();

        var updateUserFromDb = await repo.FindByIdAsync(userForUpdate.Id);

        Assert.Equal(updateUserFromDb!.Name, userForUpdate.Name);
    }

    [Fact]
    public async Task UpdateRangeAsync_ShouldSetNewUserNames()
    {
        //Arrange
        var repo = TestInitializer.CreateInMemoryUserRepo();
        var entities = TestInitializer.Create3Users();
        await repo.AddRangeAsync(entities);
        await repo.SaveChangesAsync();
        var userForUpdate1 = await repo.FindByIdAsync(entities.First().Id);
        var userForUpdate2 = await repo.FindByIdAsync(entities.Skip(1).First().Id);
        userForUpdate1!.Name = "newName1";
        userForUpdate2!.Name = "newName2";

        //Act
        await repo.UpdateRangeAsync([userForUpdate1, userForUpdate2]);
        await repo.SaveChangesAsync();

        var updatedUserFromDb1 = await repo.FindByIdAsync(userForUpdate1.Id);
        var updatedUserFromDb2 = await repo.FindByIdAsync(userForUpdate2.Id);

        Assert.Equal(updatedUserFromDb1!.Name, userForUpdate1.Name);
        Assert.Equal(updatedUserFromDb2!.Name, userForUpdate2.Name);
    }
}
