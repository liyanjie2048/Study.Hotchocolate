namespace Study.HotChocolate.Data;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required Address Address { get; set; }
    public required State State { get; set; }
    public List<Identity> Identities { get; set; } = [];
    public virtual ICollection<Post> Posts { get; set; } = [];


    public int? NameLength => Name?.Length;

    public string? EmailDomain => string.IsNullOrEmpty(Email)
        ? null
        : Email.Split('@').Last();

    public string AddressDetail => $"Test: {(Address is null ? "null" : $"C:{Address.AdCode} ({Address.Detail})")}";
}
