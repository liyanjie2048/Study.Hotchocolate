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
            .RuleForType<ICollection<Post>>(typeof(ICollection<Post>), p => new Faker<Post>()
                .RuleFor(x => x.Title, _ => _.Lorem.Sentence(5))
                .RuleFor(x => x.Content, _ => _.Lorem.Sentence(10))
                .Generate(3))
            .Generate(10));
        context.SaveChanges();
    });
});

builder.Services.AddGraphQLServer()
    .ModifyRequestOptions(o => { o.IncludeExceptionDetails = true; })
    .AddSorting()
    .AddFiltering()
    // .AddProjections(descriptor =>
    // {
    //     descriptor.Provider(new MyQueryableProjectionProvider(x =>
    //     {
    //         ArgumentNullException.ThrowIfNull(descriptor);
    //         x.RegisterFieldHandler<QueryableProjectionScalarHandler>();
    //         x.RegisterFieldHandler<QueryableProjectionListHandler>();
    //         x.RegisterFieldHandler<QueryableProjectionFieldHandler>();
    //         x.RegisterFieldInterceptor<QueryableFilterInterceptor>();
    //         x.RegisterFieldInterceptor<QueryableSortInterceptor>();
    //         x.RegisterFieldInterceptor<QueryableFirstOrDefaultInterceptor>();
    //         x.RegisterFieldInterceptor<QueryableSingleOrDefaultInterceptor>();
    //         x.RegisterOptimizer<IsProjectedProjectionOptimizer>();
    //         x.RegisterOptimizer<QueryablePagingProjectionOptimizer>();
    //         x.RegisterOptimizer<QueryableFilterProjectionOptimizer>();
    //         x.RegisterOptimizer<QueryableSortProjectionOptimizer>();
    //     }));
    // })
    .AddProjections()
    .AddHotchocolateTypes()
    ;

var app = builder.Build();

app.MapGraphQL();

app.Run();
