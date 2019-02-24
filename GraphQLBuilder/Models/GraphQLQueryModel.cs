using System;
using System.Collections.Generic;

namespace GraphQLBuilder.Models
{
    public struct GraphQLQueryModel
    {
        public GraphQLQueryModel(string entity, IEnumerable<GraphQLPropertyModel> properties, IEnumerable<GraphQLQueryParam> parameters, Type returnType)
        {
            Entity = entity;
            Properties = properties;
            Parameters = parameters;
            ReturnType = returnType;
        }

        public string Entity { get; }
        public IEnumerable<GraphQLPropertyModel> Properties { get; }
        public IEnumerable<GraphQLQueryParam> Parameters { get; }
        public Type ReturnType { get; }
    }
}
