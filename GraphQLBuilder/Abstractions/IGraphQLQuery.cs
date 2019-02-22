namespace GraphQLBuilder.Abstractions
{
    public interface IGraphQLQuery<T>
    {
        IGraphQLQuery<T> WithParam<TType>(string key, TType value) where TType : IGraphQLScalarType;
        IGraphQLRequest<T> GetRequest();
    }
}
