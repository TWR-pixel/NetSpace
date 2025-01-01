using Microsoft.EntityFrameworkCore;
using NetSpace.User.Domain.User;
using NetSpace.User.Infrastructure;
using NetSpace.User.Infrastructure.User;

namespace NetSpace.Tests.Unit.Initializer;

public static class TestInitializer
{
    private static NetSpaceDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<NetSpaceDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var db = new NetSpaceDbContext(options);

        return db;
    }

    public static UserRepository CreateInMemoryUserRepo()
    {
        var repo = new UserRepository(CreateInMemoryDb());

        return repo;
    }

    public static List<UserEntity> Create3Users()
    {
        var users = new List<UserEntity>
        {
            new(){Nickname = "nickname1", Name = "name1", Surname = "surname1", Email = "email1@mail.ru"},
            new(){Nickname = "nickname2", Name = "name2", Surname = "surname2", Email = "email2@mail.ru"},
            new(){Nickname = "nickname3", Name = "name3", Surname = "surname3", Email = "email3@mail.ru"},
        };

        return users;
    }

    public static async Task<UserRepository> AddRangeAndSaveChangesToRepo3Users()
    {
        var repo = CreateInMemoryUserRepo();
        var testUsers = Create3Users();

        await repo.AddRangeAsync(testUsers);
        await repo.SaveChangesAsync();

        return repo;
    }
}
