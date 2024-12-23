using Microsoft.EntityFrameworkCore;
using NetSpace.User.Domain;

namespace NetSpace.User.Infrastructure;

public sealed class NetSpaceDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
