using GraphQLBuilder.Abstractions;
using System;
using System.Collections.Generic;

namespace GraphQLBuilder.Types
{
    public class GraphQLList<T> : IGraphQLType where T : IGraphQLScalarType, new()
    {
        public string Type { get {
                var type = (IGraphQLScalarType) Activator.CreateInstance<T>();

                return $"[{type.Type}]";
            }
        }

        public IEnumerable<T> Value => throw new NotImplementedException();

        //public static implicit operator GraphQLList<T>(IEnumerable<T> items)
        //{
        //    return new GraphQLList<T>();
        //}
    }
}
