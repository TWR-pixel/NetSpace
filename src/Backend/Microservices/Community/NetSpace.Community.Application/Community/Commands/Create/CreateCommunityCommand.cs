using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.Common.Exceptions;
using NetSpace.Community.Domain.Community;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.Community.Commands.Create;

public sealed record CreateCommunityCommand : CommandBase<CommunityResponse>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    public required Guid OwnerId { get; set; }
}

public sealed class CreateCommunityCommandValidator : AbstractValidator<CreateCommunityCommand>
{
    public CreateCommunityCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(256);

        RuleFor(c => c.Description)
            .MaximumLength(512);

        RuleFor(c => c.OwnerId)
            .NotNull();
    }
}

public sealed class CreateCommunityCommandHandler(IUnitOfWork unitOfWork,
                                                  IValidator<CreateCommunityCommand> validator,
                                                  IMapper mapper) : CommandHandlerBase<CreateCommunityCommand, CommunityResponse>(unitOfWork)
{
    public override async Task<CommunityResponse> Handle(CreateCommunityCommand request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var userEntity = await UnitOfWork.Users.FindByIdAsync(request.OwnerId, cancellationToken)
            ?? throw new UserNotFoundException(request.OwnerId);

        var communityEntity = mapper.Map<CommunityEntity>(request);

        var result = await UnitOfWork.Communities.AddAsync(communityEntity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<CommunityResponse>(result);

        return response;
    }
}
