using Microsoft.AspNetCore.Identity;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Domain.User;

namespace NetSpace.User.Application.User.Requests;

public sealed record DeleteUserByIdRequest : RequestBase<DeleteUserByIdResponse>
{
    public required Guid Id { get; set; }
}

public sealed record DeleteUserByIdResponse : ResponseBase;

public sealed class DeleteUserByIdRequestHandler(UserManager<UserEntity> userManager) : RequestHandlerBase<DeleteUserByIdRequest, DeleteUserByIdResponse>
{
    public override async Task<DeleteUserByIdResponse> Handle(DeleteUserByIdRequest request, CancellationToken cancellationToken)
    {
        var userFromDb = await userManager.FindByIdAsync(request.Id.ToString())
            ?? throw new UserNotFoundException(request.Id);

        await userManager.DeleteAsync(userFromDb);

        return new DeleteUserByIdResponse();
    }
}
