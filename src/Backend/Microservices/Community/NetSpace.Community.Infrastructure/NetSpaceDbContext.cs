﻿using Microsoft.EntityFrameworkCore;
using NetSpace.Community.Domain;

namespace NetSpace.Community.Infrastructure;

public sealed class NetSpaceDbContext : DbContext
{
    public DbSet<CommunityEntity> Communities { get; set; }

    public NetSpaceDbContext(DbContextOptions<NetSpaceDbContext> options) : base(options)
    {

    }

    
}
