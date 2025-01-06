using NetSpace.Community.Domain.CommunityPostUserComment;

namespace NetSpace.Community.UseCases.CommunityPostUserComment;

public interface ICommunityPostUserCommentRepository : IRepository<CommunityPostUserCommentEntity, int>, ICommunityPostUserCommentReadonlyRepository
{

}
