using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Infrastructure;

public sealed class NetSpaceDbContext : IdentityDbContext<UserEntity>
{
    public NetSpaceDbContext(DbContextOptions<NetSpaceDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
