namespace Study.HotChocolate.GraphQL;

internal static class DataLoaders
{
    [DataLoader]
    public static Task<Dictionary<Guid, User>> GetUserByIdAsync(
        IReadOnlyList<Guid> ids,
        [Service] DataContext context,
        CancellationToken cancellationToken=default)
    {
        return context.Set<User>()
            .Where(_ => ids.Contains(_.Id))
            .ToDictionaryAsync(_ => _.Id, cancellationToken);
    }

    [DataLoader]
    public static Task<Dictionary<Guid, Post>> GetPostByIdAsync(
        IReadOnlyList<Guid> ids,
        [Service] DataContext context,
        CancellationToken cancellationToken=default)
    {
        return context.Set<Post>()
            .Where(_ => ids.Contains(_.Id))
            .ToDictionaryAsync(_ => _.Id, cancellationToken);
    }

    [DataLoader]
    public static async Task<Dictionary<Guid, Page<Post>>> GetPostsByUserIdAsync(
        IReadOnlyList<Guid> ids,
        PagingArguments pagingArguments,
        QueryContext<Post> queryContext,
        [Service] DataContext context,
        CancellationToken cancellationToken = default)
    {
        return await context.Set<Post>()
            .Where(_ => ids.Contains(_.User_Id))
            .With(queryContext.Include(_ => _.Id).Include(_ => _.User_Id), Defaults.DefaultOrder)
            .ToBatchPageAsync(_ => _.User_Id, pagingArguments, cancellationToken);
    }
}
