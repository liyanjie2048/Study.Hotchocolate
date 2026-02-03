namespace Study.HotChocolate.GraphQL;

[QueryType]
public class UserQuery
{
    // [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> Users(
        [Service] DataContext context,
        IResolverContext resolverContext)
    {
        return context.Set<User>().AsNoTracking()
            .Select(resolverContext.Selection);
    }

    public Task<User?> UserById(
        Guid id,
        [Service] IUserByIdDataLoader userByIdDataLoader,
        IResolverContext resolverContext)
    {
        return userByIdDataLoader.Select(resolverContext.Selection).LoadAsync(id);
    }
}
