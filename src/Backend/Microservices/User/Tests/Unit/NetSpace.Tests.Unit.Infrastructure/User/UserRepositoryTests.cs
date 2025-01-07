using NetSpace.Tests.Unit.Initializer;

namespace NetSpace.Tests.Unit.Infrastructure.User;

public class UserRepositoryTests
{
    [Fact]
    public async Task SaveChangesAsync_ShouldReturn3()
    {
        //Arrange
        var uof = await TestInitializer.CreateUnitOfWorkAsync();
        var entities = TestInitializer.Create3Users();
        await uof.Users.AddRangeAsync(entities);

        //Act
        var result = await uof.SaveChangesAsync(CancellationToken.None);

        //Assert
        Assert.NotEqual(0, result);
        Assert.Equal(3, result);
    }

    [Fact]
    public async Task SaveChanges_ShouldNotThrowException()
    {
        //Arrange
        var repo = await TestInitializer.CreateUnitOfWorkAsync();

        //Act, Assert
        AssertHelper.DoesntThrow(async () => await repo.SaveChangesAsync());
    }

    [Fact]
    public async Task CountAsync_ShouldReturn3Users()
    {
        var uof = await TestInitializer.CreateUnitOfWorkAsync();
        await uof.Users.AddRangeAsync(TestInitializer.Create3Users());
        await uof.SaveChangesAsync();

        var result = await uof.Users.CountAsync();

        Assert.NotEqual(0, result);
        Assert.Equal(3, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveFirstUser()
    {
        //Arrange
        var uof = await TestInitializer.CreateUnitOfWorkAsync();
        var entities = TestInitializer.Create3Users();
        await uof.Users.AddRangeAsync(entities);
        await uof.SaveChangesAsync();

        //Act
        await uof.Users.DeleteAsync(entities.First());
        await uof.SaveChangesAsync();

        var countAfterDeleting = await uof.Users.CountAsync();

        //Assert
        Assert.Equal(2, countAfterDeleting);
        Assert.NotEqual(3, countAfterDeleting);
    }

    [Fact]
    public async Task DeleteRangeAsync_ShouldRemoveAllUsers()
    {
        //Arrange
        var repo = await TestInitializer.CreateUnitOfWorkAsync();
        var entities = TestInitializer.Create3Users();
        await repo.Users.AddRangeAsync(entities);
        await repo.SaveChangesAsync();

        //Act
        await repo.Users.DeleteRangeAsync(entities);
        await repo.SaveChangesAsync();

        var countAfterDeleting = await repo.Users.CountAsync();

        //Assert
        Assert.Equal(0, countAfterDeleting);
        Assert.NotEqual(3, countAfterDeleting);
    }

    [Fact]
    public async Task FindByIdAsync_ShouldReturnUserWithExistingId()
    {
        //Arrange
        var repo = await TestInitializer.CreateUnitOfWorkAsync();
        var entities = TestInitializer.Create3Users();
        await repo.Users.AddRangeAsync(entities);
        await repo.SaveChangesAsync();
        var firstEntityId = entities.First().Id;

        //Act
        var result = await repo.Users.FindByIdAsync(entities.First().Id);

        Assert.NotNull(result);
        Assert.Equal(firstEntityId, result.Id);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllAddedUsers()
    {
        //Arrange
        var repo = await TestInitializer.CreateUnitOfWorkAsync();
        var entities = TestInitializer.Create3Users();
        await repo.Users.AddRangeAsync(entities);
        await repo.SaveChangesAsync();

        //Act
        var result = await repo.Users.GetAllAsync();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(entities.Count, result.Count());
    }

    [Fact]
    public async Task UpdateAsync_ShouldSetNewUserName()
    {
        //Arrange
        var repo = await TestInitializer.CreateUnitOfWorkAsync();
        var entities = TestInitializer.Create3Users();
        await repo.Users.AddRangeAsync(entities);
        await repo.SaveChangesAsync();
        var userForUpdate = await repo.Users.FindByIdAsync(entities.First().Id);
        userForUpdate!.Name = "newName";

        //Act
        await repo.Users.UpdateAsync(userForUpdate);
        await repo.SaveChangesAsync();

        var updateUserFromDb = await repo.Users.FindByIdAsync(userForUpdate.Id);

        Assert.Equal(updateUserFromDb!.Name, userForUpdate.Name);
    }

    [Fact]
    public async Task UpdateRangeAsync_ShouldSetNewUserNames()
    {
        //Arrange
        var repo = await TestInitializer.CreateUnitOfWorkAsync();
        var entities = TestInitializer.Create3Users();
        await repo.Users.AddRangeAsync(entities);
        await repo.SaveChangesAsync();
        var userForUpdate1 = await repo.Users.FindByIdAsync(entities.First().Id);
        var userForUpdate2 = await repo.Users.FindByIdAsync(entities.Skip(1).First().Id);
        userForUpdate1!.Name = "newName1";
        userForUpdate2!.Name = "newName2";

        //Act
        await repo.Users.UpdateRangeAsync([userForUpdate1, userForUpdate2]);
        await repo.SaveChangesAsync();

        var updatedUserFromDb1 = await repo.Users.FindByIdAsync(userForUpdate1.Id);
        var updatedUserFromDb2 = await repo.Users.FindByIdAsync(userForUpdate2.Id);

        Assert.Equal(updatedUserFromDb1!.Name, userForUpdate1.Name);
        Assert.Equal(updatedUserFromDb2!.Name, userForUpdate2.Name);
    }
}
