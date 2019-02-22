using GraphQLBuilder.Abstractions;

namespace GraphQLBuilder.Types
{
    public class GraphQLId : IGraphQLScalarType<string>
    {
        public GraphQLId(string value)
        {
            Value = value;
        }

        public string Type => "String";

        public string Value { get; }

        dynamic IGraphQLScalarType.Value => Value;

        public static implicit operator GraphQLId(string value)
        {
            return new GraphQLId(value);
        }
    }
}
