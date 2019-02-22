using GraphQLBuilder.Abstractions;
using System;

namespace GraphQLBuilder.Types
{
    public class Required<T> : IGraphQLType where T : IGraphQLScalarType, new()
    {
        public string Type { 
            get {
                var type = (IGraphQLScalarType) Activator.CreateInstance<T>();

                return $"{type.Type}";
            }
        }
    }
}
