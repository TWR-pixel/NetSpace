﻿namespace NetSpace.CommunitySubscribers.Domain;

public interface IEntity<TId> where TId : notnull
{
    public TId Id { get; set; }
}
