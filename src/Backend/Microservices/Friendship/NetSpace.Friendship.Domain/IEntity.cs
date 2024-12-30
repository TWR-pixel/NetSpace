namespace NetSpace.Friendship.Domain;

public interface IEntity<TId> where TId : notnull
{
    public TId Id { get; set; }
}
