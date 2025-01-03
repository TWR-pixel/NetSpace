using NetSpace.Community.Domain.CommunityPostUserComment;
using NetSpace.Community.UseCases.CommunityPostUserComment;

namespace NetSpace.Community.Infrastructure.CommunityPostUserComment;

public sealed class CommunityPostUserCommentRepository(NetSpaceDbContext dbContext) : RepositoryBase<CommunityPostUserCommentEntity, int>(dbContext), ICommunityPostUserCommentRepository
{
}
