using Mapster;
using NetSpace.Community.Application.Community;
using NetSpace.Community.Application.CommunityPost.Commands;
using NetSpace.Community.Domain.CommunityPost;

namespace NetSpace.Community.Application.CommunityPost;

public sealed class RegisterCommunityPostMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCommunityPostCommand, CommunityPostEntity>();
        config.NewConfig<CommunityPostEntity, CommunityPostResponse>();
        config.NewConfig<UpdateCommunityPostCommand, CommunityPostEntity>()
            .Ignore(c => c.Id);

        config.NewConfig<PartiallyUpdateCommunityPostCommand, CommunityPostEntity>()
            .Ignore(c => c.Id)
            .IgnoreNullValues(true);

        config.NewConfig<IEnumerable<CommunityPostEntity>, IEnumerable<CommunityResponse>>();
    }
}
