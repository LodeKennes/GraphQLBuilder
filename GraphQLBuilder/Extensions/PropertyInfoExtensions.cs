using System;
using System.Reflection;

namespace GraphQLBuilder.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static bool HasAttribute<T>(this PropertyInfo type, bool inherit = true) where T : Attribute
        {
            return type.GetCustomAttribute(typeof(T), true) != null;
        }

        public static T GetAttribute<T>(this PropertyInfo type, bool inherit = true) where T : Attribute
        {
            return (T)type.GetCustomAttribute(typeof(T), true);
        }
    }
}
