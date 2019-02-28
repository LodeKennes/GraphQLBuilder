using GraphQL.Client;
using GraphQLBuilder.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLBuilder.Implementations
{
    public class GraphQLCombinedRequest : IGraphQLRequest
    {
        private readonly string _query;
        private readonly dynamic _parameters;

        private readonly IDictionary<string, string> _headers;
        private Uri _uri;

        public GraphQLCombinedRequest(string query, dynamic parameters = null)
        {
            _query = query;
            _parameters = parameters;
            _headers = new Dictionary<string, string>();
        }

        public async Task<T> GetResponseAsync<T>()
        {
            using (var client = new GraphQLClient(_uri))
            {
                var request = new GraphQL.Common.Request.GraphQLRequest
                {
                    Query = _query,
                    Variables = _parameters
                };
                
                foreach (var header in _headers) {
                    if (client.DefaultRequestHeaders.Contains(header.Key))
                        client.DefaultRequestHeaders.Remove(header.Key);
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }

                var result = await client.PostAsync(request);

                if (result.Errors != null && result.Errors.Any())
                {
                    throw new AggregateException(result.Errors.Select(x => new Exception($"Message: {x.Message}\nLocations: {x.Locations}")));
                }

                return ((JObject)result.Data).ToObject<T>();
            }
        }

        public IGraphQLRequest WithHeader(string header, string value)
        {
            if (_headers.ContainsKey(header)) _headers[header] = value;
            else _headers.Add(header, value);

            return this;
        }

        public IGraphQLRequest WithUri(Uri uri)
        {
            _uri = uri;
            return this;
        }

        public IGraphQLRequest WithUri(string uri)
        {
            return WithUri(new Uri(uri));
        }
    }
}
