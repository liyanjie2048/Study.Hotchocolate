namespace Study.HotChocolate.Data;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required Address Address { get; set; }
    public required State State { get; set; }
    public virtual ICollection<Post> Posts { get; set; } = [];

    public int NameLength => Name.Length;
    public string EmailDomain => Email.Split('@').Last();
    public string AddressDetail => $"Test: {Address.AdCode} {Address.Detail}";
}
