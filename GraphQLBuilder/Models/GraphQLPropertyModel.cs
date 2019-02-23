using System.Collections.Generic;

namespace GraphQLBuilder.Models
{
    public struct GraphQLPropertyModel
    {
        public GraphQLPropertyModel(string name)
        {
            Name = name;
            SubProperties = new List<GraphQLPropertyModel>();
        }

        public string Name { get; }
        public IList<GraphQLPropertyModel> SubProperties { get; }
    }
}
