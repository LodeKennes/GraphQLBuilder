using GraphQL.IQueryable.Tests.GraphQLModels;
using System;
using System.Collections.Generic;

namespace GraphQL.IQueryable.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var query1 = GraphQLBuilder.GraphQL<IEnumerable<Country>>("countries");
            var result = query1.Build();
            var response = result.WithUri("https://countries.trevorblades.com/").Response().GetAwaiter().GetResult();

            Console.WriteLine("Hello World!");
        }
    }
}
