using System;

namespace GraphQLBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class GraphQLNestedProperty : Attribute
    {
    }
}
