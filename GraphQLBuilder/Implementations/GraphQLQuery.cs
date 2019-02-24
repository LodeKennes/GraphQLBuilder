using GraphQLBuilder.Abstractions;
using GraphQLBuilder.Helpers;
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
        private readonly IList<GraphQLQueryParam> _params;
        public IEnumerable<GraphQLQueryParam> Parameters => _params;

        public string Entity { get; }
        public IEnumerable<GraphQLPropertyModel> Properties { get; }

        public Type ReturnType => typeof(T);

        public GraphQLQuery(string entity, IEnumerable<GraphQLPropertyModel> properties)
        {
            Entity = entity;
            Properties = properties;

            _params = new List<GraphQLQueryParam>();
        }

        public IGraphQLQuery<T> WithParam<TType>(string key, TType value) where TType : IGraphQLScalarType
        {
            _params.Add(new GraphQLQueryParam(key, value));

            return this;
        }

        public IGraphQLRequest GetRequest()
        {
            var builder = new StringBuilder(QueryBuilder.BuildHeader(Entity, _params));

            builder.Append(QueryBuilder.BuildBody(Properties));

            builder.Append(QueryBuilder.BuildFooter());

            var parametersObject = new ExpandoObject() as IDictionary<string, object>; ;

            foreach (var param in _params)
            {
                var scalar = (IGraphQLScalarType)param.Type;
                parametersObject.Add(param.Key, scalar.Value);
            }

            return new GraphQLRequest(Entity, builder.ToString(), (dynamic)parametersObject);
        }

        public static CombinedGraphQLQuery operator &(GraphQLQuery<T> s1, IGraphQLQuery s2)
        {
            if (s1._params.Select(p => p.Key).Any(k => s2.Parameters.Select(p => p.Key).Contains(k))) throw new Exception("Duplicate parameter found in queries");


            IList<GraphQLQueryModel> models = new List<GraphQLQueryModel>
            {
                new GraphQLQueryModel(s1.Entity, s1.Properties, s1._params, s1.ReturnType),
                new GraphQLQueryModel(s2.Entity, s2.Properties, s2.Parameters, s2.ReturnType)
            };


            return new CombinedGraphQLQuery(models);
        }
    }
}
