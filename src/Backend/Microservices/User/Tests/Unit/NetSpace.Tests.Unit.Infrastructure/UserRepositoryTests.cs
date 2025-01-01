using NetSpace.Tests.Unit.Initializer;
using NetSpace.User.Domain.User;

namespace NetSpace.Tests.Unit.Infrastructure;

public class UserRepositoryTests
{
    [Fact]
    public async Task SaveChangesAsync_ShouldReturn3()
    {
        //Arrange
        var repo = TestInitializer.CreateUserRepo();
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
        var repo = TestInitializer.CreateUserRepo();

        //Act, Assert
        AssertHelper.DoesntThrow(async () => await repo.SaveChangesAsync());
    }


}
