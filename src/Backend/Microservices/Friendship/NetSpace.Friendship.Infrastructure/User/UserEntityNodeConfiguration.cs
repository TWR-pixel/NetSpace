using Neo4j.Berries.OGM.Interfaces;
using Neo4j.Berries.OGM.Models.Config;
using NetSpace.Friendship.Domain;

namespace NetSpace.Friendship.Infrastructure.User;

public sealed class UserEntityNodeConfiguration : INodeConfiguration<UserEntity>
{
    public void Configure(NodeTypeBuilder<UserEntity> builder)
    {
        builder.HasRelationWithMultiple(u => u.Followers, "FRIEND_WITH", Neo4j.Berries.OGM.Enums.RelationDirection.Out);
        
    }
}
