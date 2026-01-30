namespace Study.HotChocolate.GraphQL;

[ExtendObjectType<User>]
public class UserExtension
{
    public string StateDescription(
        [Parent(requires: nameof(User.State))] User user)
    {
        return $"{user.State.Value}-{user.State.Remark}";
    }
}

