namespace Study.HotChocolate.GraphQL;

[ExtendObjectType<Post>]
public static class PostExtension
{
    public static string UserDisplay(
        [Parent("User{Name Email}")] Post post)
    {
        return $"{post.User.Name}({post.User.Email})";
    }
}
