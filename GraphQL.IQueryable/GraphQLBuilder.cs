using GraphQL.IQueryable.Abstractions;
using GraphQL.IQueryable.Attributes;
using GraphQL.IQueryable.Extensions;
using GraphQL.IQueryable.Implementations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphQL.IQueryable
{
    public static class GraphQLBuilder
    {
        public static IGraphQLQuery<T> GraphQL<T>(string entity) where T : class
        {
            var type = typeof(T);

            if (type.ImplementsInterface(typeof(IEnumerable))) type = type.GetGenericArguments()[0];

            var possibleFields = new List<string>();

            if (!type.HasAttribute<GraphQLClass>()) {
                possibleFields.AddRange(type.GetFieldsWithAttribute<GraphQLProperty>().SelectNames());
            } else
            {
                possibleFields.AddRange(type.GetSettableProperties().SelectNames());
            }

            if (possibleFields.Count == 0) throw new ArgumentException($"Type {type.Name} doesn't contain any appropriate fields, please use GraphQLClass and GraphQLProperty");

            return new GraphQLQuery<T>(entity, possibleFields);
        }

        public static IGraphQLQuery<T> GraphQL<T>(string entity, params string[] fields) where T : class
        {
            if (!fields.Any()) throw new ArgumentException("Fields should contain atleast one value");

            var type = typeof(T);

            if (type.ImplementsGenericInterface(typeof(IEnumerable<>))) type = type.GetGenericTypeDefinition();

            var nonValidFields = GetNonValidFields(type, fields);

            if (nonValidFields.Any()) throw new ArgumentException($"Couldn't find fields {string.Join(", ", nonValidFields)} on type ${type.Name}");

            return new GraphQLQuery<T>(entity, fields);
        }

        private static IEnumerable<string> GetNonValidFields(Type type, IEnumerable<string> fields) {
            var publicFields = type.GetSettableProperties();
            var publicFieldNames = publicFields.SelectNames().AllToLower();

            var lowerFields = fields.AllToLower();

            return lowerFields.Where(f => !publicFieldNames.Contains(f));
        }
    }
}
