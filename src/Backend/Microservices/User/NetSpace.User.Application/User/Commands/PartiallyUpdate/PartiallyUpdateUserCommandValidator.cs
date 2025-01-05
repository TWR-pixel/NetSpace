using FluentValidation;

namespace NetSpace.User.Application.User.Commands.PartiallyUpdate;

public sealed class PartiallyUpdateUserCommandValidator : AbstractValidator<PartiallyUpdateUserCommand>
{
    public PartiallyUpdateUserCommandValidator()
    {
        RuleFor(r => r.Nickname)
            .MaximumLength(50)
            .NotEmpty()
            .When(r => r.Nickname is not null);

        RuleFor(r => r.Name)
            .MaximumLength(100)
            .NotEmpty()
            .When(r => r.Name is not null);

        RuleFor(r => r.Surname)
            .MaximumLength(100)
            .NotEmpty()
            .When(r => r.Surname is not null);

        //RuleFor(r => r.Email) move to identity
        //    .MaximumLength(50)
        //    .EmailAddress()
        //    .Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")
        //    .When(r => r.Email is not null);

        RuleFor(r => r.LastName)
            .MaximumLength(50)
            .NotEmpty()
            .When(r => r.LastName is not null);

        RuleFor(r => r.About)
            .MaximumLength(512)
            .When(r => r.About is not null);

        RuleFor(r => r.Hometown)
            .MaximumLength(50)
            .When(r => r.Hometown is not null);

        RuleFor(r => r.CurrentCity)
            .MaximumLength(55)
            .When(r => r.CurrentCity is not null);

        RuleFor(r => r.SchoolName)
            .MaximumLength(50)
            .When(r => r.SchoolName is not null);
    }
}
