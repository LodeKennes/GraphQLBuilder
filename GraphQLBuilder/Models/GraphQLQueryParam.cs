using GraphQLBuilder.Abstractions;

namespace GraphQLBuilder.Models
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
