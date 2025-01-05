﻿using NetSpace.User.Domain.UserPost;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.Common.Repositories;

namespace NetSpace.User.UseCases.UserPost;

public interface IUserPostRepository : IRepository<UserPostEntity, int>
{
    public Task<IEnumerable<UserPostEntity>> FilterAsync(UserPostFilterOptions filter, PaginationOptions paginationk, SortOptions sort, CancellationToken cancellationToken = default);
}
