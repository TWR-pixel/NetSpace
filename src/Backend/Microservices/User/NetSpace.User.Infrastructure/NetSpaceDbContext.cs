﻿using Microsoft.EntityFrameworkCore;
using NetSpace.User.Domain.User;
using NetSpace.User.Domain.UserPost;
using NetSpace.User.Domain.UserPostUserComment;

namespace NetSpace.User.Infrastructure;

public sealed class NetSpaceDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserPostEntity> UserPosts { get; set; }
    public DbSet<UserPostUserCommentEntity> UserPostUserComments { get; set; }

    public NetSpaceDbContext(DbContextOptions<NetSpaceDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NetSpaceDbContext).Assembly);
    }
}
