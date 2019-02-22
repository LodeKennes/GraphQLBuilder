namespace GraphQLBuilder.Abstractions
{
    public interface IGraphQLScalarType<T> : IGraphQLScalarType
    {
        T Value { get; }
    }

    public interface IGraphQLScalarType : IGraphQLType
    {
        dynamic Value { get; }
    }
}
