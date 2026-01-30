namespace Study.HotChocolate.GraphQL;

[QueryType]
public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> Users(
        [Service] DataContext context)
        => context.Set<User>().AsQueryable();
}
