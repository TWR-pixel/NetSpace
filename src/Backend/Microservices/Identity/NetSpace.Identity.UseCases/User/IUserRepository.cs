using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.UseCases.User;

public interface IUserRepository : IRepository<UserEntity, string>
{
    public Task<UserEntity?> FindByEmailAsync(string email);
}
