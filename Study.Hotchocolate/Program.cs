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
            .RuleForType<ICollection<Post>>(typeof(ICollection<Post>), _ => new Faker<Post>()
                .RuleFor(x => x.Title, _ => _.Lorem.Sentence(5))
                .RuleFor(x => x.Content, _ => _.Lorem.Sentence(10))
                .Generate(3))
            .Generate(10));
        context.SaveChanges();
    });
});
builder.Services.AddGraphQLServer()
    .AddSorting()
    .AddFiltering()
    .AddProjections()
    .AddHotchocolateTypes();

var app = builder.Build();

app.MapGraphQL();

app.Run();
