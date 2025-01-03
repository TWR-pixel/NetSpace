using FluentValidation;
using NetSpace.User.Application.UserPost.Exceptions;
using NetSpace.User.Application.UserPost.Extensions;
using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.Application.UserPost.Requests.PartiallyUpdate;

public sealed record PartiallyUpdateUserPostRequest : RequestBase<UserPostResponse>
{
    public required int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}

public sealed class PartiallyUpdateUserPostRequestValidator : AbstractValidator<PartiallyUpdateUserPostRequest>
{
    public PartiallyUpdateUserPostRequestValidator()
    {

    }
}

public sealed class PartiallyUpdateUserPostRequestHandler(IUserPostRepository userPostRepository,
                                                          IValidator<PartiallyUpdateUserPostRequest> requestValidator) : RequestHandlerBase<PartiallyUpdateUserPostRequest, UserPostResponse>
{
    public override async Task<UserPostResponse> Handle(PartiallyUpdateUserPostRequest request, CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAndThrowAsync(request, cancellationToken);

        var userPostEntity = await userPostRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostNotFoundException(request.Id);

        userPostEntity.Title = request.Title;
        userPostEntity.Body = request.Body;

        await userPostRepository.UpdateAsync(userPostEntity, cancellationToken);
        await userPostRepository.SaveChangesAsync(cancellationToken);

        return userPostEntity.ToResponse();
    }
}
