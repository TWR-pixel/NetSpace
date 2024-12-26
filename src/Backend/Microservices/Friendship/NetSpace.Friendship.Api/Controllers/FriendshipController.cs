using Microsoft.AspNetCore.Mvc;
using NetSpace.Friendship.Domain;
using NetSpace.Friendship.UseCases;

namespace NetSpace.Friendship.Api.Controllers;

[ApiController]
[Route("/api/friendships/")]
public sealed class FriendshipController(IUserRepository users) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<UserEntity>> GetAllByStatus(Guid id)
    {
        var result = await users.GetAllFriendsByStatus(id, FriendshipStatus.Accepted);

        return result;
    }

    [HttpPost]
    public async Task<UserEntity> CreateUser()
    {
        var user = new UserEntity(Guid.NewGuid(), "iowejf", "owijef", null, Gender.Male);
        await users.AddAsync(user);

        return user;
    }
}
