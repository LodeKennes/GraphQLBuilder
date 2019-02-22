using GraphQLBuilder.Abstractions;

namespace GraphQLBuilder.Types
{
    public class GraphQLFloat : IGraphQLScalarType<float>
    {
        public GraphQLFloat(float value)
        {
            Value = value;
        }

        public string Type => "Float";

        public float Value { get; }

        dynamic IGraphQLScalarType.Value => Value;

        public static implicit operator GraphQLFloat(float value)
        {
            return new GraphQLFloat(value);
        }
    }
}
