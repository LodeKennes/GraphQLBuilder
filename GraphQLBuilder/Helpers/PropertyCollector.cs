using GraphQLBuilder.Attributes;
using GraphQLBuilder.Extensions;
using GraphQLBuilder.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GraphQLBuilder.Helpers
{
    internal static class PropertyCollector
    {
        public static IEnumerable<GraphQLPropertyModel> BuildPropertyTreeFor<T>()
        {
            return BuildPropertyTreeFor(typeof(T));
        }

        public static IEnumerable<GraphQLPropertyModel> BuildPropertyTreeFor(Type type)
        {
            if (type.ImplementsInterface(typeof(IEnumerable))) type = type.GetGenericArguments()[0];

            var possibleProperties = new List<PropertyInfo>();

            if (!type.HasAttribute<GraphQLClass>())
            {
                possibleProperties.AddRange(type.GetPropertiesWithAttribute<GraphQLProperty>());
                possibleProperties.AddRange(type.GetPropertiesWithAttribute<GraphQLNestedProperty>());
            }
            else
            {
                possibleProperties.AddRange(type.GetSettableProperties());
            }

            possibleProperties = possibleProperties.Where(p => !p.HasAttribute<GraphQLIgnore>()).ToList();
            
            var simpleProperties = possibleProperties.Where(t => !t.HasAttribute<GraphQLNestedProperty>());
            var nestedProperties = possibleProperties.Where(t => t.HasAttribute<GraphQLNestedProperty>());

            return simpleProperties.Select(p => new GraphQLPropertyModel(p.Name)).Concat(nestedProperties.Select(p => {
                var gqlModel = new GraphQLPropertyModel(p.Name);
                
                foreach (var model in BuildPropertyTreeFor(p.PropertyType))
                {
                    gqlModel.SubProperties.Add(model);
                }

                return gqlModel;
            }));
        }
    }
}
