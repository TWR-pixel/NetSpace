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
    public Language Language { get; set; } = Language.NotSet;
    public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.NotSet;
}

public sealed class RegisterUserRequestHandler(IUserRepository userRepository, IPublishEndpoint publishEndpoint) : RequestHandlerBase<RegisterUserRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var userFromDb = await userRepository.FindByEmailAsync(request.Email);

        if (userFromDb != null)
            throw new UserAlreadyExistsException(userFromDb.Email);

        var newUser = new UserEntity(request.Nickname,
                                     request.UserName,
                                     request.Surname,
                                     request.LastName,
                                     request.About,
                                     birthDate: request.BirthDate,
                                     gender: request.Gender,
                                     language: request.Language,
                                     maritalStatus: request.MaritalStatus);

        await userRepository.AddAsync(newUser, request.Password, cancellationToken);

        await publishEndpoint.Publish(new UserCreatedMessage(newUser.Id,
                                                             newUser.Nickname,
                                                             newUser.Name,
                                                             newUser.Surname,
                                                             newUser.Email,
                                                             newUser.LastName,
                                                             newUser.About,
                                                             newUser.AvatarUrl,
                                                             (NetSpace.Common.Messages.User.Gender)newUser.Gender), cancellationToken);

        return new UserResponse();
    }
}