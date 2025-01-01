using NetSpace.User.Domain.User;

namespace NetSpace.User.UseCases.UserPostUserComment;

public interface IUserPostUserCommentRepository : IRepository<UserPostUserCommentEntity, int>
{
    public IQueryable<UserPostUserCommentEntity> Filter(UserPostUserCommentFilterOptions filter);
}
