using GraphQLBuilder.Abstractions;

namespace GraphQLBuilder.Types
{
    public class GraphQLBoolean : IGraphQLScalarType<bool>
    {
        public GraphQLBoolean(bool value)
        {
            Value = value;
        }

        public string Type => "String";

        public bool Value { get; }

        dynamic IGraphQLScalarType.Value => Value;

        public static implicit operator GraphQLBoolean(bool value)
        {
            return new GraphQLBoolean(value);
        }
    }
}
