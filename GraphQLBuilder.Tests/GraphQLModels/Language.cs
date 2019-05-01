using GraphQLBuilder.Attributes;

namespace GraphQLBuilder.Tests.GraphQLModels
{
    [GraphQLClass]
    public class Language
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Native { get; set; }
        public int? Rtl{ get; set; }
    }
}