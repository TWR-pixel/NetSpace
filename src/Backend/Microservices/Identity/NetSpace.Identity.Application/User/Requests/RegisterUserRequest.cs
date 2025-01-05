using MassTransit;
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
    public Gender Gender { get; set; } = Gender.NotSet;
    public Language Language { get; set; } = Language.NotSet;
    public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.NotSet;
}

public sealed class RegisterUserRequestHandler(IUserRepository userRepository, IPublishEndpoint publishEndpoint) : RequestHandlerBase<RegisterUserRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var userFromDbEntity = await userRepository.FindByEmailAsync(request.Email);

        if (userFromDbEntity != null)
            throw new UserAlreadyExistsException(userFromDbEntity.Email);

        var newUserEntity = new UserEntity
        {
            Nickname = request.Nickname,
            UserName = request.UserName,
            Surname = request.Surname,
            LastName = request.LastName,
            About = request.About,
            BirthDate = request.BirthDate,
            Language = request.Language,
            MaritalStatus = request.MaritalStatus,
            Gender = request.Gender,
        };

        await userRepository.AddAsync(newUserEntity, request.Password, cancellationToken);
        await publishEndpoint.Publish(newUserEntity.ToCreated(), cancellationToken);

        return new UserResponse();
    }
}