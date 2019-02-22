using GraphQLBuilder.Tests.GraphQLModels;
using GraphQLBuilder.Types;
using System;

namespace GraphQLBuilder.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var query1 = GraphQLBuilder.GraphQL<Country>("country")
                .WithParam<GraphQLString>("code", "AD")
                .Build()
                .WithUri("https://countries.trevorblades.com/").Response().GetAwaiter().GetResult();

            Console.WriteLine("Hello World!");
        }
    }
}
