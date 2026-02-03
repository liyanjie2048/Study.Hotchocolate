namespace Study.HotChocolate.GraphQL;

internal static class DataLoaders
{
    [DataLoader]
    public static async Task<Dictionary<Guid, User>> GetUserByIdAsync(
        IReadOnlyList<Guid> ids,
        DataContext context,
        CancellationToken cancellationToken)
    {
        return await context.Set<User>()
            .Where(_ => ids.Contains(_.Id))
            .ToDictionaryAsync(_ => _.Id, cancellationToken);
    }
}
