using MapsterMapper;
using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Identity.Application.User.Exceptions;
using NetSpace.Identity.Domain.User;
using NetSpace.Identity.UseCases.User;

namespace NetSpace.Identity.Application.User.Requests;

public sealed record RegisterUserRequest : RequestBase<UserResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Nickname { get; set; }
    public required string UserName { get; set; }
    public required string Surname { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;

    public DateTime? BirthDate { get; set; }
    public Domain.User.Gender Gender { get; set; } = Domain.User.Gender.NotSet;
    public Domain.User.Language Language { get; set; } = Domain.User.Language.NotSet;
    public Domain.User.MaritalStatus MaritalStatus { get; set; } = Domain.User.MaritalStatus.NotSet;
}

public sealed class RegisterUserRequestHandler(IUserRepository userRepository, IPublishEndpoint publishEndpoint, IMapper mapper) : RequestHandlerBase<RegisterUserRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var userFromDbEntity = await userRepository.FindByEmailAsync(request.Email);

        if (userFromDbEntity != null)
            throw new UserAlreadyExistsException(userFromDbEntity.Email);

        var newUserEntity = mapper.Map<UserEntity>(request);

        await userRepository.AddAsync(newUserEntity, request.Password, cancellationToken);
        await publishEndpoint.Publish(mapper.Map<UserCreatedMessage>(newUserEntity), cancellationToken);

        return mapper.Map<UserResponse>(newUserEntity);
    }
}