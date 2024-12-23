using NetSpace.Common.Application;

namespace NetSpace.User.Application.Requests;

public sealed record DeleteUserByIdRequest : RequestBase<DeleteUserByIdResponse>
{
    public required Guid Id { get; set; }
}

public sealed record DeleteUserByIdResponse : ResponseBase;

public sealed class DeleteUserByIdRequestHandler : RequestHandlerBase<DeleteUserByIdRequest, DeleteUserByIdResponse>
{
    public override Task<DeleteUserByIdResponse> Handle(DeleteUserByIdRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
