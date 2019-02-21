using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GraphQL.IQueryable.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<string> AllToLower(this IEnumerable<string> items)
        {
            return items.Select(i => i.ToLower());
        }

        public static IEnumerable<string> SelectNames(this IEnumerable<PropertyInfo> propertyInfos)
        {
            return propertyInfos.Select(fi => fi.Name);
        }

        public static IEnumerable<PropertyInfo> GetFieldsWithAttribute<TAttribute>(this Type type, bool inherit = true) where TAttribute : Attribute
        {
            var attributeType = typeof(TAttribute);

            return type.GetProperties().Where(f => f.GetCustomAttribute(attributeType, inherit) != null);
        }
    }
}
