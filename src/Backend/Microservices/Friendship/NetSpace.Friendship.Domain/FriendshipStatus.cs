namespace NetSpace.Friendship.Domain;

public enum FriendshipStatus : byte
{
    Accepted = 1,
    Rejected = 2,
    WaitingForConfirmation = 3,
}
