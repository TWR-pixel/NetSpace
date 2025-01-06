using NetSpace.Community.Domain.Community;

namespace NetSpace.Community.UseCases.Community;

public interface ICommunityRepository : IRepository<CommunityEntity, int>, ICommunityReadonlyRepository
{
}
