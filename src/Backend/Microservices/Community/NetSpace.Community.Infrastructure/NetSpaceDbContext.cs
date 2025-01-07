using Microsoft.EntityFrameworkCore;
using NetSpace.Community.Domain.Community;
using NetSpace.Community.Domain.CommunityPost;
using NetSpace.Community.Domain.CommunityPostUserComment;
using NetSpace.Community.Domain.CommunitySubscription;
using NetSpace.Community.Domain.User;

namespace NetSpace.Community.Infrastructure;

public sealed class NetSpaceDbContext : DbContext
{
    public DbSet<CommunityEntity> Communities { get; set; }
    public DbSet<CommunityPostEntity> CommunityPosts { get; set; }
    public DbSet<CommunityPostUserCommentEntity> CommunityPostUserComments { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CommunitySubscriptionEntity> CommunitySubscriptions { get; set; }

    public NetSpaceDbContext(DbContextOptions<NetSpaceDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NetSpaceDbContext).Assembly);
    }
}
