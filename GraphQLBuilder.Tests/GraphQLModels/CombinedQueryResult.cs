using System.Collections.Generic;

namespace GraphQLBuilder.Tests.GraphQLModels
{
    public class CombinedQueryResult
    {
        public Country Country { get; set; }
        public IEnumerable<Continent> Continents { get; set; }
        public IEnumerable<Language> Languages { get; set; }
    }
}