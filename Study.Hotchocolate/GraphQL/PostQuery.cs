namespace Study.HotChocolate.GraphQL;

[QueryType]
public class PostQuery
{
    [UseFiltering]
    [UseSorting]
    public IQueryable<Post> Posts(
        [Service] DataContext context,
        IResolverContext resolverContext)
    {
        return context.Set<Post>().AsNoTracking()
            .Select(resolverContext.Selection);
    }

    public Task<Post?> PostById(
        Guid id,
        [Service] IPostByIdDataLoader postByIdDataLoader,
        IResolverContext resolverContext)
    {
        return postByIdDataLoader.Select(resolverContext.Selection).LoadAsync(id);
    }
}

