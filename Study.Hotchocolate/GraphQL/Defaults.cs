namespace Study.HotChocolate.GraphQL;

public sealed class Defaults
{
    public static SortDefinition<T> DefaultOrder<T>(SortDefinition<T> definition)
        where T : Entity
    {
        return definition.IfEmpty(_ => _.AddDescending(__ => __.CreatedAt))
            .AddAscending(_ => _.Id);
    }
}
