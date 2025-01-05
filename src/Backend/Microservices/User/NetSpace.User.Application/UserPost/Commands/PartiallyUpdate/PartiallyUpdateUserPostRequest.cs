using FluentValidation;
using MapsterMapper;
using NetSpace.User.Application.UserPost.Exceptions;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.UserPost.Commands.PartiallyUpdate;

public sealed record PartiallyUpdateUserPostRequest : CommandBase<UserPostResponse>
{
    public required int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
}

public sealed class PartiallyUpdateUserPostRequestValidator : AbstractValidator<PartiallyUpdateUserPostRequest>
{
    public PartiallyUpdateUserPostRequestValidator()
    {

    }
}

public sealed class PartiallyUpdateUserPostRequestHandler(IUnitOfWork unitOfWork,
                                                          IValidator<PartiallyUpdateUserPostRequest> requestValidator,
                                                          IMapper mapper) : CommandHandlerBase<PartiallyUpdateUserPostRequest, UserPostResponse>(unitOfWork)
{
    public override async Task<UserPostResponse> Handle(PartiallyUpdateUserPostRequest request, CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAndThrowAsync(request, cancellationToken);

        var userPostEntity = await UnitOfWork.UserPosts.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostNotFoundException(request.Id);

        mapper.Map(request, userPostEntity);

        return mapper.Map<UserPostResponse>(userPostEntity);
    }
}
