using System;

namespace GraphQLBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class GraphQLProperty : Attribute
    {
    }
}
