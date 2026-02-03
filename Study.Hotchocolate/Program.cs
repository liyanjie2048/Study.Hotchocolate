var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();
builder.Services.AddDbContextPool<DataContext>(optionsBuilder =>
{
    optionsBuilder.EnableDetailedErrors();
    optionsBuilder.EnableSensitiveDataLogging();
    optionsBuilder.UseSqlServer("Server=localhost;Database=Study.HotChocolate;Uid=sa;Pwd=sa;TrustServerCertificate=true");
    optionsBuilder.UseSeeding((context, f) =>
    {
        context.Set<User>().AddRange(new Faker<User>()
            .RuleFor(x => x.Name, _ => _.Name.FullName())
            .RuleFor(x => x.Email, _ => _.Internet.Email())
            .RuleFor(_ => _.Address, _ => new Faker<Address>()
                .RuleFor(x => x.AdCode, a => a.Random.Number(100000000, 999999999).ToString())
                .RuleFor(x => x.Detail, a => a.Address.StreetAddress()))
            .RuleFor(_ => _.State, _ => new Faker<State>()
                .RuleFor(x => x.Value, a => true)
                .RuleFor(_ => _.Remark, a => a.Lorem.Letter(6)))
            .RuleFor(_ => _.Identities, _ => new Faker<Identity>()
                .RuleFor(x => x.Id, a => a.Random.Guid())
                .RuleFor(x => x.Type, a => a.Lorem.Letter(2))
                .RuleFor(x => x.Value, a => a.Name.LastName())
                .Generate(3))
            .RuleForType<ICollection<Post>>(typeof(ICollection<Post>), p => new Faker<Post>()
                .RuleFor(x => x.Title, _ => _.Lorem.Sentence(5))
                .RuleFor(x => x.Content, _ => _.Lorem.Sentence(10))
                .Generate(3))
            .Generate(10));
        context.SaveChanges();
    });
});

builder.Services.AddGraphQLServer()
    .ModifyRequestOptions(o => o.IncludeExceptionDetails = true)
    .AddSorting()
    .AddFiltering()
    .AddProjections()
    .AddHotchocolateTypes()
    ;

var app = builder.Build();

app.MapGraphQL();

app.Run();
