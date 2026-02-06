namespace Study.HotChocolate.Data;

public class User : Entity
{
    [MaxLength(50)]
    public required string Name { get; set; }

    [MaxLength(50)]
    public required string Email { get; set; }

    public int Age { get; set; }
    public required Address Address { get; set; }
    public required State State { get; set; }
    public List<Identity> Identities { get; set; } = [];
    public virtual ICollection<Post> Posts { get; set; } = [];


    public int Type { get; set; }

    public string TypeName => Type switch
    {
        1 => "Admin",
        2 => "User",
        _ => "Unknown",
    };

    public Provider Provider { get; set; }

    public string ProviderName => Provider switch
    {
        Provider.Google => "Google",
        Provider.Facebook => "Facebook",
        _ => "Unknown",
    };


    public int? NameLength => Name?.Length;

    public string? EmailDomain => string.IsNullOrEmpty(Email)
        ? null
        : Email.Split('@').Last();

    public string? AgenSummary => $"{Age} years old";
    public string AddressDetail => $"Test: {(Address is null ? "null" : $"c:{Address.AdCode}({Address.Detail})")}";

    public string PostSummary => Posts.Count == 0
        ? "None"
        : string.Join(',', Posts.Select(x => x.Title));
}
