using System;
using System.Linq;
using System.Reflection;

namespace GraphQLBuilder.Extensions
{
    public static class TypeExtensions
    {
        public static PropertyInfo[] GetSettableProperties(this Type type)
        {
            return type.GetProperties().Where(f => f.SetMethod != null).ToArray();
        }

        public static bool HasAttribute<T>(this Type type, bool inherit = true) where T : Attribute
        {
            return type.GetCustomAttribute(typeof(T), true) != null;
        }

        public static T GetAttribute<T>(this Type type, bool inherit = true) where T : Attribute
        {
            return (T)type.GetCustomAttribute(typeof(T), true);
        }

        public static bool ImplementsGenericInterface(this Type type, Type intType)
        {
            if (!intType.IsInterface) throw new ArgumentException("Given argument is not an interface");

            return type.GetInterfaces().Any(x =>
                x.IsGenericType &&
                x.GetGenericTypeDefinition() == intType);
        }

        public static bool ImplementsInterface(this Type type, Type intType)
        {
            if (!intType.IsInterface) throw new ArgumentException("Given argument is not an interface");

            return type.GetInterfaces().Any(x =>
                x == intType);
        }
    }
}
