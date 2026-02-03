namespace Study.HotChocolate.GraphQL;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(_ => _.Id).IsProjected();
        // descriptor.Field(_ => _.EmailDomain).DependsOn("Email");
        // descriptor.Field(_ => _.NameLength).DependsOn("Name");
        // descriptor.Field(_ => _.AddressDetail).DependsOn("Address");

        descriptor.Field(_ => _.EmailDomain).ParentRequires("Email");
        descriptor.Field(_ => _.NameLength).ParentRequires("Name");
        descriptor.Field(_ => _.AddressDetail).ParentRequires("Address { AdCode Detail }");
        // .ParentRequires<User>(_ => _.Address.AdCode!)
        // .ParentRequires<User>(_ => _.Address.Detail!);
    }
}
