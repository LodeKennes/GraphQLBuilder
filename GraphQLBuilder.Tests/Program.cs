using GraphQLBuilder.Implementations;
using GraphQLBuilder.Tests.GraphQLModels;
using GraphQLBuilder.Types;
using System;
using System.Collections.Generic;

namespace GraphQLBuilder.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var query1 = GraphQLBuilder.GraphQL<Country>("country")
                .WithParam<GraphQLString>("code", "AD") as GraphQLQuery<Country>;
            var query2 = GraphQLBuilder.GraphQL<IEnumerable<Continent>>("continents")
                as GraphQLQuery<IEnumerable<Continent>>;

            var combinedQuery = query1 & query2;

            var cqstr = combinedQuery.GetRequest();
            var result = cqstr.WithUri("https://countries.trevorblades.com/").GetResponseAsync<dynamic>().GetAwaiter().GetResult();

            Console.WriteLine("Hello World!");
        }
    }
}
