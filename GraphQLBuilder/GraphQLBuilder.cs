using GraphQLBuilder.Abstractions;
using GraphQLBuilder.Extensions;
using GraphQLBuilder.Helpers;
using GraphQLBuilder.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLBuilder
{
    public static class GraphQLBuilder
    {
        public static IGraphQLQuery<T> GraphQL<T>(string entity) where T : class
        {
            var possibleFields = PropertyCollector.BuildPropertyTreeFor<T>();

            if (!possibleFields.Any()) throw new ArgumentException($"Type {typeof(T).Name} doesn't contain any appropriate fields, please use GraphQLClass and GraphQLProperty");

            return new GraphQLQuery<T>(entity, possibleFields);
        }

        public static IGraphQLQuery<T> GraphQL<T>(string entity, params string[] fields) where T : class
        {
            if (!fields.Any()) throw new ArgumentException("Fields should contain atleast one value");

            var possibleFields = PropertyCollector.BuildPropertyTreeFor<T>();

            if (!possibleFields.Any()) throw new ArgumentException($"Type {typeof(T).Name} doesn't contain any appropriate fields, please use GraphQLClass and GraphQLProperty");

            return new GraphQLQuery<T>(entity, possibleFields);
        }

        private static IEnumerable<string> GetNonValidFields(Type type, IEnumerable<string> fields) {
            var publicFields = type.GetSettableProperties();
            var publicFieldNames = publicFields.SelectNames().AllToLower();

            var lowerFields = fields.AllToLower();

            return lowerFields.Where(f => !publicFieldNames.Contains(f));
        }
    }
}
