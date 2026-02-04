namespace Study.HotChocolate.GraphQL;

[QueryType]
public static class UserQuery
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static ValueTask<Connection<User>> Users(
        PagingArguments pagingArguments,
        QueryContext<User> queryContext,
        [Service] DataContext context,
        CancellationToken cancellationToken = default)
    {
        return context.Set<User>().AsNoTracking()
            .With(queryContext.Include(_ => _.Id), Defaults.DefaultOrder)
            .ToPageAsync(pagingArguments, cancellationToken)
            .ToConnectionAsync();
    }

    [UseFirstOrDefault]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<User> UserFirst(
        QueryContext<User> queryContext,
        [Service] DataContext context,
        CancellationToken cancellationToken = default)
    {
        return context.Set<User>().AsNoTracking()
            .With(queryContext.Include(_ => _.Id), Defaults.DefaultOrder);
    }

    public static Task<User?> UserById(
        Guid id,
        [Service] IUserByIdDataLoader userByIdDataLoader,
        CancellationToken cancellationToken = default)
    {
        return userByIdDataLoader.LoadAsync(id, cancellationToken);
    }
}
