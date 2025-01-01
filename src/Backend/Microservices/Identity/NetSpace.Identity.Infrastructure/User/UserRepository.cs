using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetSpace.Identity.Domain.User;
using NetSpace.Identity.UseCases.User;

namespace NetSpace.Identity.Infrastructure.User;

public sealed class UserRepository(NetSpaceDbContext dbContext, UserManager<UserEntity> userManager) : IUserRepository
{
    public async Task<IdentityResult> AddAsync(UserEntity entity, string password, CancellationToken cancellationToken = default)
    {
        var result = await userManager.CreateAsync(entity, password);

        return result;
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await userManager.Users.AnyAsync(cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await userManager.Users.CountAsync(cancellationToken);
    }

    public async Task DeleteAsync(UserEntity entity, CancellationToken cancellationToken = default)
    {
        await userManager.DeleteAsync(entity);
    }

    public async Task<UserEntity?> FindByEmailAsync(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<UserEntity?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await userManager.FindByIdAsync(id);
    }

    public async Task<IEnumerable<UserEntity>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await userManager.Users.ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(UserEntity entity, CancellationToken cancellationToken = default)
    {
        await userManager.UpdateAsync(entity);
    }
}
