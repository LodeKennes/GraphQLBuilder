using GraphQLBuilder.Abstractions;
using GraphQLBuilder.Helpers;
using GraphQLBuilder.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace GraphQLBuilder.Implementations
{
    public class GraphQLQuery<T> : IGraphQLQuery<T>
    {
        private readonly string _entity;
        private readonly IEnumerable<GraphQLPropertyModel> _properties;
        private readonly IList<GraphQLQueryParam> _params;

        public GraphQLQuery(string entity, IEnumerable<GraphQLPropertyModel> fields)
        {
            _entity = entity;
            _properties = fields;

            _params = new List<GraphQLQueryParam>();
        }

        public IGraphQLQuery<T> WithParam<TType>(string key, TType value) where TType : IGraphQLScalarType
        {
            _params.Add(new GraphQLQueryParam(key, value));

            return this;
        }

        public IGraphQLRequest<T> GetRequest()
        {
            var builder = new StringBuilder(QueryBuilder.BuildHeader(_entity, _params));

            builder.Append(QueryBuilder.BuildBody(_properties));
            builder.Append(QueryBuilder.BuildFooter());

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
