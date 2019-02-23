using GraphQLBuilder.Extensions;
using GraphQLBuilder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphQLBuilder.Helpers
{
    public static class QueryBuilder
    {
        public static string BuildHeader(string entity, IEnumerable<GraphQLQueryParam> parameters)
        {
            var builder = new StringBuilder("query ");

            if (parameters.Any())
            {
                builder.Append("(");

                var queryParameters = parameters.Select(param => $"${param.Key}: {param.Type.Type}");

                builder.Append(string.Join(", ", queryParameters));
                builder.Append(")");
            }

            builder.Append("{ \n");

            builder.Append($"{entity}");
            if (parameters.Any())
            {
                builder.Append("(");

                var parametersString = parameters.Select(p => $"{p.Key}: ${p.Key}");

                builder.Append(string.Join(", ", parametersString));

                builder.Append(")");
            }

            return builder.ToString();
        }

        public static string BuildBody(IEnumerable<GraphQLPropertyModel> properties)
        {
            var builder = new StringBuilder("\n {");
            
            foreach (var property in properties)
            {
                if (!property.SubProperties.Any())
                {
                    builder.Append($"{property.Name.ToCamelCase()},\n");
                } else
                {
                    var str = $"{property.Name.ToCamelCase()} {BuildBody(property.SubProperties)}";

                    builder.Append($"{str}, \n");
                }
            }

            builder.Append("\n }");

            return builder.ToString();
        }

        public static string BuildFooter() => "\n }";
    }
}
