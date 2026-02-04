namespace Study.HotChocolate.GraphQL;

[QueryType]
public static class PostQuery
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static ValueTask<Connection<Post>> Posts(
        PagingArguments pagingArguments,
        QueryContext<Post> queryContext,
        [Service] DataContext context,
        CancellationToken cancellationToken = default)
    {
        return context.Set<Post>()
            .With(queryContext.Include(_ => _.Id), Defaults.DefaultOrder)
            .ToPageAsync(pagingArguments, cancellationToken)
            .ToConnectionAsync();
    }

    public static Task<Post?> PostById(
        Guid id,
        [Service] IPostByIdDataLoader postByIdDataLoader)
    {
        return postByIdDataLoader.LoadAsync(id);
    }
}
