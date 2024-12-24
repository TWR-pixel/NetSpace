using NetSpace.Common.Application;

namespace NetSpace.User.Application.User.Requests;

public sealed record GetUserByIdRequest : RequestBase<GetUserByIdResponse>
{
}

public sealed record GetUserByIdResponse : ResponseBase;

public sealed class GetUserByIdRequestHandler : RequestHandlerBase<GetUserByIdRequest, GetUserByIdResponse>
{
    public override Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
