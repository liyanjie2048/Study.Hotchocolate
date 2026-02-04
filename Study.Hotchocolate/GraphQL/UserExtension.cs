namespace Study.HotChocolate.GraphQL;

[ExtendObjectType<User>]
public static class UserExtension
{
    public static string StateDescription(
        [Parent(nameof(User.State))] User user)
    {
        return $"{user.State?.Value}-{user.State?.Remark}";
    }

    public static string NameX2(
        [Parent(requires: nameof(User.Name))] User user)
    {
        return $"{user.Name} + {user.Name}";
    }

    public static int? IdentityCount(
        [Parent(nameof(User.Identities))] User user)
    {
        return user.Identities.Count;
    }

    [UsePaging]
    [UseFiltering]
    [UseSorting]
    [BindMember(nameof(User.Posts))]
    public static Task<Connection<Post>> Posts(
        [Parent(nameof(User.Id))] User user,
        QueryContext<Post> queryContext,
        PagingArguments pagingArguments,
        [Service] IPostsByUserIdDataLoader postsByUserIdDataLoader,
        CancellationToken cancellationToken = default)
    {
        return postsByUserIdDataLoader.With(pagingArguments, queryContext).LoadAsync(user.Id, cancellationToken).ToConnectionAsync();
    }
}
