using Neo4j.Berries.OGM.Contexts;
using Neo4j.Berries.OGM.Models.Sets;
using NetSpace.Friendship.Domain;
using System.Data;

namespace NetSpace.Friendship.Infrastructure;

public sealed class NetSpaceDbContext(Neo4jOptions options) : GraphContext(options)
{
    public NodeSet<UserEntity> Users { get; set; }

}
