using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Infrastructure;

public sealed class NetSpaceDbContext(DbContextOptions<NetSpaceDbContext> options) : IdentityDbContext<UserEntity>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
