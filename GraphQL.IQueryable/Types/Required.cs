using GraphQL.IQueryable.Abstractions;
using System;

namespace GraphQL.IQueryable.Types
{
    public class Required<T> : IGraphQLType where T : IGraphQLType, new()
    {
        public string Type { 
            get {
                var type = (IGraphQLType) Activator.CreateInstance<T>();

                return $"{type.Type}";
            }
        }
    }
}
