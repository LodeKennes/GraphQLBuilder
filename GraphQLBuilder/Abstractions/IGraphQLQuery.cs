using GraphQLBuilder.Models;
using System;
using System.Collections.Generic;

namespace GraphQLBuilder.Abstractions
{
    public interface IGraphQLQuery<in T> : IGraphQLQuery
    {
        IGraphQLQuery<T> WithParam<TType>(string key, TType value) where TType : IGraphQLScalarType;
        IGraphQLRequest GetRequest();
    }

    public interface IGraphQLQuery
    {
        string Entity { get; }
        IEnumerable<GraphQLQueryParam> Parameters { get; }
        IEnumerable<GraphQLPropertyModel> Properties { get; }
        Type ReturnType { get; }
    }
}
