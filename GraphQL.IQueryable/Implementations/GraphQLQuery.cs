using GraphQL.IQueryable.Abstractions;
using GraphQL.IQueryable.Extensions;
using GraphQL.IQueryable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphQL.IQueryable.Implementations
{
    public class GraphQLQuery<T> : IGraphQLQuery<T>
    {
        private readonly string _entity;
        private readonly IEnumerable<string> _fields;
        private readonly IList<GraphQLQueryParam> _params;

        public GraphQLQuery(string entity, IEnumerable<string> fields)
        {
            _entity = entity;
            _fields = fields;

            _params = new List<GraphQLQueryParam>();
        }

        public IGraphQLQuery<T> WithParam<TType>(string key, TType value) where TType : IGraphQLType
        {
            if (!value.GetType().ImplementsGenericInterface(typeof(IGraphQLScalarType<>))) throw new ArgumentException($"Given type {value.GetType().Name} doesn't implement IGraphQLScalarType"); 

            _params.Add(new GraphQLQueryParam(key, value));

            return this;
        }

        public IGraphQLRequest<T> Build()
        {
            var builder = new StringBuilder("query ");

            if (_params.Any())
            {
                builder.Append("(");

                var queryParameters = _params.Select(param => $"${param.Key}: {param.Type.Type}");

                builder.Append(string.Join(", ", queryParameters));
                builder.Append(")");
            }

            builder.Append("{ \n");

            builder.Append($"{_entity}");

            if (_params.Any())
            {
                builder.Append("(");

                var parameters = _params.Select(p => $"{p.Key}: ${p.Key}");

                builder.Append(string.Join(", ", parameters));

                builder.Append(")");
            }

            builder.Append("\n {");

            builder.Append(string.Join(",\n", _fields.ToCamelCase()));

            builder.Append("\n}");
            builder.Append("\n}");

            return new GraphQLRequest<T>(_entity, builder.ToString());
        }
    }
}
