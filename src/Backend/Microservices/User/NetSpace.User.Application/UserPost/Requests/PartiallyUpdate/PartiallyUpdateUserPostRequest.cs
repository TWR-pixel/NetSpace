using FluentValidation;
using MapsterMapper;
using NetSpace.User.Application.UserPost.Exceptions;
using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.Application.UserPost.Requests.PartiallyUpdate;

public sealed record PartiallyUpdateUserPostRequest : RequestBase<UserPostResponse>
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

public sealed class PartiallyUpdateUserPostRequestHandler(IUserPostRepository userPostRepository,
                                                          IValidator<PartiallyUpdateUserPostRequest> requestValidator,
                                                          IMapper mapper) : RequestHandlerBase<PartiallyUpdateUserPostRequest, UserPostResponse>
{
    public override async Task<UserPostResponse> Handle(PartiallyUpdateUserPostRequest request, CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAndThrowAsync(request, cancellationToken);

        var userPostEntity = await userPostRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostNotFoundException(request.Id);

        mapper.Map(request, userPostEntity);

        await userPostRepository.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserPostResponse>(userPostEntity);
    }
}
