namespace Study.HotChocolate.GraphQL;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(_ => _.Id).IsProjected();
        descriptor.Field(_ => _.EmailDomain).ParentRequires("Email");
        descriptor.Field(_ => _.NameLength).ParentRequires("Name");
        descriptor.Field(_ => _.AddressDetail).ParentRequires("Address { AdCode Detail }");
    }
}
