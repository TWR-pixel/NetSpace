using Mapster;
using NetSpace.Community.Application.CommunityPostUserComment.Commands;
using NetSpace.Community.Application.CommunitySubscription;
using NetSpace.Community.Domain.CommunityPostUserComment;
using NetSpace.Community.Domain.CommunitySubscription;

namespace NetSpace.Community.Application.CommunityPostUserComment;

public sealed class RegisterCommunityPostUserCommentMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCommunityPostUserCommentCommand, CommunityPostUserCommentEntity>();
        config.NewConfig<CommunityPostUserCommentEntity, CommunityPostUserCommentResponse>();
        config.NewConfig<UpdateCommunityPostUserCommentCommand, CommunityPostUserCommentEntity>()
            .Ignore(c => c.Id);

        config.NewConfig<PartiallyUpdateCommunityPostUserCommentCommand, CommunityPostUserCommentEntity>()
            .Ignore(c => c.Id)
            .IgnoreNullValues(true);

        config.NewConfig<IEnumerable<CommunityPostUserCommentEntity>, IEnumerable<CommunityPostUserCommentResponse>>();

        config.NewConfig<CommunitySubscriptionEntity, CommunitySubscriptionResponse>();
        config.NewConfig<IEnumerable<CommunitySubscriptionEntity>, IEnumerable<CommunitySubscriptionResponse>>();
    }
}
