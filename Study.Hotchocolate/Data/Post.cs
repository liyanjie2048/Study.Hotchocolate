namespace Study.HotChocolate.Data;

public class Post : Entity
{
    public required string Title { get; set; }
    public required string Content { get; set; }

    public Guid User_Id { get; set; }
    public virtual User User { get; set; } = null!;
}
