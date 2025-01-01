using Microsoft.EntityFrameworkCore;
using NetSpace.User.Domain.User;

namespace NetSpace.User.Infrastructure.Common;

public sealed class NetSpaceDbContext(DbContextOptions<NetSpaceDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserPostEntity> UserPosts { get; set; }
    public DbSet<UserPostUserCommentEntity> UserPostUserComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
