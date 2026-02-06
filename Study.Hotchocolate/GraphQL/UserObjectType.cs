namespace Study.HotChocolate.GraphQL;

public class UserObjectType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(_ => _.EmailDomain).ParentRequires("Email");
        descriptor.Field(_ => _.NameLength).ParentRequires("Name");
        descriptor.Field(_ => _.AddressDetail).ParentRequires("Address");
        descriptor.Field(_ => _.Identities).ParentRequires("Identities");

        // descriptor.Field(_ => _.TypeName).ParentRequires<User>(_ => _.Type);            // does not work
        // descriptor.Field(_ => _.ProviderName).ParentRequires<User>(_ => _.Provider);    // does not work
        descriptor.Field(_ => _.TypeName).ParentRequires("Type");         // works
        descriptor.Field(_ => _.ProviderName).ParentRequires("Provider"); // works
    }
}
