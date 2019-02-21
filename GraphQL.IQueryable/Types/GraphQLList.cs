using GraphQL.IQueryable.Abstractions;
using System;
using System.Collections.Generic;

namespace GraphQL.IQueryable.Types
{
    public class GraphQLList<T> : IGraphQLType where T : IGraphQLType, new()
    {
        public string Type { get {
                var type = (IGraphQLType) Activator.CreateInstance<T>();

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
