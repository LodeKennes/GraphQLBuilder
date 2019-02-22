namespace GraphQLBuilder.Abstractions
{
    public interface IGraphQLScalarType<T>
    {
        T Value { get; }
    }

    public interface IGraphQLScalarType
    {
        dynamic Value { get; }
    }
}
