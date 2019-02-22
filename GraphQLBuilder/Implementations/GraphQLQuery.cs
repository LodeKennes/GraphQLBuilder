using GraphQLBuilder.Abstractions;
using GraphQLBuilder.Extensions;
using GraphQLBuilder.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace GraphQLBuilder.Implementations
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

            var parametersObject = new ExpandoObject() as IDictionary<string, object>; ;

            foreach (var param in _params)
            {
                var scalar = (IGraphQLScalarType)param.Type;
                parametersObject.Add(param.Key, scalar.Value);
            }

            return new GraphQLRequest<T>(_entity, builder.ToString(), (dynamic) parametersObject);
        }
    }
}
