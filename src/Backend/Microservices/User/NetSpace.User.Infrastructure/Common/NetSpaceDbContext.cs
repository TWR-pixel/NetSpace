using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetSpace.User.Domain;

namespace NetSpace.User.Infrastructure.Common;

public sealed class NetSpaceDbContext : IdentityDbContext<UserEntity>
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
