using Microsoft.EntityFrameworkCore;
using NetSpace.Community.Domain.Community;
using NetSpace.Community.UseCases;
using NetSpace.Community.UseCases.Community;

namespace NetSpace.Community.Infrastructure.Community;

public sealed class CommunityRepository(NetSpaceDbContext dbContext) : RepositoryBase<CommunityEntity, int>(dbContext), ICommunityRepository
{
    public async Task<IEnumerable<CommunityEntity>?> FilterAsync(CommunityFilterOptions options,
                                                                 PaginationOptions pagination,
                                                                 SortOptions sort,
                                                                 CancellationToken cancellationToken = default)
    {
        var query = DbContext.Communities.AsQueryable();

        if (options.Id != default)
        {
            var community = await query.Where(c => c.Id == options.Id).ToArrayAsync(cancellationToken);

            return community;
        }

        if (!string.IsNullOrWhiteSpace(options.Name))
            query = query.Where(c => c.Name == options.Name);

        if (!string.IsNullOrWhiteSpace(options.Description))
            query = query.Where(c => c.Description == options.Description);

        if (options.CreatedAt is not null)
            query = query.Where(c => c.CreatedAt == options.CreatedAt);

        if (options.LastNameUpdatedAt is not null)
            query = query.Where(c => c.LastNameUpdatedAt == options.LastNameUpdatedAt);

        var result = await query.ToArrayAsync(cancellationToken: cancellationToken);

        return result;
    }
}
