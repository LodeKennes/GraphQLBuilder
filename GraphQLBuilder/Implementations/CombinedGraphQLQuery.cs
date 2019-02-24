using GraphQLBuilder.Abstractions;
using GraphQLBuilder.Helpers;
using GraphQLBuilder.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace GraphQLBuilder.Implementations
{
    public class CombinedGraphQLQuery
    {
        protected IList<GraphQLQueryModel> Queries { get; }
        protected IEnumerable<GraphQLQueryParam> Parameters => Queries.SelectMany(q => q.Parameters);

        internal CombinedGraphQLQuery(IEnumerable<GraphQLQueryModel> queries)
        {
            Queries = queries.ToList();
        }

        public static CombinedGraphQLQuery operator &(CombinedGraphQLQuery combined, IGraphQLQuery s2)
        {
            if (combined.Parameters.Select(p => p.Key).Any(k => s2.Parameters.Select(p => p.Key).Contains(k))) throw new System.Exception("Duplicate parameter found in queries");

            combined.Queries.Add(new GraphQLQueryModel(s2.Entity, s2.Properties, s2.Parameters, s2.ReturnType));

            return combined;
        }

        public IGraphQLRequest GetRequest()
        {
            var parametersObject = new ExpandoObject() as IDictionary<string, object>; ;

            foreach (var param in Parameters)
            {
                var scalar = (IGraphQLScalarType)param.Type;
                parametersObject.Add(param.Key, scalar.Value);
            }

            return new GraphQLCombinedRequest(BuildQuery(), (dynamic)parametersObject);
        }

        private string BuildQuery()
        {
            var builder = new StringBuilder();

            builder.Append(QueryBuilder.BuildQueryHeader(Parameters));

            foreach(var query in Queries)
            {
                builder.Append(QueryBuilder.BuildHeader(query.Entity, query.Parameters));
                builder.Append(QueryBuilder.BuildBody(query.Properties));
                builder.Append(",");
            }

            builder.Append(QueryBuilder.BuildFooter());

            return builder.ToString();
        }
    }
}
