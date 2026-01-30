namespace Study.HotChocolate.Data;

public class Post
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }

    public Guid User_Id { get; set; }
    public virtual User User { get; set; } = null!;
}
