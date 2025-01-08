﻿using MapsterMapper;
using NetSpace.User.Application.UserPostUserComment.Exceptions;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.UserPostUserComment.Commands.Delete;

public sealed record DeleteUserPostUserCommentCommand : CommandBase<UserPostUserCommentResponse>
{
    public required int Id { get; set; }
}

public sealed class DeleteUserPostUserCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : CommandHandlerBase<DeleteUserPostUserCommentCommand, UserPostUserCommentResponse>(unitOfWork)
{
    public override async Task<UserPostUserCommentResponse> Handle(DeleteUserPostUserCommentCommand request, CancellationToken cancellationToken)
    {
        var userCommentEntity = await UnitOfWork.UserPostUserComments.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostUserCommentNotFoundException(request.Id);

        await UnitOfWork.UserPostUserComments.DeleteAsync(userCommentEntity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserPostUserCommentResponse>(userCommentEntity);
    }
}
