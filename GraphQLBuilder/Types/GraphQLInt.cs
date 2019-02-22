using GraphQLBuilder.Abstractions;

namespace GraphQLBuilder.Types
{
    public class GraphQLInt : IGraphQLType, IGraphQLScalarType, IGraphQLScalarType<int>
    {
        public GraphQLInt(int value)
        {
            Value = value;
        }

        public string Type => "Int";

        public int Value { get; }

        dynamic IGraphQLScalarType.Value => Value;

        public static implicit operator GraphQLInt(int value)
        {
            return new GraphQLInt(value);
        }
    }
}
