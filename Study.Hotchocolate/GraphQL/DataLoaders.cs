namespace Study.HotChocolate.GraphQL;

internal static class DataLoaders
{
    [DataLoader]
    public static async Task<Dictionary<Guid, User>> GetUserByIdAsync(
        IReadOnlyList<Guid> ids,
        DataContext context,
        CancellationToken cancellationToken)
    {
        return await context.Set<User>().AsNoTracking()
            .Where(_ => ids.Contains(_.Id))
            .ToDictionaryAsync(_ => _.Id, cancellationToken);
    }

    [DataLoader]
    public static async Task<Dictionary<Guid, Post>> GetPostByIdAsync(
        IReadOnlyList<Guid> ids,
        DataContext context,
        CancellationToken cancellationToken)
    {
        return await context.Set<Post>().AsNoTracking()
            .Where(_ => ids.Contains(_.Id))
            .ToDictionaryAsync(_ => _.Id, cancellationToken);
    }
}
