using GraphQL.IQueryable.Attributes;

namespace GraphQL.IQueryable.Tests.GraphQLModels
{
    [GraphQLClass]
    public class Country
    {
        public string Code { get; set; }
    }
}
