namespace Study.HotChocolate.Data;

public class Identity
{
    public Guid Id { get; set; }
    public required string Type { get; set; }
    public required string Value { get; set; }
}

