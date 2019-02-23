using System;

namespace GraphQLBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class GraphQLIgnore : Attribute
    {
    }
}
