namespace Study.HotChocolate.Data;

public class Address
{
    public int AdCode { get; set; }
    [MaxLength(50)]
    public string? Detail { get; set; }
}

