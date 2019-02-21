using GraphQL.IQueryable.Abstractions;

namespace GraphQL.IQueryable.Models
{
    public struct GraphQLQueryParam
    {
        public GraphQLQueryParam(string key, IGraphQLType type)
        {
            Key = key;
            Type = type;
        }

        public string Key { get; }
        public IGraphQLType Type { get; }
    }
}
