namespace Study.HotChocolate.GraphQL;

[ExtendObjectType<Post>]
public class PostExtension
{
    public string UserDisplay(
        [Parent("User {Name Email}")] Post post)
    {
        return $"{post.User.Name}({post.User.Email})";
    }
}
