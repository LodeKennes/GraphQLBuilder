using GraphQLBuilder.Abstractions;

namespace GraphQLBuilder.Types
{
    public class GraphQLString : IGraphQLType, IGraphQLScalarType, IGraphQLScalarType<string>
    {
        public GraphQLString(string value)
        {
            Value = value;
        }

        public string Type => "String";

        public string Value { get; }

        dynamic IGraphQLScalarType.Value => Value;

        public static implicit operator GraphQLString(string value)
        {
            return new GraphQLString(value);
        }
    }
}
