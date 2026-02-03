namespace Study.HotChocolate.GraphQL;

[ExtendObjectType<User>]
public class UserExtension
{
    public string StateDescription(
        [Parent("State{Value Remark}")] User user)
    {
        return $"{user.State?.Value}-{user.State?.Remark}";
    }

    public string NameName(
        [Parent(requires: nameof(User.Name))] User user)
    {
        return $"{user.Name}+{user.Name}";
    }
}

