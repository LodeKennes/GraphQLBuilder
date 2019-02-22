using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQLBuilder.Abstractions;

namespace GraphQLBuilder.Implementations
{
    public class GraphQLRequest<T> : IGraphQLRequest<T>
    {
        private readonly string _entity;
        private readonly string _query;
        private readonly dynamic _parameters;

        private readonly IDictionary<string, string> _headers;
        private Uri _uri;

        public GraphQLRequest(string entity, string query, dynamic parameters = null)
        {
            _entity = entity;
            _query = query;
            _parameters = parameters;
            _headers = new Dictionary<string, string>();
        }

        public async Task<T> Response()
        {
            using (var client = new GraphQLClient(_uri))
            {
                var request = new GraphQL.Common.Request.GraphQLRequest
                {
                    Query = _query,
                    Variables = _parameters
                };
                var result = await client.PostAsync(request);

                return result.GetDataFieldAs<T>(_entity);
            }
        }

        public IGraphQLRequest<T> WithHeader(string header, string value)
        {
            if (_headers.ContainsKey(header)) _headers[header] = value;
            else _headers.Add(header, value);

            return this;
        }

        public IGraphQLRequest<T> WithUri(Uri uri)
        {
            _uri = uri;
            return this;
        }

        public IGraphQLRequest<T> WithUri(string uri)
        {
            return WithUri(new Uri(uri));
        }
    }
}
