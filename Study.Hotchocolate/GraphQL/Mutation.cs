namespace Study.HotChocolate.GraphQL;

[MutationType]
public class Mutation
{
    public string Delete(int id)
    {
        return $"{id} is deleted";
    }
}
