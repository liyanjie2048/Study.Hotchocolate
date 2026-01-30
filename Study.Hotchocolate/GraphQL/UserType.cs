namespace Study.HotChocolate.GraphQL;

public class UserType : ObjectType<User>
{
    /// <inheritdoc />
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(_ => _.Id).IsProjected();
        descriptor.Field(_ => _.EmailDomain).ParentRequires<User>(_ => _.Email);
        descriptor.Field(_ => _.NameLength).ParentRequires<User>(_ => _.Name);
        descriptor.Field(_ => _.AddressDetail)
            .ParentRequires<User>(_ => _.Address).UseProjection();
    }
}
