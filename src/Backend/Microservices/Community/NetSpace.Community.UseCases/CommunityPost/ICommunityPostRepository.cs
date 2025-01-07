using NetSpace.Community.Domain.CommunityPost;

namespace NetSpace.Community.UseCases.CommunityPost;

public interface ICommunityPostRepository : IRepository<CommunityPostEntity, int>, ICommunityPostReadonlyRepository
{
    public Task<CommunityPostEntity?> GetByIdWithDetails(int id, CancellationToken cancellationToken = default);
}
