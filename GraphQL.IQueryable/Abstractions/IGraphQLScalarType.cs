namespace GraphQL.IQueryable.Abstractions
{
    public interface IGraphQLScalarType<T>
    {
        T Value { get; }
    }
}
