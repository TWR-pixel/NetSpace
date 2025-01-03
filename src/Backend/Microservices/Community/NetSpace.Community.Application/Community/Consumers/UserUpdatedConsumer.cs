using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.Community.Domain.User;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Application.Community.Consumers;

public sealed class UserUpdatedConsumer(IUserRepository userRepository) : IConsumer<UserUpdatedMessage>
{
    public async Task Consume(ConsumeContext<UserUpdatedMessage> context)
    {
        var msg = context.Message;

        var userEntity = await userRepository.FindByIdAsync(msg.Id, context.CancellationToken);

        var newUserEntity = new UserEntity
        {
            Id = msg.Id,
            Nickname = msg.Nickname,
            UserName = msg.UserName,
            Surname = msg.Surname,
            LastName = msg.LastName,
            Email = msg.Email,
            About = msg.About,
            AvatarUrl = msg.AvatarUrl,
            BirthDate = msg.BirthDate,
            RegistrationDate = msg.RegistrationDate,
            LastLoginAt = msg.LastLoginAt,
            Hometown = msg.Hometown,
            Language = (Domain.User.Language)msg.Language,
            MaritalStatus = (Domain.User.MaritalStatus)msg.MaritalStatus,
            CurrentCity = msg.CurrentCity,
            PersonalSite = msg.PersonalSite,
            Gender = (Domain.User.Gender)msg.Gender,
            SchoolName = msg.SchoolName,
        };

        if (userEntity is null)
        {
            await userRepository.AddAsync(newUserEntity, context.CancellationToken);
            await userRepository.SaveChangesAsync(context.CancellationToken);

            return;
        }

        userEntity = newUserEntity;

        await userRepository.UpdateAsync(userEntity, context.CancellationToken);
        await userRepository.SaveChangesAsync(context.CancellationToken);
    }
}
