using Microsoft.EntityFrameworkCore;
using NetSpace.Community.Domain.Community;

namespace NetSpace.Community.Infrastructure;

public sealed class NetSpaceDbContext : DbContext
{
    public DbSet<CommunityEntity> Communities { get; set; }

    public NetSpaceDbContext(DbContextOptions<NetSpaceDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NetSpaceDbContext).Assembly);
    }
}
