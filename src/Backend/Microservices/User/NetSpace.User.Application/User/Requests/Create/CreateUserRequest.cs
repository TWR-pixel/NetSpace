using MassTransit;
using NetSpace.User.Application.Common.Cache;
using NetSpace.User.Application.User.Extensions;
using NetSpace.User.Domain.User;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Application.User.Requests.Create;

public sealed record CreateUserRequest : RequestBase<UserResponse>
{
    public required string Nickname { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }

    public string Hometown { get; set; } = string.Empty;
    public Language Language { get; set; } = Language.NotSet;
    public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.NotSet;
    public string CurrentCity { get; set; } = string.Empty;
    public string PersonalSite { get; set; } = string.Empty;

    public Gender Gender { get; set; } = Gender.NotSet;

    public string SchoolName { get; set; } = string.Empty;
}

public sealed class CreateUserRequestHandler(IPublishEndpoint publisher,
                                             IUserRepository userRepository,
                                             IUserDistributedCacheStorage cache) : RequestHandlerBase<CreateUserRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var userEntity = request.ToEntity();
        await userRepository.AddAsync(userEntity, cancellationToken);

        var userCreatedMessage = userEntity.ToUserCreated();

        await publisher.Publish(userCreatedMessage, cancellationToken);
        await cache.AddAsync(userEntity, cancellationToken);

        await userRepository.SaveChangesAsync(cancellationToken);

        return userEntity.ToResponse();
    }
}
