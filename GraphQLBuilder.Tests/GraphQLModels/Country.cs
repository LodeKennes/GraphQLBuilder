using GraphQLBuilder.Attributes;

namespace GraphQLBuilder.Tests.GraphQLModels
{
    [GraphQLClass]
    public class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Native { get; set; }
        public string Phone { get; set; }
        public string Currency { get; set; }
        public string Emoji { get; set; }
        public string EmojiU { get; set; }
    }
}
