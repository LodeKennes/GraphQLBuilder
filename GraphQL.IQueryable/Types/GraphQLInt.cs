using GraphQL.IQueryable.Abstractions;

namespace GraphQL.IQueryable.Types
{
    public class GraphQLInt : IGraphQLType, IGraphQLScalarType<int>
    {
        public GraphQLInt(int value)
        {
            Value = value;
        }

        public string Type => "Int";

        public int Value { get; }

        public static implicit operator GraphQLInt(int value)
        {
            return new GraphQLInt(value);
        }
    }
}
