namespace GraphQL.IQueryable.Abstractions
{
    public interface IGraphQLQuery<T>
    {
        IGraphQLQuery<T> WithParam<TType>(string key, TType value) where TType : IGraphQLType;
        IGraphQLRequest<T> Build();
    }
}
