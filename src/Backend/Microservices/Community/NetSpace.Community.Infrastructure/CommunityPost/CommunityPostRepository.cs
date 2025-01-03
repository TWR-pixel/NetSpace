using NetSpace.Community.Domain.CommunityPost;
using NetSpace.Community.UseCases.CommunityPost;

namespace NetSpace.Community.Infrastructure.CommunityPost;

public sealed class CommunityPostRepository(NetSpaceDbContext dbContext) : RepositoryBase<CommunityPostEntity, int>(dbContext), ICommunityPostRepository
{
}
