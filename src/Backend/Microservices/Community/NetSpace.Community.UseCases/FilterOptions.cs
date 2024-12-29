namespace NetSpace.Community.UseCases;

public sealed class FilterOptions
{
    public int Id { get; set; } = default;
    public string? Name { get; set; }
    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? LastNameUpdatedAt { get; set; }
}
