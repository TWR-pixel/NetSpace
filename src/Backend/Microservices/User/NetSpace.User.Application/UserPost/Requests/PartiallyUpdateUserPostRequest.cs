using FluentValidation;
using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.Application.UserPost.Requests;

public sealed record PartiallyUpdateUserPostRequest : RequestBase<UserPostResponse>
{
    public string? Title { get; set; }
    public string? Body { get; set; }

    public Guid? OwnerId { get; set; }
}

public sealed class PartiallyUpdateUserPostRequestValidator : AbstractValidator<PartiallyUpdateUserPostRequest>
{
    public PartiallyUpdateUserPostRequestValidator()
    {
        
    }
}

public sealed class PartiallyUpdateUserPostRequestHandler(IUserPostRepository userPostRepository) : RequestHandlerBase<PartiallyUpdateUserPostRequest, UserPostResponse>
{
    public override Task<UserPostResponse> Handle(PartiallyUpdateUserPostRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
