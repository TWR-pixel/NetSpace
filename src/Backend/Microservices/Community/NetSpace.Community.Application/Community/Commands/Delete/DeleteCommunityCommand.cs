﻿using MapsterMapper;
using NetSpace.Community.Application.Common;
using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.UseCases.Community;

namespace NetSpace.Community.Application.Community.Commands.Delete;

public sealed record DeleteCommunityCommand : CommandBase<CommunityResponse>
{
    public required int Id { get; set; }
}

public sealed class DeleteCommunityRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : CommandHandlerBase<DeleteCommunityCommand, CommunityResponse>(unitOfWork)
{
    public override async Task<CommunityResponse> Handle(DeleteCommunityCommand request, CancellationToken cancellationToken)
    {
        var entity = await UnitOfWork.Communities.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityNotFoundException(request.Id);

        await UnitOfWork.Communities.DeleteAsync(entity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CommunityResponse>(entity);
    }
}
