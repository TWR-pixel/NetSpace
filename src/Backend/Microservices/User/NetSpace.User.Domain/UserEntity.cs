using NetSpace.Common.Domain;

namespace NetSpace.User.Domain;

public sealed class UserEntity : IEntity<string>
{
    public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
