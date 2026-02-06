namespace Study.HotChocolate.GraphQL;

public class UserObjectType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(_ => _.NameLength).ParentRequires<User>(_ => _.Name);
        descriptor.Field(_ => _.EmailDomain).ParentRequires<User>(_ => _.Email);

        // descriptor.Field(_ => _.AgenSummary).ParentRequires<User>(_ =>  _.Age );            // does not work
        // descriptor.Field(_ => _.AgenSummary).ParentRequires<User>(_ => "Age");              // works
        descriptor.Field(_ => _.AgenSummary).ParentRequires<User>(_ => new { _.Age }); // works

        // descriptor.Field(_ => _.AddressDetail).ParentRequires<User>(_ => new { _.Address.AdCode, _.Address.Detail });    // does not work
        descriptor.Field(_ => _.AddressDetail)
            .ParentRequires<User>(_ => _.Address.AdCode)
            .ParentRequires<User>(_ => _.Address.Detail!); // works

        descriptor.Field(_ => _.Identities).ParentRequires<User>(_ => _.Identities);
        descriptor.Field(_ => _.PostSummary).ParentRequires<User>(_ => _.Posts.Select(__ => __.Title));    // does not work

        // descriptor.Field(_ => _.TypeName).ParentRequires<User>(_ => _.Type);                // does not work
        // descriptor.Field(_ => _.ProviderName).ParentRequires<User>(_ => _.Provider);        // does not work
        // descriptor.Field(_ => _.TypeName).ParentRequires("Type");                           // works
        // descriptor.Field(_ => _.ProviderName).ParentRequires("Provider");                   // works
        descriptor.Field(_ => _.TypeName).ParentRequires<User>(_ => new { _.Type });         // works
        descriptor.Field(_ => _.ProviderName).ParentRequires<User>(_ => new { _.Provider }); // works
    }
}
