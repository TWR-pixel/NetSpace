namespace NetSpace.User.UseCases.UserPost;

public sealed class UserPostFilterOptions
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }

    public Guid? UserId { get; set; }
    public bool IncludeComments { get; set; } = false;
}
