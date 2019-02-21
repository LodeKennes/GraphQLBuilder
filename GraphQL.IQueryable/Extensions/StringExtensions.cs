using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQL.IQueryable.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return Char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }

        public static IEnumerable<string> ToCamelCase(this IEnumerable<string> items)
        {
            return items.Select(i => i.ToCamelCase());
        }
    }
}
