using NetSpace.Community.Domain.User;

namespace NetSpace.Community.UseCases.User;

public interface IUserReadonlyRepository : IReadonlyRepository<UserEntity, Guid>
{
    
}
